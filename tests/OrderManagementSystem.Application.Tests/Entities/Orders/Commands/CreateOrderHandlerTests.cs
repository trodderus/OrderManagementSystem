using Moq;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Application.Entities.Orders.Commands;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;
using Shouldly;

namespace OrderManagementSystem.Application.Tests.Entities.Orders.Commands
{
    public class CreateOrderHandlerTests
    {
        [Fact]
        public async Task Handle_StockAvailable_ShouldDeductStock()
        {
            // Arrange
            var products = new List<Product>
            {
                new() { Id = 1, Name = "Prod1", Price = 10, StockQuantity = 5 }
            };

            var productRepoMock = new Mock<IProductRepository>();
            productRepoMock.Setup(r => r.GetByIdAsync(1))
                .ReturnsAsync(products[0]);
            productRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            var handler = new CreateOrderHandler(productRepoMock.Object, Mock.Of<IOrderRepository>());

            var command = new CreateOrderCommand(new() { new(1, 2) });

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.Total.ShouldBe(20);
            products[0].StockQuantity.ShouldBe(3);
        }

        [Fact]
        public async Task Handle_InsufficientStock_ShouldThrowException()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Prod1", Price = 10, StockQuantity = 1 };
            var productRepoMock = new Mock<IProductRepository>();
            productRepoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            var handler = new CreateOrderHandler(productRepoMock.Object, Mock.Of<IOrderRepository>());

            var command = new CreateOrderCommand(new() { new(1, 2) });

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command, CancellationToken.None));
        }
    }
}
