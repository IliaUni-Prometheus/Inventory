using Domain.Models.Abstraction;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly NorthwindContext _context;

    public ProductRepository(NorthwindContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var products = await _context.Products.ToListAsync();
        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        return product;
    }

    public async Task<int> InsertAsync(Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product.ProductId;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        _context.Products.Remove(product!);
        await _context.SaveChangesAsync();
    }
    
    
}