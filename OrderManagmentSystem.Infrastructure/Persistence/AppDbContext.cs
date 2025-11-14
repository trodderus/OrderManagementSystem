using Microsoft.EntityFrameworkCore;

namespace OrderManagementSystem.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
