using ClientSide.Models;

namespace ClientSide.Data
{
    public interface IOrderService
    {
        Task<PaginatedResultViewModel<OrderViewModel>> All(int page);
    }
}
