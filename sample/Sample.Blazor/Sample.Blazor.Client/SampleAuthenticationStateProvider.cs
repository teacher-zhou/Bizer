using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;

namespace Sample.Blazor.Client
{
    public class SampleAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IServiceProvider _serviceProvider;
        const string KEY = "TOKEN";

        public SampleAuthenticationStateProvider(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = string.Empty;
            var js = _serviceProvider.GetRequiredService<IJSRuntime>();
            token = await js.InvokeAsync<string?>("localStorage.getItem", KEY);


            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new());
            }

            JwtSecurityTokenHandler handler = new();
            var jwt = handler.ReadJwtToken(token);

            ClaimsIdentity identity = new(jwt.Claims);

            return new AuthenticationState(new(identity));
        }

        public async Task StoreTokenAsync(string? token)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(token, nameof(token));

            var js = _serviceProvider.GetRequiredService<IJSRuntime>();
            await js.InvokeVoidAsync("localStorage.setItem", KEY, token);
        }
    }
}
