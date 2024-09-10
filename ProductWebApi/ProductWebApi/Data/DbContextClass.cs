using Microsoft.EntityFrameworkCore;
using ProductWebApi.Entities;

namespace ProductWebApi.Data
{
    public class DbContextClass : DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
