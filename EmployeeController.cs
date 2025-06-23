using Agri_Energy_Connect.Data;
using Agri_Energy_Connect.Models;
using Agri_Energy_Connect.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Agri_Energy_Connect.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> DashboardE()
        {
            ViewBag.FarmerCount = await _context.Farmers.CountAsync();
            ViewBag.ProductCount = await _context.Products.CountAsync();
            ViewBag.CategoryCount = await _context.Categories.CountAsync();

            var recentProducts = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .OrderByDescending(p => p.ProductionDate)
                .Take(10)
                .ToListAsync();

            return View(recentProducts);
        }

        [HttpGet]
        public async Task<IActionResult> FarmerDatabase()
        {
            var farmers = await _context.Farmers
                .Include(f => f.Products)
                .OrderByDescending(f => f.JoinDate)
                .ToListAsync();

            return View(farmers);
        }
        [HttpGet]
        public async Task<IActionResult> ProductList(
            int? farmerId = null,
            int? categoryId = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string searchTerm = null)
        {
            var productsQuery = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Farmer)
                .AsQueryable();

            // Apply filters
            if (farmerId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.FarmerId == farmerId.Value);
                ViewBag.FarmerName = await _context.Farmers
                    .Where(f => f.Id == farmerId.Value)
                    .Select(f => f.FarmName)
                    .FirstOrDefaultAsync();
            }

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            if (minPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.Price <= maxPrice.Value);
            }

            if (startDate.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.ProductionDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                // Add one day to include the end date in the results
                var nextDay = endDate.Value.AddDays(1);
                productsQuery = productsQuery.Where(p => p.ProductionDate < nextDay);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm) ||
                                                       p.Description.Contains(searchTerm));
            }

            var products = await productsQuery
                .OrderByDescending(p => p.ProductionDate)
                .ToListAsync();

            // Pass categories for filter dropdown
            ViewBag.Categories = await _context.Categories.ToListAsync();

            // Pass current filter values to the view for maintaining state
            ViewBag.CurrentCategoryId = categoryId;
            ViewBag.CurrentMinPrice = minPrice;
            ViewBag.CurrentMaxPrice = maxPrice;
            ViewBag.CurrentStartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.CurrentEndDate = endDate?.ToString("yyyy-MM-dd");
            ViewBag.CurrentSearchTerm = searchTerm;
            ViewBag.FarmerId = farmerId;

            return View(products);
        }



        [HttpGet]
        public async Task<IActionResult> ViewFarmerProducts(int id)
        {
            var farmer = await _context.Farmers
                .Include(f => f.Products)
                .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (farmer == null)
            {
                return NotFound();
            }

            return View(farmer);
        }

        [HttpPost]
        public async Task<IActionResult> AddFarmer(FarmerRegistrationViewModel model)
        {
            try
            {
                if (model.UserDetails == null)
                {
                    model.UserDetails = new User();
                }

                bool isValid = !string.IsNullOrEmpty(model.UserDetails.Name) &&
                              !string.IsNullOrEmpty(model.UserDetails.LastName) &&
                              !string.IsNullOrEmpty(model.UserDetails.Email) &&
                              !string.IsNullOrEmpty(model.UserDetails.Password) &&
                              !string.IsNullOrEmpty(model.FarmName) &&
                              !string.IsNullOrEmpty(model.Address);

                if (isValid)
                {
                    var user = new User
                    {
                        Name = model.UserDetails.Name,
                        LastName = model.UserDetails.LastName,
                        Email = model.UserDetails.Email,
                        Password = model.UserDetails.Password,
                        Role = "farmer"
                    };

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    var farmer = new Farmer
                    {
                        UserId = user.Id,
                        FarmName = model.FarmName,
                        Address = model.Address,
                        PhoneNumber = model.PhoneNumber,
                        Email = model.UserDetails.Email,
                        Description = model.Description,
                        JoinDate = DateTime.Now
                    };

                    await _context.Farmers.AddAsync(farmer);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Farmer registered successfully!";
                    return RedirectToAction(nameof(FarmerDatabase));
                }

                TempData["ErrorMessage"] = "Error registering farmer. Please check the information and try again.";
                return RedirectToAction(nameof(FarmerDatabase));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction(nameof(FarmerDatabase));
            }
        }


    }
}
