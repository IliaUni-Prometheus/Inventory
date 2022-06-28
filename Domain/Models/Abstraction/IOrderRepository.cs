using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface IOrderRepository
    {
        Task<Order?> CreateAsync(Order product);
        Task<IEnumerable<Order>> RetrieveAllAsync(int page, int itemsPerPage);
        Task<Order?> RetrieveByIdAsync(int id);
        Task<bool> UpdateAsync(Order product);
        Task<bool?> DeleteByIdAsync(int id);
        Task<int> Count();
    }
}
