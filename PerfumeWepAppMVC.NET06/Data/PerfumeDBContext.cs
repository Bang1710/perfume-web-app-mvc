using Microsoft.EntityFrameworkCore;
using PerfumeWepAppMVC.NET06.Models;

namespace PerfumeWebApp.NET06.Data
{
    public class PerfumeDBContext : DbContext
    {
        public PerfumeDBContext(DbContextOptions<PerfumeDBContext> options) :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductSpec> ProductSpecs { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ReviewProduct> ReviewProducts { get; set; }


    }
}
