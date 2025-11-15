using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Entities.Orders.Commands;

namespace OrderManagementSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand cmd) =>
            Ok(await _mediator.Send(cmd));
    }
}
