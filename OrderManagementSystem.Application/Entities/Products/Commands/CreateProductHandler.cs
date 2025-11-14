using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;

namespace OrderManagementSystem.Application.Entities.Products.Commands
{
    public class CreateProductHandler(IProductRepository repo) : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly IProductRepository _repo = repo;

        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                StockQuantity = request.StockQuantity
            };

            await _repo.AddAsync(product);

            return new ProductDto(product.Id, product.Name, product.Price, product.StockQuantity);
        }
    }
}
