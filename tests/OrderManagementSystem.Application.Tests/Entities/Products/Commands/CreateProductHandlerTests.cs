using Moq;
using OrderManagementSystem.Application.Entities.Products.Commands;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;
using Shouldly;

namespace OrderManagementSystem.Application.Tests.Entities.Products.Commands
{
    public class CreateProductHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ShouldAddProduct()
        {
            // Arrange
            var productRepoMock = new Mock<IProductRepository>();
            var handler = new CreateProductHandler(productRepoMock.Object);

            var command = new CreateProductCommand("Test Product", 10.5m, 20);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Name.ShouldBe("Test Product");
            productRepoMock.Verify(r => r.AddAsync(It.IsAny<Product>()), Times.Once);
        }
    }
}
