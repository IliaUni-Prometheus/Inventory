using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TerritoryRepository : ITerritoryRepository
    {
        private readonly NorthwindContext _db;

        // Constructor
        public TerritoryRepository(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Territory>> RetrieveAllAsync()
        {
            return await _db.Territories.ToListAsync();
        }
    }
}
