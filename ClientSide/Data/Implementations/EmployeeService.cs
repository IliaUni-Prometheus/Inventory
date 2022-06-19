using ClientSide.Models;
using Flurl.Http;

namespace ClientSide.Data.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<List<EmployeeViewModel>> All()
        {
            return await $"https://localhost:7045/api/Employee".GetJsonAsync<List<EmployeeViewModel>>();
        }
    }
}
