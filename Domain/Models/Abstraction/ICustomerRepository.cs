using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> RetrieveAllAsync();
    }
}
