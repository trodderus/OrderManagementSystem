using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);

        Task<List<Product>> GetAllAsync();

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task<List<Product>> GetLowStockAsync(int threshold);
    }
}
