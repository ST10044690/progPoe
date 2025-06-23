using Agri_Energy_Connect.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Agri_Energy_Connect.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Farmer> Farmers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Farmer>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Farmer>()
                .Property(f => f.UserId)
                .HasColumnName("UserId");

            // Seed Categories
            modelBuilder.Entity<Category>().HasData(
               new Category { Id = 1, Name = "Fruits", Description = "Fresh fruits from the farm" },
               new Category { Id = 2, Name = "Vegetables", Description = "Fresh vegetables from the farm" },
               new Category { Id = 3, Name = "Dairy", Description = "Milk and dairy products" },
               new Category { Id = 4, Name = "Grains", Description = "Wheat, rice, and other grains" },
               new Category { Id = 5, Name = "Meat", Description = "Fresh farm meats" },
               new Category { Id = 6, Name = "Herbs & Spices", Description = "Fresh and dried herbs and spices" },
               new Category { Id = 7, Name = "Honey & Preserves", Description = "Honey, jams, and other preserves" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    Password = "Password123", // In production, use password hashing
                    Role = "farmer"
                },
                new User
                {
                    Id = 2,
                    Name = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    Password = "Password123",
                    Role = "farmer"
                },
                new User
                {
                    Id = 3,
                    Name = "Admin",
                    LastName = "User",
                    Email = "admin@example.com",
                    Password = "Admin123",
                    Role = "employee"
                }
            );

            // Seed Farmers
            modelBuilder.Entity<Farmer>().HasData(
                new Farmer
                {
                    Id = 1,
                    UserId = 1, // Related to John Doe
                    FarmName = "Green Valley Farm",
                    Address = "123 Rural Road, Countryside",
                    PhoneNumber = "555-1234",
                    Email = "john.doe@example.com",
                    Description = "Family-owned organic farm specializing in seasonal vegetables.",
                    JoinDate = new DateTime(2024, 1, 15)
                },
                new Farmer
                {
                    Id = 2,
                    UserId = 2, // Related to Jane Smith
                    FarmName = "Smith's Orchard",
                    Address = "456 Orchard Lane, Fruitville",
                    PhoneNumber = "555-5678",
                    Email = "jane.smith@example.com",
                    Description = "Specializing in apples, pears, and stone fruits.",
                    JoinDate = new DateTime(2024, 2, 20)
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Organic Tomatoes",
                    CategoryId = 2, // Vegetables
                    FarmerId = 1, // Green Valley Farm
                    ProductionDate = new DateTime(2025, 5, 4), // 10 days ago
                    Description = "Vine-ripened organic tomatoes, perfect for salads and sauces.",
                    Quantity = 50,
                    QuantityUnit = "kg",
                    Price = 3.99m
                },
                new Product
                {
                    Id = 2,
                    Name = "Fresh Basil",
                    CategoryId = 6, // Herbs & Spices
                    FarmerId = 1, // Green Valley Farm
                    ProductionDate = new DateTime(2025, 5, 9), // 5 days ago
                    Description = "Fragrant organic basil, freshly harvested.",
                    Quantity = 20,
                    QuantityUnit = "bunch",
                    Price = 2.50m
                },
                new Product
                {
                    Id = 3,
                    Name = "Gala Apples",
                    CategoryId = 1, // Fruits
                    FarmerId = 2, // Smith's Orchard
                    ProductionDate = new DateTime(2025, 4, 29), // 15 days ago
                    Description = "Sweet and crisp Gala apples, picked at peak ripeness.",
                    Quantity = 100,
                    QuantityUnit = "kg",
                    Price = 2.99m
                },
                new Product
                {
                    Id = 4,
                    Name = "Bartlett Pears",
                    CategoryId = 1, // Fruits
                    FarmerId = 2, // Smith's Orchard
                    ProductionDate = new DateTime(2025, 5, 2), // 12 days ago
                    Description = "Juicy and sweet Bartlett pears.",
                    Quantity = 75,
                    QuantityUnit = "kg",
                    Price = 3.50m
                }
            );
        }

    }
}
