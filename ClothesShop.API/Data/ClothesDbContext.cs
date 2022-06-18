using ClotheShop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ClotheShop.API.Data
{
    public class ClothesDbContext : DbContext
    {
        public ClothesDbContext(DbContextOptions<ClothesDbContext> options) : base (options) { }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Rating> Rating { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=.;Initial Catalog=ClotheShop;Integrated Security=True")
                .UseLazyLoadingProxies()
                .LogTo(Console.WriteLine, new[]
                {
                    DbLoggerCategory.Model.Name,
                    DbLoggerCategory.Database.Command.Name,
                    DbLoggerCategory.Database.Transaction.Name,
                    DbLoggerCategory.Query.Name,
                    DbLoggerCategory.ChangeTracking.Name,
                },
                    LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
    }
}
