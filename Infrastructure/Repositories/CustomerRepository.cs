using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly NorthwindContext _db;

        // Constructor 
        public CustomerRepository(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Customer>> RetrieveAllAsync()
        {
            return await _db.Customers.ToListAsync();
        }
    }
}
