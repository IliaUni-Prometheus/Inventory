using Flurl.Http;

namespace ClientSide.Data.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ILocalStorageService _localStorage;

        public OrderService(ILocalStorageService localStorageService)
        {
            _localStorage = localStorageService;
        }
        public async Task<List<object>> All()
        {
            var token = await _localStorage.GetItem<string>("accessToken");

            var response = await "https://localhost:7045/api/Employee"
                               .WithOAuthBearerToken(token).
                               GetJsonAsync<object>();


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
