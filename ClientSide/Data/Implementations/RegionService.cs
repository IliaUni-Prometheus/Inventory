using ClientSide.Configs;
using ClientSide.Models;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ClientSide.Data.Implementations
{
    public class RegionService : IRegionService
    {
        private readonly ApiConfigs _configs;
        private readonly ILocalStorageService _localStorageService;

        public RegionService(IOptions<ApiConfigs> configs, ILocalStorageService localStorageService)
        {
            this._configs = configs.Value;
            this._localStorageService = localStorageService;
        }

        public async Task<List<RegionViewModel>> All()
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            return await $"{_configs.ApiBaseUrl}/Region"
                .WithOAuthBearerToken(accessToken)
                .GetJsonAsync<List<RegionViewModel>>();
        }
    }
}
