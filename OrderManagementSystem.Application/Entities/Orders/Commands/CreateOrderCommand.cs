using MediatR;
using OrderManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.Entities.Orders.Commands
{
    public record CreateOrderCommand(List<OrderItem> Items) : IRequest<OrderSummaryDto>;
}
