using MediatR;
using OrderManagementSystem.Application.DTOs;
using OrderManagementSystem.Application.Interfaces;

namespace OrderManagementSystem.Application.Entities.Reports.Queries
{
    internal class GetDailySummaryHandler(IOrderRepository orders) : IRequestHandler<GetDailySummaryQuery, DailySummaryDto>
    {
        private readonly IOrderRepository _orders = orders;

        public async Task<DailySummaryDto> Handle(GetDailySummaryQuery request, CancellationToken ct)
        {
            var count = await _orders.GetTodayOrderCountAsync();
            var revenue = await _orders.GetTodayRevenueAsync();

            return new DailySummaryDto(count, revenue);
        }
    }
}
