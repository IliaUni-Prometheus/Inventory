using ClientSide.Models;
using Flurl.Http;

namespace ClientSide.Data.Implementations
{
    public class OrderService : IOrderService
    {
        public async Task<PaginatedResultViewModel<OrderViewModel>> All(int page)
        {
            return await $"https://localhost:7045/api/Order?page={page}".GetJsonAsync<PaginatedResultViewModel<OrderViewModel>>();
        }
    }
}
