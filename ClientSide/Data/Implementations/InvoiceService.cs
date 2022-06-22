using ClientSide.Configs;
using ClientSide.Models;
using Flurl.Http;
using Microsoft.Extensions.Options;

namespace ClientSide.Data.Implementations
{
    public class InvoiceService : IInvoiceService
    {
        private readonly ApiConfigs _configs;
        private readonly ILocalStorageService _localStorageService;

        public InvoiceService(IOptions<ApiConfigs> configs, ILocalStorageService localStorageService)
        {
            this._configs = configs.Value;
            this._localStorageService = localStorageService;
        }

        public async Task<List<InvoiceViewModel>> All()
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            return await $"{_configs.ApiBaseUrl}/Invoice"
                .WithOAuthBearerToken(accessToken)
                .GetJsonAsync<List<InvoiceViewModel>>();
        }
    }
}
