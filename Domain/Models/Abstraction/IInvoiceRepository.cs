using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface IInvoiceRepository
    {
        Task<IEnumerable<Invoice>> RetrieveAllAsync();
    }
}
