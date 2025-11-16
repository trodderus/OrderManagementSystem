using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Application.Entities.Orders.Commands;
using OrderManagementSystem.Application.Entities.Products.Commands;
using OrderManagementSystem.Domain.Entities;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace OrderManagementSystem.Presentation.Tests.Controllers
{
    public class OrderControllerTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task CreateOrder_ShouldDeductStockAndReturnSummary()
        {
            // 1. Create a product first
            var productReq = new CreateProductCommand("OrderTest Product", 15m, 5);

            var createdProductResponse = await _client.PostAsJsonAsync("/api/products", productReq);

            var product = await createdProductResponse.Content.ReadFromJsonAsync<Product>();
            _ = product.ShouldNotBeNull();
            var productId = product.Id;

            // 2. Submit order
            var orderReq = new CreateOrderCommand(
                new()
                {
                    new(productId, 2),
                });

            var orderResponse = await _client.PostAsJsonAsync("/api/orders", orderReq);

            orderResponse.StatusCode.ShouldBe(HttpStatusCode.Created);

            var summary = await orderResponse.Content.ReadFromJsonAsync<OrderSummaryDto>();

            summary.ShouldNotBeNull();
            summary.Total.ShouldBe(30m); // 2 × 15
            var item = summary.Items.ShouldHaveSingleItem();
            item.Quantity.ShouldBe(2);
        }

        [Fact]
        public async Task CreateOrder_Should_Return_400_When_Invalid()
        {
            var request = new CreateOrderCommand(
                new()
                {
                    new(-1, -2),
                });

            var response = await _client.PostAsJsonAsync("/api/orders", request);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            problem.ShouldNotBeNull();
            problem.Errors.ShouldContainKey("Items[0].ProductId");
            problem.Errors.ShouldContainKey("Items[0].Quantity");
        }
    }
}
