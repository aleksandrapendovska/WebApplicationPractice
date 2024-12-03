using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationPractice.Data;
using WebApplicationPractice.Models;

namespace WebApplicationPractice.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public UsersController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User viewModel)
        {
            if (await dbContext.Users.AnyAsync(u => u.Username == viewModel.Username))
            {
                ModelState.AddModelError("Username", "This username is already taken.");
                return View(viewModel);
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(viewModel.Password);

            var user = new User
            {
                Username = viewModel.Username,
                Password = hashedPassword 
            };

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User viewModel)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == viewModel.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(viewModel.Password, user.Password))
            {
                ModelState.AddModelError("Password", "The username or password you have entered is incorrect");
            }
            return RedirectToAction("Index", "Products");
        }
    }
}
