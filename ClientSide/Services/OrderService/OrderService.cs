using ClientSide.Configs;
using ClientSide.Data;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Shared;
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

        public async Task<(BrowseResult<OrderDTO> Data, ErrorDetails Errors)> GetOrders(int page, int pageSize)
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            var response = await $"{_configs.ApiBaseUrl}"
                                    .AppendPathSegment("/Order")
                                    .SetQueryParam("page", page)
                                    .SetQueryParam("pageSize", pageSize)
                                    .AllowAnyHttpStatus()
                                    .WithOAuthBearerToken(accessToken)
                                    .GetAsync();

            BrowseResult<OrderDTO> data = null;
            ErrorDetails errors = null;

            if (response.StatusCode < 300)
            {
                data = await response.GetJsonAsync<BrowseResult<OrderDTO>>();
            }
            else if (response.StatusCode == 400)
            {
                errors = await response.GetJsonAsync<ErrorDetails>();
            }
            else if (response.StatusCode == 403)
            {
                errors = new ErrorDetails
                {
                    Status = response.StatusCode,
                    Error = new Error
                    {
                        Message = "Not Authorized",
                        Code = response.StatusCode.ToString()
                    }
                };
            }
            else
            {
                errors = new ErrorDetails
                {
                    Status = response.StatusCode,
                    Error = new Error
                    {
                        Message = "Server Error",
                        Code = response.StatusCode.ToString()
                    }
                };
            }

            return (data, errors);

        }
        public async Task<OrderDTO> GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

    }
}
