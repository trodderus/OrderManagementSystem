using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Entities.Orders.Commands;

namespace OrderManagementSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController(IMediator mediator, IValidator<CreateOrderCommand> validator) : Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IValidator<CreateOrderCommand> _validator = validator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand cmd)
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
