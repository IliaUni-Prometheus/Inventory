using Shared.DTOs;

namespace ClientSide.Services.OrderService
{
    public interface IOrderService
    {
        Task<(BrowseResult<OrderDTO> Data, ErrorDetails Errors)> GetOrders(int page, int pageSize);
        Task<OrderDTO> GetOrderById(int id);
    }
}
