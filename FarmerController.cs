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
    public class FarmerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FarmerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Products()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.FarmerId == GetCurrentFarmerId())
                .ToListAsync();


            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            var farmerId = GetCurrentFarmerId();
            if (farmerId == -1)
            {
                TempData["ErrorMessage"] = "Please log in as a farmer to add products.";
                return RedirectToAction("Login", "Account");  // Adjust this to your login path
            }

            if (ModelState.IsValid)
            {
                try
                {
                    product.FarmerId = farmerId;
                    product.Description = product.Description ?? string.Empty;

                    _context.Products.Add(product);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Product added successfully!";
                    return RedirectToAction(nameof(Products));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error saving product: {ex.Message}");
                }
            }

            // Stay on the Products view
            ViewBag.Categories = await _context.Categories.ToListAsync();
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.FarmerId == farmerId)
                .ToListAsync();

            return View(nameof(Products), products);
        }


        [HttpGet]
        public IActionResult Dashboard()
        {
            var products = _context.Products
                .Include(p => p.Category)
                .Where(p => p.FarmerId == GetCurrentFarmerId())
                .ToList();

            ViewBag.Categories = _context.Categories.ToList();

            var viewModel = new DashboardViewModel
            {
                Products = products
            };

            return View(viewModel);
        }

        private int GetCurrentFarmerId()
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                TempData["ErrorMessage"] = "Please log in to continue.";
                return -1;
            }

            var farmer = _context.Farmers.FirstOrDefault(f => f.UserId == userId);

            if (farmer == null)
            {
                TempData["ErrorMessage"] = "Only farmers can add products.";
                return -1;
            }

            return farmer.Id;
        }


    }
}
