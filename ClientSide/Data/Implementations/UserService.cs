using ClientSide.Configs;
using ClientSide.Helpers;
using ClientSide.Models;
using Flurl.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

namespace ClientSide.Data.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApiConfigs _configs;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public UserService(IOptions<ApiConfigs> configs, AuthenticationStateProvider authenticationStateProvider)
        {
            _configs = configs.Value;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task Logout()
        {
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }

        public async Task<SignInViewModel> SignIn(SignInViewModel model)
        {
            var response = await $"{ _configs.BaseUrl}/User/Authenticate"
                                 .PostJsonAsync(model)
                                 .ReceiveJson<SignInViewModel>();

            if (response != null)
            {
                ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(response);
            }

            return response;
        }
    }
}
