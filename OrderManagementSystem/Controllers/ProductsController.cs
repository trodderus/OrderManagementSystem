using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Entities.Products.Commands;
using OrderManagementSystem.Application.Entities.Products.Queries;

namespace OrderManagementSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            Ok(await _mediator.Send(new GetAllProductsQuery()));

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand cmd) =>
            Ok(await _mediator.Send(cmd));
    }
}
