﻿using ClothesShop.API.Models;
using ClothesShop.API.Models.Authenticate;
using Microsoft.EntityFrameworkCore;

namespace ClothesShop.API.Data
{
    public class ClothesDbContext : DbContext
    {
        public ClothesDbContext(DbContextOptions<ClothesDbContext> options) : base (options) { }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=.;Initial Catalog=ClothesShop;Integrated Security=True")
                .UseLazyLoadingProxies();
                /*.LogTo(Console.WriteLine, new[]
                {
                    DbLoggerCategory.Model.Name,
                    DbLoggerCategory.Database.Command.Name,
                    DbLoggerCategory.Database.Transaction.Name,
                    DbLoggerCategory.Query.Name,
                    DbLoggerCategory.ChangeTracking.Name,
                },
                    LogLevel.Information)
                .EnableSensitiveDataLogging(); */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Image>()
            //    .HasIndex(image => image.URL)
            //    .IsUnique();
        }
    }
}
