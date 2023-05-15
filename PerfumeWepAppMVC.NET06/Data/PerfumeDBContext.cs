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


    }
}
