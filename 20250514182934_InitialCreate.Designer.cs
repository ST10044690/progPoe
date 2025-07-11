﻿// <auto-generated />
using System;
using Agri_Energy_Connect.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Agri_Energy_Connect.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250514182934_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("Agri_Energy_Connect.Models.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Fresh fruits from the farm",
                            Name = "Fruits"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Fresh vegetables from the farm",
                            Name = "Vegetables"
                        },
                        new
                        {
                            Id = 3,
                            Description = "Milk and dairy products",
                            Name = "Dairy"
                        },
                        new
                        {
                            Id = 4,
                            Description = "Wheat, rice, and other grains",
                            Name = "Grains"
                        },
                        new
                        {
                            Id = 5,
                            Description = "Fresh farm meats",
                            Name = "Meat"
                        },
                        new
                        {
                            Id = 6,
                            Description = "Fresh and dried herbs and spices",
                            Name = "Herbs & Spices"
                        },
                        new
                        {
                            Id = 7,
                            Description = "Honey, jams, and other preserves",
                            Name = "Honey & Preserves"
                        });
                });

            modelBuilder.Entity("Agri_Energy_Connect.Models.Entities.Farmer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FarmName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER")
                        .HasColumnName("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Farmers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "123 Rural Road, Countryside",
                            Description = "Family-owned organic farm specializing in seasonal vegetables.",
                            Email = "john.doe@example.com",
                            FarmName = "Green Valley Farm",
                            JoinDate = new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "555-1234",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Address = "456 Orchard Lane, Fruitville",
                            Description = "Specializing in apples, pears, and stone fruits.",
                            Email = "jane.smith@example.com",
                            FarmName = "Smith's Orchard",
                            JoinDate = new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PhoneNumber = "555-5678",
                            UserId = 2
                        });
                });

            modelBuilder.Entity("Agri_Energy_Connect.Models.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("FarmerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ProductionDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("TEXT");

                    b.Property<string>("QuantityUnit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FarmerId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 2,
                            Description = "Vine-ripened organic tomatoes, perfect for salads and sauces.",
                            FarmerId = 1,
                            Name = "Organic Tomatoes",
                            Price = 3.99m,
                            ProductionDate = new DateTime(2025, 5, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Quantity = 50m,
                            QuantityUnit = "kg"
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 6,
                            Description = "Fragrant organic basil, freshly harvested.",
                            FarmerId = 1,
                            Name = "Fresh Basil",
                            Price = 2.50m,
                            ProductionDate = new DateTime(2025, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Quantity = 20m,
                            QuantityUnit = "bunch"
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 1,
                            Description = "Sweet and crisp Gala apples, picked at peak ripeness.",
                            FarmerId = 2,
                            Name = "Gala Apples",
                            Price = 2.99m,
                            ProductionDate = new DateTime(2025, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Quantity = 100m,
                            QuantityUnit = "kg"
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 1,
                            Description = "Juicy and sweet Bartlett pears.",
                            FarmerId = 2,
                            Name = "Bartlett Pears",
                            Price = 3.50m,
                            ProductionDate = new DateTime(2025, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Quantity = 75m,
                            QuantityUnit = "kg"
                        });
                });

            modelBuilder.Entity("Agri_Energy_Connect.Models.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "john.doe@example.com",
                            LastName = "Doe",
                            Name = "John",
                            Password = "Password123",
                            Role = "farmer"
                        },
                        new
                        {
                            Id = 2,
                            Email = "jane.smith@example.com",
                            LastName = "Smith",
                            Name = "Jane",
                            Password = "Password123",
                            Role = "farmer"
                        },
                        new
                        {
                            Id = 3,
                            Email = "admin@example.com",
                            LastName = "User",
                            Name = "Admin",
                            Password = "Admin123",
                            Role = "employee"
                        });
                });

            modelBuilder.Entity("Agri_Energy_Connect.Models.Entities.Farmer", b =>
                {
                    b.HasOne("Agri_Energy_Connect.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Agri_Energy_Connect.Models.Entities.Product", b =>
                {
                    b.HasOne("Agri_Energy_Connect.Models.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Agri_Energy_Connect.Models.Entities.Farmer", "Farmer")
                        .WithMany("Products")
                        .HasForeignKey("FarmerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Farmer");
                });

            modelBuilder.Entity("Agri_Energy_Connect.Models.Entities.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Agri_Energy_Connect.Models.Entities.Farmer", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
