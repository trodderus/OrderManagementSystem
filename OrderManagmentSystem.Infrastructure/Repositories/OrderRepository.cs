using Microsoft.EntityFrameworkCore;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagementSystem.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;

        public OrderRepository(AppDbContext db) => _db = db;

        public async Task AddAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }

        public Task<int> GetTodayOrderCountAsync()
        {
            var today = DateTime.UtcNow.Date;
            return _db.Orders.CountAsync(o => o.CreatedAt >= today);
        }

        public Task<decimal> GetTodayRevenueAsync()
        {
            var today = DateTime.UtcNow.Date;
            return _db.Orders
                .Where(o => o.CreatedAt >= today)
                .SelectMany(o => o.Items)
                .SumAsync(i => i.Price * i.Quantity);
        }
    }
}
