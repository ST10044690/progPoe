using Agri_Energy_Connect.Data;
using Agri_Energy_Connect.Models;
using Agri_Energy_Connect.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Agri_Energy_Connect.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by email
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {

                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserRole", user.Role);

                    if (user.Role == "farmer")
                    {
                        return RedirectToAction("Dashboard", "Farmer");
                    }
                    else if (user.Role == "employee")
                    {
                        return RedirectToAction("DashboardE", "Employee");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User user, string role)
        {
            if (ModelState.IsValid)
            {

                user.Role = role;

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();


                if (role == "farmer")
                {
                    var farmer = new Farmer
                    {
                        UserId = user.Id,
                        JoinDate = DateTime.Now
                    };

                    await _context.Farmers.AddAsync(farmer);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Dashboard", "Farmer");
                }

                if (role == "employee")
                {
                    return RedirectToAction("DashboardE", "Employee");
                }

                return RedirectToAction("Login");
            }
            return View(user);
        }

        [HttpPost]
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }


    }
}
