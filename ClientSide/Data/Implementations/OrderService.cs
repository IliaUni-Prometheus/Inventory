using ClientSide.Configs;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ClientSide.Data.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApiConfigs _configs;
        private readonly ILocalStorageService _localStorageService;
        public OrderService(IOptions<ApiConfigs> configs, ILocalStorageService localStorageService)
        {
            _configs = configs.Value;
            _localStorageService = localStorageService;
        }

        public async Task<List<object>> All()
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            var response = await $"{_configs.ApiBaseUrl}/Employee"
                                .WithOAuthBearerToken(accessToken)
                                .GetJsonAsync<object>();

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
