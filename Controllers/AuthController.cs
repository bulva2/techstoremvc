using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechStoreMVC.Database;
using TechStoreMVC.Entities;
using TechStoreMVC.Helper;
using TechStoreMVC.Models.Auth;

namespace TechStoreMVC.Controllers
{
    public class AuthController : BaseController
    {
        private DatabaseContext _context;

        public AuthController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            string hashedPassword = SHA256Helper.HashPassword(loginViewModel.Password);
            Account? acc = await _context.Accounts.SingleOrDefaultAsync(a => (a.Username == loginViewModel.Username) && (a.Password == hashedPassword));

            if (acc == null) {
                return View(loginViewModel);
            }

            HttpContext.Session.SetString("username", acc.Username);
            HttpContext.Session.SetString("role", acc.Role);

            return RedirectToAction("index", "home");
        }

        public IActionResult Register()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Entered information is invalid!";
                TempData["MessageType"] = "danger";

                return View(loginViewModel);
            }

            Account? existingAcc = await _context.Accounts.SingleOrDefaultAsync(a => a.Username == loginViewModel.Username);
            if (existingAcc != null)
            {
                TempData["Message"] = "Entered username is already in use!";
                TempData["MessageType"] = "danger";
            }

            Account newAcc = new Account(0, loginViewModel.Username, SHA256Helper.HashPassword(loginViewModel.Password), "user", null);
            await _context.Accounts.AddAsync(newAcc);
            await _context.SaveChangesAsync();

            TempData["Message"] = "New account has been successfully created!";
            TempData["MessageType"] = "success";

            return RedirectToAction("Login", "Auth");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("username");
            HttpContext.Session.Remove("role");
            return RedirectToAction("index", "home");
        }
    }
}
