using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Infrastructure.Persistence;

namespace OrderManagementSystem.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext db) : IProductRepository
    {
        private readonly AppDbContext _db = db;

        public Task<List<Product>> GetAllAsync() => _db.Products.ToListAsync();

        public Task<Product?> GetByIdAsync(int id) => _db.Products.FindAsync(id).AsTask();

        public async Task AddAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            await _db.SaveChangesAsync();
        }

        public Task<List<Product>> GetLowStockAsync(int threshold) =>
            _db.Products.Where(p => p.StockQuantity < threshold).ToListAsync();
    }
}
