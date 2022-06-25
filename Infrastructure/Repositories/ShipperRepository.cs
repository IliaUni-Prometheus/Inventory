using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly NorthwindContext _db;

        // Constructor
        public ShipperRepository(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Shipper>> RetrieveAllAsync()
        {
            return await _db.Shippers.ToListAsync();
        }
    }
}
