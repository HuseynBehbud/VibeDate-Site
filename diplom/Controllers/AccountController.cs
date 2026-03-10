using diplom.Models;
using diplom.ViewModel;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;

namespace diplom.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if (!ModelState.IsValid) return View(user);

            AppUser newUser = new AppUser
            {
                FullName = user.FirstName + " " + user.LastName,
                UserName = user.Email, 
                Email = user.Email,
                Location = user.Location
            };

            var identityResult = await _userManager.CreateAsync(newUser, user.Password);

            if (identityResult.Succeeded)
            {
                if (await _roleManager.RoleExistsAsync("Member"))
                {
                    await _userManager.AddToRoleAsync(newUser, "Member");
                }

                await _signInManager.SignInAsync(newUser, isPersistent: true);
                return RedirectToAction("Welcome", "Home");
            }

            foreach (var err in identityResult.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel LoginVM)
        {
            if(!ModelState.IsValid) return View(LoginVM);

            AppUser existUser = await _userManager.FindByNameAsync(LoginVM.UserNameOrEmail);
            if(existUser == null)
            {
                existUser = await _userManager.FindByEmailAsync(LoginVM.UserNameOrEmail);
                if(existUser == null)
                {
                    ModelState.AddModelError("", "Email or password is wrong!");
                    return View(LoginVM);
                }
            }

            if (await _userManager.IsInRoleAsync(existUser, "Admin"))
            {
                ModelState.AddModelError("", "You are not allowed to login here.");
                return View();
            }
            var result = await _userManager.CheckPasswordAsync(existUser, LoginVM.Password);
            if (!result)
            {
                ModelState.AddModelError("", "The provided credentials are incorrect.");
                return View();
            }

            await _signInManager.SignInAsync(existUser, LoginVM.RememberMe);
            return RedirectToAction("Welcome", "Home");
        }

        public IActionResult Settings()
        {
            var currentUser = _userManager.GetUserAsync(User);
            if (currentUser == null)
                return RedirectToAction("Login");
            var userSettingsVM = new UserSettingsViewModel
            {
                FirstName = currentUser.Result.FullName.Split(" ")[0],
                LastName = currentUser.Result.FullName.Split(" ")[1],
                Email = currentUser.Result.Email,
                Location = currentUser.Result.Location
            };
            return View(userSettingsVM);
        }
        [HttpPost]
        public async Task<IActionResult> Settings(UserSettingsViewModel userSettingsVM)
        {
            if (!ModelState.IsValid) return View(userSettingsVM);

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
                return RedirectToAction("Login");
            currentUser.FullName = userSettingsVM.FirstName + " " + userSettingsVM.LastName;
            currentUser.Email = userSettingsVM.Email;
            currentUser.Location = userSettingsVM.Location;

            var result = await _userManager.UpdateAsync(currentUser);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPasswordAsync(ForgetPasswordVM forgetPasswordVM)
        {
            if (!ModelState.IsValid) return View();
            var existUser = await _userManager.FindByEmailAsync(forgetPasswordVM.Email);
            if (existUser is null)
            {
                ModelState.AddModelError("", "Email not found");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(existUser);
            var url = Url.Action("resetpassword", "account", new { email = existUser.Email, token }, Request.Scheme);

            // user,gmail,path
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("guliyev1290@gmail.com"));
            email.To.Add(MailboxAddress.Parse(existUser.Email));
            email.Subject = $"Hi {existUser.FullName}. Please reset your password";
            using StreamReader reader = new StreamReader("wwwroot/templates/forgetpassword.html");
            string body = await reader.ReadToEndAsync();
            body = body.Replace("{{username}}", existUser.FullName);
            body = body.Replace("{{url}}", url);
            body = body.Replace("{{email}}", existUser.Email);
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            //send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("guliyev1290@gmail.com", "nnsg uqry hffm sjzj");
            smtp.Send(email);
            smtp.Disconnect(true);
            return RedirectToAction("index", "home");
        }

        public IActionResult ResetPassword(string email, string token)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new ResetPasswordVM
            {
                Email = email,
                Token = token
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordVM resetPasswordVM)
        {
            if (!ModelState.IsValid) return View();
            var existUser = await _userManager.FindByEmailAsync(resetPasswordVM.Email);
            if (existUser is null)
            {
                ModelState.AddModelError("", "Email not found.");
                return View();
            }
            var result = await _userManager.ResetPasswordAsync(existUser, resetPasswordVM.Token, resetPasswordVM.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            // create email
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("guliyev1290@gmail.com"));
            email.To.Add(MailboxAddress.Parse(existUser.Email));
            email.Subject = $"Hi {existUser.FullName}. Your password has changed.";
            using StreamReader reader = new StreamReader("wwwroot/templates/notify.html");
            string body = await reader.ReadToEndAsync();
            body = body.Replace("{{username}}", existUser.FullName);
            body = body.Replace("{{email}}", existUser.Email);
            email.Body = new TextPart(TextFormat.Html) { Text = body };

            //send email
            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("guliyev1290@gmail.com", "nnsg uqry hffm sjzj");
            smtp.Send(email);
            smtp.Disconnect(true);

            return RedirectToAction("login");
        }


        //public async Task<IActionResult> CreateRoles()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole("Member"));
        //    await _roleManager.CreateAsync(new IdentityRole("Admin"));
        //    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
        //    return Content("Roles created");

        //}
    }
}
