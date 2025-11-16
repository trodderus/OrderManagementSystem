using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Entities.Products.Commands;
using OrderManagementSystem.Application.Entities.Products.Queries;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderManagementSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController(IMediator mediator, IValidator<CreateProductCommand> validator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IValidator<CreateProductCommand> _validator = validator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllProductsQuery()));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand cmd)
        {
            var validationResult = await _validator.ValidateAsync(cmd);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return ValidationProblem(ModelState);
            }

            return Created(string.Empty, await _mediator.Send(cmd));
        }
    }
}
