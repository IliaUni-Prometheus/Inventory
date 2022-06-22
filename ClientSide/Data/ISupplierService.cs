using ClientSide.Models;

namespace ClientSide.Data
{
    public interface ISupplierService
    {
        Task<List<SupplierViewModel>> All();
    }
}
