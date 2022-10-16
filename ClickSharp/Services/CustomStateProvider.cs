namespace ClickSharp.Services
{
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Security.Claims;
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private ClaimsIdentity? _claimsIdentity;
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_claimsIdentity != null)
            {
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(_claimsIdentity)));
            }
            else
            {
                ClaimsIdentity anonymous = new ClaimsIdentity();
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
            }
        }
        public async Task Login(string name)
        {
            _claimsIdentity = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Role, "default")
                }, "authorizedUser");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
        public async Task Logoff()
        {
            _claimsIdentity = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
    }
}
