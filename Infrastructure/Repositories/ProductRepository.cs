using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly NorthwindContext _db;

        // Constructor for injection
        public ProductRepository(NorthwindContext db) { _db = db; }

        // Interface implementation
        public async Task<IEnumerable<Product>> RetrieveAllAsync()
        {
            return await _db.Products.OrderBy(p => p.ProductName).ToListAsync();
        }
        public async Task<Product?> RetrieveByIdAsync(int id)
        {
            return await _db.Products.Where(p => p.ProductId == id).FirstOrDefaultAsync();
        }
        public async Task<Product?> CreateAsync(Product product)
        {
            await _db.Products.AddAsync(product);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1) { return product; }

            return null;
        }
        public async Task<bool> UpdateAsync(Product product)
        {
            _db.Products.Update(product);
            int affected = await _db.SaveChangesAsync();
            return affected == 1;
        }
        public async Task<bool?> DeleteByIdAsync(int id)
        {
            Product? product = await _db.Products.FindAsync(id);
            // Product wasn't found at all
            if (product == null) { return null; }

            // Deleted successfuly
            _db.Products.Remove(product);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1) { return true; }

            // Product was found, but it couldn't be deleted
            return false;
        }
    }
}
