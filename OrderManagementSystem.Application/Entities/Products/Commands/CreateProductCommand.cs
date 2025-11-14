using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Entities.Products.Commands
{
    public record CreateProductCommand(string Name, decimal Price, int StockQuantity) : IRequest<ProductDto>;
}
