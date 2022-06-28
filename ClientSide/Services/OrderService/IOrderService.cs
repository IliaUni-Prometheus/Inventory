using Shared.DTOs;

namespace ClientSide.Services.OrderService
{
    public interface IOrderService
    {
        Task<BrowseResult<OrderDTO>> GetOrders(int page, int pageSize);
        Task<OrderDTO> GetOrderById(int id);
    }
}
