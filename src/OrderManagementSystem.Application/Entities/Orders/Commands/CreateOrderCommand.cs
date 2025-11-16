using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Application.Entities.Orders.Commands
{
    public record CreateOrderCommand(List<OrderItem> Items) : IRequest<OrderSummaryDto>;
}
