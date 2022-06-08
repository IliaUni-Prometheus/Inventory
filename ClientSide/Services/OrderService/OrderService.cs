using Flurl.Http;
using Shared.DTOs;

namespace ClientSide.Services.OrderService
{
    public class OrderService : IOrderService
    {
        public async Task<BrowseResult<OrderDTO>> GetOrders(int page)
        {
            return await $"https://localhost:7045/api/Order?page={page}".GetJsonAsync<BrowseResult<OrderDTO>>();
        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
