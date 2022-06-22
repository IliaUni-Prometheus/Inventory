using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NorthwindContext _db;

        // Constructor
        public RegionRepository(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Region>> RetrieveAllAsync()
        {
            return await _db.Regions.ToListAsync();
        }
    }
}
