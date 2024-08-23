using GamerShop.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace GamerShop.Core.Repositories
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=gamer_shop_db;Integrated Security=True;TrustServerCertificate=true;");
        }

    }
}
