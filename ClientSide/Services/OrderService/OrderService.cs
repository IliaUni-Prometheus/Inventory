using ClientSide.Configs;
using ClientSide.Data;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Shared.DTOs;

namespace ClientSide.Services.OrderService
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

        public async Task<BrowseResult<OrderDTO>> GetOrders(int page,int pageSize)
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            return await $"{_configs.ApiBaseUrl}"
                                    .AppendPathSegment("/Order")
                                    .SetQueryParam("page", page)
                                    .SetQueryParam("pageSize", pageSize)
                                    .WithOAuthBearerToken(accessToken)
                                    .GetJsonAsync<BrowseResult<OrderDTO>>();
        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
