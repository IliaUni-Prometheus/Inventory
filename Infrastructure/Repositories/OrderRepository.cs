using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly NorthwindContext _db;

        // Constructor for injection
        public OrderRepository(NorthwindContext db) { _db = db; }

        public async Task<IEnumerable<Order>> RetrieveAllAsync(int page, int itemsPerPage)
        {
            return await _db.Orders
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
        }
        public async Task<Order?> RetrieveByIdAsync(int id)
        {
            return await _db.Orders.Where(o => o.OrderId == id).FirstOrDefaultAsync();
        }
        public async Task<Order?> CreateAsync(Order order)
        {
            await _db.Orders.AddAsync(order);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1) { return order; }

            return null;
        }
        public async Task<bool> UpdateAsync(Order order)
        {
            _db.Orders.Update(order);
            int affected = await _db.SaveChangesAsync();
            return affected == 1;
        }
        public async Task<bool?> DeleteByIdAsync(int id)
        {
            Order? order = await _db.Orders.FindAsync(id);
            // Order wasn't found at all
            if (order == null) { return null; }

            // Deleted successfuly
            _db.Orders.Remove(order);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1) { return true; }

            // Order was found, but it couldn't be deleted
            return false;
        }
        public async Task<int> Count()
        {
            return await _db.Orders.CountAsync();
        }
    }
}
