using OrderManagementSystem.Application.Entities.Orders.Commands;

namespace OrderManagementSystem.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);

        Task<int> GetTodayOrderCountAsync();

        Task<decimal> GetTodayRevenueAsync();
    }
}
