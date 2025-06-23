using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Agri_Energy_Connect.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farmers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    FarmName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    JoinDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Farmers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    FarmerId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductionDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    QuantityUnit = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: "Farmers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Fresh fruits from the farm", "Fruits" },
                    { 2, "Fresh vegetables from the farm", "Vegetables" },
                    { 3, "Milk and dairy products", "Dairy" },
                    { 4, "Wheat, rice, and other grains", "Grains" },
                    { 5, "Fresh farm meats", "Meat" },
                    { 6, "Fresh and dried herbs and spices", "Herbs & Spices" },
                    { 7, "Honey, jams, and other preserves", "Honey & Preserves" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastName", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "Doe", "John", "Password123", "farmer" },
                    { 2, "jane.smith@example.com", "Smith", "Jane", "Password123", "farmer" },
                    { 3, "admin@example.com", "User", "Admin", "Admin123", "employee" }
                });

            migrationBuilder.InsertData(
                table: "Farmers",
                columns: new[] { "Id", "Address", "Description", "Email", "FarmName", "JoinDate", "PhoneNumber", "UserId" },
                values: new object[,]
                {
                    { 1, "123 Rural Road, Countryside", "Family-owned organic farm specializing in seasonal vegetables.", "john.doe@example.com", "Green Valley Farm", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "555-1234", 1 },
                    { 2, "456 Orchard Lane, Fruitville", "Specializing in apples, pears, and stone fruits.", "jane.smith@example.com", "Smith's Orchard", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "555-5678", 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "FarmerId", "Name", "Price", "ProductionDate", "Quantity", "QuantityUnit" },
                values: new object[,]
                {
                    { 1, 2, "Vine-ripened organic tomatoes, perfect for salads and sauces.", 1, "Organic Tomatoes", 3.99m, new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 50m, "kg" },
                    { 2, 6, "Fragrant organic basil, freshly harvested.", 1, "Fresh Basil", 2.50m, new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 20m, "bunch" },
                    { 3, 1, "Sweet and crisp Gala apples, picked at peak ripeness.", 2, "Gala Apples", 2.99m, new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m, "kg" },
                    { 4, 1, "Juicy and sweet Bartlett pears.", 2, "Bartlett Pears", 3.50m, new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 75m, "kg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Farmers_UserId",
                table: "Farmers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_FarmerId",
                table: "Products",
                column: "FarmerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Farmers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
