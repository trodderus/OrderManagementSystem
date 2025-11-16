using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Entities.Products.Queries
{
    public record GetAllProductsQuery() : IRequest<List<ProductDto>>;
}
