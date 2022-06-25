using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly NorthwindContext _db;

        // Constructor
        public SupplierRepository(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Supplier>> RetrieveAllAsync()
        {
            return await _db.Suppliers.ToListAsync();
        }
    }
}
