using Flurl.Http;

namespace ClientSide.Data.Implementations
{
    public class OrderService : IOrderService
    {
        public async Task<List<object>> All()
        {
            var response = await "https://localhost:7045/api/Employee".GetJsonAsync<object>();

            throw new NotImplementedException();
        }

        public Task Delete(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<object> OfId(int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
