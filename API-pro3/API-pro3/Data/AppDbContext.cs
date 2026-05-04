using API_pro3.Models;
using Microsoft.EntityFrameworkCore;

namespace API_pro3.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; } //using for crud operations
        public DbSet<Product> Products { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
