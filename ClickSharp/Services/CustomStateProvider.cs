using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using ClickSharp.DataLayer;
using Microsoft.EntityFrameworkCore;
using ClickSharp.Models.Data;
using ClickSharp.DataLayer.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using ClickSharp.Models.Interfaces;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ClickSharp.Configuration;
using ClickSharp.Helpers;

namespace ClickSharp.Services
{
    public class CustomStateProvider : AuthenticationStateProvider
    {
        private ClaimsIdentity? _claimsIdentity;
        private readonly ClickSharpContext _context;
        public CustomStateProvider(ClickSharpContext Context)
        {
            _context = Context;
        }
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
        public async Task Login(ILogin user)
        {
            if (_context.Users != null)
            {
                User? getUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == new HashHelper().GetHash(user.Password));
                if (getUser != null)
                {
                    List<Claim> claims = new()
                    {
                        new Claim(ClaimTypes.Email, getUser.Email),
                        new Claim(ClaimTypes.Name, getUser.Name),
                        new Claim(ClaimTypes.NameIdentifier, getUser.Id.ToString())
                    };

                    if (_context.Roles != null)
                    {
                        getUser.Roles = await _context.Roles.Where(x => x.UserId == getUser.Id).ToListAsync();

                        foreach (var role in getUser.Roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.Name));
                        }
                    }

                    _claimsIdentity = new ClaimsIdentity(claims, "authorizedUser");
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                }
            }
            await Task.Delay(500);
            await Task.CompletedTask;
        }
        public async Task LogOff()
        {
            _claimsIdentity = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
    }
}
