using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KASHOP12.DAL.Data;
using KASHOP12.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace KASHOP12.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order Request)
        {
            await _context.AddAsync(Request);
            await _context.SaveChangesAsync();
            return Request;
        }

        public async Task<Order> GetBySessionIdAsync(string sessionId)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.SessionId == sessionId);
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
