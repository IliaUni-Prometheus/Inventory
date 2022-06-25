using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface ITerritoryRepository
    {
        Task<IEnumerable<Territory>> RetrieveAllAsync();
    }
}
