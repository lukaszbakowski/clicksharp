namespace ClickSharp.Services
{
    using ClickSharp.Models.Data.Interfaces;
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
        public async Task Login(IUser user)
        {

            _claimsIdentity = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Email, user.Email),
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
