using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly NorthwindContext _db;

        // Constructor for injection
        public CategoryRepository(NorthwindContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Category>> RetrieveAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }
    }
}
