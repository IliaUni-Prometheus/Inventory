using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface IProductRepository
    {
        Task<Product?> CreateAsync(Product product);
        Task<IEnumerable<Product>> RetrieveAllAsync();
        Task<Product?> RetrieveByIdAsync(int id);
        Task<bool> UpdateAsync(Product product);
        Task<bool?> DeleteByIdAsync(int id);
    }
}
