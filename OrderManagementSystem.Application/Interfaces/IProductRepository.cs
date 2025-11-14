using OrderManagementSystem.Application.Entities.Products.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);

        Task<List<Product>> GetAllAsync();

        Task AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task<IEnumerable<Product>> GetLowStockAsync(int threshold);
    }
}
