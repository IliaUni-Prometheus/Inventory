using ClientSide.Models;

namespace ClientSide.Data
{
    public interface ICustomerService
    {
        Task<List<CustomerViewModel>> All();
    }
}
