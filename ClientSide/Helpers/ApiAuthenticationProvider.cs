using ClientSide.Data;
using ClientSide.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;

namespace ClientSide.Helpers
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        public string Role;
        public bool IsAdmin;
        public bool IsCustomer;
        public bool IsSupplier;

        public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItem<string>("accessToken");

            if (savedToken == null)
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var state = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromToken(savedToken))));

            Role = state.User.Claims.FirstOrDefault(x => x.Type == "role")?.Value;

            if (Role == "Customer")
                IsCustomer = true;

            if (Role == "Admin")
                IsAdmin = true;

            if (Role == "Supplier")
                IsSupplier = true;

            return state;

        }

        private IEnumerable<Claim> ParseClaimsFromToken(string token)
        {
            var claims = new List<Claim>();
            var payload = token.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    var parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (var parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }
        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        public void MarkUserAsAuthenticated(SignInViewModel model)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Role, model.UserName) }));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            _localStorage.SetItem("accessToken", model.AccessToken);

            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymous));
            _localStorage.RemoveItem("accessToken");

            NotifyAuthenticationStateChanged(authState);
        }
    }
}
