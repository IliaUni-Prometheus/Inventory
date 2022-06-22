using ClientSide.Configs;
using ClientSide.Models;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ClientSide.Data.Implementations
{
    public class ShipperService : IShipperService
    {
        private readonly ApiConfigs _configs;
        private readonly ILocalStorageService _localStorageService;

        public ShipperService(IOptions<ApiConfigs> configs, ILocalStorageService localStorageService)
        {
            this._configs = configs.Value;
            this._localStorageService = localStorageService;
        }

        public async Task<List<ShipperViewModel>> All()
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            return await $"{_configs.ApiBaseUrl}/Shipper"
                .WithOAuthBearerToken(accessToken)
                .GetJsonAsync<List<ShipperViewModel>>();
        }
    }
}
