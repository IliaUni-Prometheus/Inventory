using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface IShipperRepository
    {
        Task<IEnumerable<Shipper>> RetrieveAllAsync();
    }
}
