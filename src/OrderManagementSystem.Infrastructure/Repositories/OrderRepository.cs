using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Infrastructure.Persistence;

namespace OrderManagementSystem.Infrastructure.Repositories
{
    public class OrderRepository(AppDbContext db, TimeProvider timeProvider) : IOrderRepository
    {
        private readonly AppDbContext _db = db;
        private readonly TimeProvider _timeProvider = timeProvider;

        public async Task AddAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }

        public Task<int> GetTodayOrderCountAsync()
        {
            var today = _timeProvider.GetUtcNow().Date;
            return _db.Orders
                .Where(o => o.CreatedAt >= today)
                .CountAsync();
        }

        public async Task<decimal> GetTodayRevenueAsync()
        {
            var today = _timeProvider.GetUtcNow().Date;
            
            var items = await _db.Orders
                .Where(o => o.CreatedAt >= today)
                .SelectMany(o => o.Items)
                .ToListAsync();
            return items.Sum(i => i.Price * i.Quantity);
        }
    }
}
