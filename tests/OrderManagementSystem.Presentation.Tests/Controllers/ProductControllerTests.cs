using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Entities.Products.Commands;
using OrderManagementSystem.Domain.Entities;
using Shouldly;
using System.Net;
using System.Net.Http.Json;

namespace OrderManagementSystem.Presentation.Tests.Controllers
{
    public class ProductControllerTests(TestWebApplicationFactory factory) : IClassFixture<TestWebApplicationFactory>
    {
        private readonly HttpClient _client = factory.CreateClient();

        [Fact]
        public async Task CreateProduct_ShouldReturnCreatedProduct()
        {
            var request = new CreateProductCommand("Integration Test Product", 20.5m, 10);

            var response = await _client.PostAsJsonAsync("/api/products", request);

            response.StatusCode.ShouldBe(HttpStatusCode.Created);

            var product = await response.Content.ReadFromJsonAsync<Product>();

            product.ShouldNotBeNull();
            product.Name.ShouldBe("Integration Test Product");
        }

        [Fact]
        public async Task CreateProduct_Should_Return_400_When_Invalid()
        {
            var request = new CreateProductCommand("", -10, -1);

            var response = await _client.PostAsJsonAsync("/api/products", request);

            response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);

            var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

            problem.ShouldNotBeNull();
            problem.Errors.ShouldContainKey("Name");
            problem.Errors.ShouldContainKey("Price");
            problem.Errors.ShouldContainKey("StockQuantity");
        }
    }
}
