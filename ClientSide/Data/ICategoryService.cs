using ClientSide.Models;

namespace ClientSide.Data
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> All();
    }
}
