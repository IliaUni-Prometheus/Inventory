using Shared.DTOs;

namespace ClientSide.Services.EmployeeService
{
    public interface IEmployeeService
    {
        Task<BrowseResult<EmployeeDTO>> GetEmployees(int page);
        Task<EmployeeDTO> GetEmployeeById(int id);
    }
}
