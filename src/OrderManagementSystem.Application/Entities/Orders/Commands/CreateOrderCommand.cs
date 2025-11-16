using MediatR;
using OrderManagementSystem.Application.DTOs;

namespace OrderManagementSystem.Application.Entities.Orders.Commands
{
    public record CreateOrderCommand(List<CreateOrderItemDto> Items) : IRequest<OrderSummaryDto>;
}
