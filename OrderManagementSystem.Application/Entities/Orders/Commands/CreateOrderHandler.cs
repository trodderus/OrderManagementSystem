using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Application.Entities.Orders.Commands
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, OrderSummaryDto>
    {
        private readonly IProductRepository _products;
        private readonly IOrderRepository _orders;

        public CreateOrderHandler(IProductRepository p, IOrderRepository o)
        {
            _products = p;
            _orders = o;
        }

        public async Task<OrderSummaryDto> Handle(CreateOrderCommand request, CancellationToken ct)
        {
            var order = new Order();

            var summaryItems = new List<OrderSummaryItem>();

            foreach (var item in request.Items)
            {
                var product = await _products.GetByIdAsync(item.ProductId)
                    ?? throw new Exception($"Product {item.ProductId} not found");

                if (product.StockQuantity < item.Quantity)
                    throw new Exception($"Not enough stock for product {product.Name}");

                product.StockQuantity -= item.Quantity;
                await _products.UpdateAsync(product);

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price
                });

                summaryItems.Add(new OrderSummaryItem(
                    product.Name,
                    item.Quantity,
                    product.Price
                ));
            }

            await _orders.AddAsync(order);

            return new OrderSummaryDto(
                order.Id,
                summaryItems.Sum(i => i.Quantity * i.Price),
                order.CreatedAt,
                summaryItems
            );
        }
    }
}
