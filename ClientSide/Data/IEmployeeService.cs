using ClientSide.Models;

namespace ClientSide.Data
{
    public interface IEmployeeService
    {
        Task<List<EmployeeViewModel>> All();
    }
}
