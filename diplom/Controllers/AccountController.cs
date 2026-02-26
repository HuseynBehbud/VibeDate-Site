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
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel user)
        {
            if(!ModelState.IsValid) return View(user);
            AppUser newUser = new AppUser
            {
               FullName= user.FirstName,
               UserName=user.LastName,
                Email=user.Email,
            };
            var identityResult= await _userManager.CreateAsync(newUser,user.Password);
            if(!identityResult.Succeeded)
            {
                foreach (var err in identityResult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return View(user);
            }
            await _signInManager.SignInAsync(newUser, true); 
            return RedirectToAction("Welcome", "Home");
        }
    }
}
