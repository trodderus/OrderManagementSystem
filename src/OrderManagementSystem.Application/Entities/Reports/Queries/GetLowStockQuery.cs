using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Entities.Reports.Queries
{
    public record GetLowStockQuery(int Limit) : IRequest<List<ProductDto>>;
}
