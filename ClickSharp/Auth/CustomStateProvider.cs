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
using ClickSharp.Models.Auth;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ClickSharp.Configuration;
using ClickSharp.Helpers;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ClickSharp.Models.Enums;
using static ClickSharp.Configuration.AppRoles;
using Microsoft.Extensions.Options;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace ClickSharp.Auth
{
    public class CustomStateProvider : AuthenticationStateProvider
    {

#if DEBUG
        private ClaimsIdentity? _claimsIdentity = new ClaimsIdentity(new Identity { AuthenticationType = "authorizedUser", IsAuthenticated = true, Name = "Admin" }, new List<Claim>() { new Claim(ClaimTypes.Role, "admin")});
#else
        private ClaimsIdentity? _claimsIdentity;
#endif
        private readonly ClickSharpContext _context;
        private readonly ProtectedSessionStorage _protectedSessionStorage;
        public CustomStateProvider(ProtectedSessionStorage protectedSessionStorage, ClickSharpContext Context)
        {
            _context = Context;
            _protectedSessionStorage = protectedSessionStorage;
        }
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (_claimsIdentity != null)
            {
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(_claimsIdentity)));
            }
            else
            {
                try
                {
                    var token = await _protectedSessionStorage.GetAsync<string>("Token");
                    if (token.Success)
                    {
                        var tokenHandler = new TokenHandler();
                        string? value = token.Value?.ToString();
                        if (value != null)
                        {
                            if (tokenHandler.ValidateCurrentToken(value))
                            {
                                List<Claim> claims = (List<Claim>)tokenHandler.GetClaims(value);
                                _claimsIdentity = new ClaimsIdentity(claims, "authorizedUser");
                                Console.WriteLine("ok");
                                foreach (Claim claim in _claimsIdentity.Claims)
                                {
                                    Console.WriteLine(claim.ValueType.ToString());
                                    Console.WriteLine(claim.Type.ToString());
                                    Console.WriteLine(claim.Value.ToString());
                                }
                                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(_claimsIdentity)));
                            } else
                            {
                                throw new Exception("token is invalid");
                            }
                        } else
                        {
                            throw new Exception("token has no value");
                        }
                    } else
                    {
                        throw new Exception("token not success");
                    }
                } catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                ClaimsIdentity anonymous = new ClaimsIdentity();
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
            }
        }
        public async Task<bool> Login(ILogin user)
        {
            bool isSuccess = false;
            if (_context.Users != null)
            {
                ClickSharp.DataLayer.Entities.User? getUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email && x.Password == new HashHelper().GetHash(user.Password));
                if (getUser != null)
                {
                    
                    List<Claim> claims = new()
                    {
                        new Claim(nameof(ClaimType.Email), getUser.Email),
                        new Claim(nameof(ClaimType.Name), getUser.Name),
                        new Claim(nameof(ClaimType.Id), getUser.Id.ToString())
                    };

                    if (_context.Privileges != null)
                    {
                        getUser.Privileges = await _context.Privileges.Where(x => x.UserId == getUser.Id).Include(x => x.Role).ToListAsync();

                        foreach (var privilege in getUser.Privileges)
                        {
                            Console.WriteLine(privilege.Role.Name);
                            claims.Add(new Claim(ClaimTypes.Role, privilege.Role.Name));
                        }
                    }

                    await _protectedSessionStorage.SetAsync("Token", new TokenHandler().GenerateToken(claims));

                    Identity identity = new Identity { AuthenticationType = "authorizedUser", IsAuthenticated = true, Name = getUser.Name };

                    _claimsIdentity = new ClaimsIdentity(identity,claims);
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                    isSuccess = true;
                }
            }
            await Task.Delay(500);
            return await Task.FromResult(isSuccess);
        }
        public async Task LogOff()
        {
            await _protectedSessionStorage.DeleteAsync("Token");
            _claimsIdentity = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
    }
}
