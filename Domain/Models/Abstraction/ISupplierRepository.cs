using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> RetrieveAllAsync();
    }
}
