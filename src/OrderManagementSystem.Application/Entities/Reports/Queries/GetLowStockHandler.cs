using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Application.Entities.Reports.Queries
{
    public class GetLowStockHandler(IProductRepository productRepository) : IRequestHandler<GetLowStockQuery, List<ProductDto>>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<List<ProductDto>> Handle(GetLowStockQuery request, CancellationToken cancellationToken)
        {
            // Load products with stock < Limit
            var lowStockProducts = (await _productRepository
                .GetAllAsync())
                .Where(p => p.StockQuantity < request.Limit)
                .Select(p => new ProductDto(p.Id, p.Name, p.Price,p.StockQuantity))
                .ToList();

            return lowStockProducts;
        }
    }
}
