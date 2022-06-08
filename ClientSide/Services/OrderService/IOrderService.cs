using Shared.DTOs;

namespace ClientSide.Services.OrderService
{
    public interface IOrderService
    {
        Task<BrowseResult<OrderDTO>> GetOrders(int page);
        Task<OrderDTO> GetOrderById(int id);
    }
}
