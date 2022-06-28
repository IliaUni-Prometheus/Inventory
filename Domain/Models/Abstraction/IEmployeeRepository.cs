using Infrastructure.Models;

namespace Domain.Models.Abstraction
{
    public interface IEmployeeRepository
    {
        Task<Employee?> CreateAsync(Employee employee);
        Task<IEnumerable<Employee>> RetrieveAllAsync(int page, int itemsPerPage);
        Task<Employee?> RetrieveByIdAsync(int id);
        Task<bool> UpdateAsync(Employee employee);
        Task<bool?> DeleteByIdAsync(int id);
        Task<int> Count();
    }
}
