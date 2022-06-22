using ClientSide.Configs;
using ClientSide.Models;
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
            this._configs = configs.Value;
            this._localStorageService = localStorageService;
        }

        public async Task<PaginatedResultViewModel<OrderViewModel>> All(int page)
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            return await $"{_configs.ApiBaseUrl}/Order?page={page}"
                .WithOAuthBearerToken(accessToken)
                .GetJsonAsync<PaginatedResultViewModel<OrderViewModel>>();
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
