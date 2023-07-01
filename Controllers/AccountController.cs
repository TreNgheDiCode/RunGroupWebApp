using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RunGroupWebApp.Data;
using RunGroupWebApp.Models;
using RunGroupWebApp.ViewModels;

namespace RunGroupWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DataContext _context;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, DataContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View();

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);

            if (user != null)
            {
                //User found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (passwordCheck)
                {
                    // Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Race");
                    }

                    // Password is incorrect
                    TempData["Error"] = "Wrong credentials. Please try again.";
                }
            }

            //User not found
            TempData["Error"] = "Wrong credentials. Plese try again.";

            return View(loginVM);
        }
    }
}
