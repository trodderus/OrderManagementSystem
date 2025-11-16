using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Application.Entities.Orders.Commands;
using OrderManagementSystem.Application.Entities.Products.Commands;
using OrderManagementSystem.Domain.Entities;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace OrderManagementSystem.Presentation.Tests.Controllers
{
    public class ReportControllerTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task LowStock_ShouldReturnProductsBelowLimit()
        {
            // Add 3 products
            await _client.PostAsJsonAsync("/api/products", new CreateProductCommand("A", 10, 2));
            await _client.PostAsJsonAsync("/api/products", new CreateProductCommand("B", 10, 10));
            await _client.PostAsJsonAsync("/api/products", new CreateProductCommand("C", 10, 4));

            var response = await _client.GetAsync("/api/reports/low-stock?limit=5");

            response.StatusCode.ShouldBe(HttpStatusCode.OK);

            var result = await response.Content.ReadFromJsonAsync<List<ProductDto>>();

            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
        }
    }
}
