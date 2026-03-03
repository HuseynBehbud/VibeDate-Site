using diplom.Models;
using diplom.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace diplom.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }
        private RoleManager<IdentityRole> _roleManager { get; }
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
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            AppUser existUser = await _userManager.FindByEmailAsync(user.Email);
            if (existUser == null)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(user);
            }
            var signInResult = await _signInManager.PasswordSignInAsync(existUser, user.Password, true, true);
            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account is blocked for 5 minutes");
                return View(user);
            }
            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email or password is wrong");
                return View(user);
            }
            return RedirectToAction("Welcome", "Home");
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
