using FluentValidation.TestHelper;
using OrderManagementSystem.Application.Entities.Products.Commands;

namespace OrderManagementSystem.Application.Tests.Entities.Products.Commands
{
    public class CreateProductValidatorTests
    {
        private readonly CreateProductValidator _validator;

        public CreateProductValidatorTests()
        {
            _validator = new CreateProductValidator();
        }

        [Fact]
        public void Name_Should_Have_Error_When_Empty()
        {
            var model = new CreateProductCommand("", 10, 5);

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Price_Should_Have_Error_When_NonPositive()
        {
            var model = new CreateProductCommand("Test", 0, 5);

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void Stock_Should_Have_Error_When_Negative()
        {
            var model = new CreateProductCommand("Test", 9, -1);

            var result = _validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.StockQuantity);
        }

        [Fact]
        public void Valid_Model_Should_Pass_Validation()
        {
            var model = new CreateProductCommand("Valid Product", 20, 10);

            var result = _validator.TestValidate(model);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
