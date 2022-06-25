using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface IRegionRepository
    {
        Task<IEnumerable<Region>> RetrieveAllAsync();
    }
}
