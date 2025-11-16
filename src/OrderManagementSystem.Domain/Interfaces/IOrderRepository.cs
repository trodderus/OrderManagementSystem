using OrderManagementSystem.Domain.Entities;

namespace OrderManagementSystem.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);

        Task<int> GetTodayOrderCountAsync();

        Task<decimal> GetTodayRevenueAsync();
    }
}
