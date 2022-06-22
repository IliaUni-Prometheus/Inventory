using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> RetrieveAllAsync();
    }
}
