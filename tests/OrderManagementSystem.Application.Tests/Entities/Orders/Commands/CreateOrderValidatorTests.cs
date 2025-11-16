using FluentValidation.TestHelper;
using OrderManagementSystem.Application.Entities.Orders.Commands;

namespace OrderManagementSystem.Application.Tests.Entities.Orders.Commands
{
    public class CreateOrderValidatorTests
    {
        private readonly CreateOrderValidator _validator;

        public CreateOrderValidatorTests()
        {
            _validator = new CreateOrderValidator();
        }

        [Fact]
        public void ProductId_Should_Have_Error_When_Empty()
        {
            var model = new CreateOrderCommand(new() { new() { Quantity = 2 } });

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor("Items[0].ProductId");
        }

        [Fact]
        public void ProductId_Should_Have_Error_When_NonPositive()
        {
            var model = new CreateOrderCommand(new() { new() { ProductId = 0, Quantity = 2 } });


            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor("Items[0].ProductId");
        }

        [Fact]
        public void ProductId_Should_Have_Error_When_Negative()
        {
            var model = new CreateOrderCommand(new() { new() { ProductId = -1, Quantity = 2 } });

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor("Items[0].ProductId");
        }

        [Fact]
        public void Quantity_Should_Have_Error_When_Empty()
        {
            var model = new CreateOrderCommand(new() { new() { ProductId = 2 } });

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor("Items[0].Quantity");
        }

        [Fact]
        public void Quantity_Should_Have_Error_When_NonPositive()
        {
            var model = new CreateOrderCommand(new() { new() { ProductId = 3, Quantity = 0 } });


            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor("Items[0].Quantity");
        }

        [Fact]
        public void Quantity_Should_Have_Error_When_Negative()
        {
            var model = new CreateOrderCommand(new() { new() { ProductId = 1, Quantity = -2 } });

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor("Items[0].Quantity");
        }

        [Fact]
        public void Valid_Model_Should_Pass_Validation()
        {
            var model = new CreateOrderCommand(new() { new() { ProductId = 3, Quantity = 2 } });

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
