using OrderManagementSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.Services
{
    public interface IOrderService
    {
        Task<OrderSummaryDto> CreateOrderAsync(CreateOrderRequest request);
    }
}
