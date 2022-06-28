using ClientSide.Configs;
using ClientSide.Data;
using Flurl;
using Flurl.Http;
using Microsoft.Extensions.Options;
using Shared.DTOs;

namespace ClientSide.Services.EmployeeService
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApiConfigs _configs;
        private readonly ILocalStorageService _localStorageService;
        public EmployeeService(IOptions<ApiConfigs> configs, ILocalStorageService localStorageService)
        {
            _configs = configs.Value;
            _localStorageService = localStorageService;
        }

        public async Task<BrowseResult<EmployeeDTO>> GetEmployees(int page,int pageSize)
        {
            var accessToken = await _localStorageService.GetItem<string>("accessToken");

            return await $"{_configs.ApiBaseUrl}"
                                    .AppendPathSegment("/Employee")
                                    .SetQueryParam("page", page)
                                    .SetQueryParam("pageSize", pageSize)
                                    .WithOAuthBearerToken(accessToken)
                                    .GetJsonAsync<BrowseResult<EmployeeDTO>>();
        }
        public async Task<EmployeeDTO> GetEmployeeById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
