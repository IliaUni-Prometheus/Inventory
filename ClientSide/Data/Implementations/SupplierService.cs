using ClientSide.Configs;
using ClientSide.Models;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ClientSide.Data.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ApiConfigs _configs;
        private readonly ILocalStorageService _localStorageService;

        public SupplierService(IOptions<ApiConfigs> configs, ILocalStorageService localStorageService)
        {
            this._configs = configs.Value;
            this._localStorageService = localStorageService;
        }

        public async Task<List<SupplierViewModel>> All()
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            return await $"{_configs.ApiBaseUrl}/Supplier"
                .WithOAuthBearerToken(accessToken)
                .GetJsonAsync<List<SupplierViewModel>>();
        }
    }
}
