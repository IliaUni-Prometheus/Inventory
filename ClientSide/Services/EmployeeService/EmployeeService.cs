using Flurl.Http;
using Shared.DTOs;

namespace ClientSide.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<BrowseResult<EmployeeDTO>> GetEmployees(int page)
        {
            return await $"https://localhost:7045/api/Employee?page={page}".GetJsonAsync<BrowseResult<EmployeeDTO>>();
        }
        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
