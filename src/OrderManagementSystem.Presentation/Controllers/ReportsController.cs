using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Application.Entities.Reports.Queries;

namespace OrderManagementSystem.Presentation.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportsController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("daily-summary")]
        public async Task<IActionResult> DailySummary() =>
            Ok(await _mediator.Send(new GetDailySummaryQuery()));

        [HttpGet("low-stock")]
        public async Task<IActionResult> LowStock([FromQuery] int limit = 5) =>
            Ok(await _mediator.Send(new GetLowStockQuery(limit)));
    }
}
