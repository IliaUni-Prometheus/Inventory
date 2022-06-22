using ClientSide.Models;

namespace ClientSide.Data
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> All();
    }
}
