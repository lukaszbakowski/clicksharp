namespace ClickSharp.Services
{
    using ClickSharp.Models.Data.Interfaces;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Security.Claims;
    using ClickSharp.DataLayer;
    using Microsoft.EntityFrameworkCore;
    using ClickSharp.Models.Data;
    using ClickSharp.DataLayer.Entities;
    using System.Security.Cryptography;
    using System.Text;
    using System.Data;

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
        public async Task Login(IUser user)
        {
            if (_context.Users != null)
            {
                User? getUser = await _context.Users.FirstOrDefaultAsync(x => (x.Email == user.Email) && VerifyHash(x.Password, user.Password));
                if (getUser != null)
                {
                    List<Claim> claims = new()
                    {
                        new Claim(ClaimTypes.Email, getUser.Email),
                        new Claim(ClaimTypes.Name, getUser.Name)
                    };

                    foreach (var role in getUser.Roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }

                    _claimsIdentity = new ClaimsIdentity(claims, "authorizedUser");
                    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
                }
            }
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
        public async Task LogOff()
        {
            _claimsIdentity = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
        public async Task AddOrModifyUser(UserModel currentUser, UserModel newUser, IEnumerable<string>? roles)
        {
            if (_context.Users != null)
            {
                User? getUser = await _context.Users.FirstOrDefaultAsync(x => (x.Email == currentUser.Email) && VerifyHash(x.Password, currentUser.Password));
                if (getUser == null)
                {
                    getUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == newUser.Email);
                    if(getUser == null && roles != null)
                    {
                        getUser = new();
                        getUser.Name = newUser.Name;
                        getUser.Email = newUser.Email;
                        getUser.Password = GetHash(newUser.Password);
                        getUser.Status = newUser.Status;
                        if(roles.Any())
                        {
                            foreach (var role in roles)
                            {
                                getUser.Roles.Add(new Role()
                                {
                                    Name = role
                                });
                            }
                        } else
                        {
                            getUser.Roles.Add(new Role()
                            {
                                Name = "default"
                            });
                        }

                        _context.Users.Add(getUser);
                        await _context.SaveChangesAsync();
                    }
                } else
                {
                    getUser.Email = newUser.Email;
                    getUser.Name = newUser.Name;
                    getUser.Password = GetHash(newUser.Password);
                    await _context.SaveChangesAsync();
                }
            }
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
        private string GetHash(string input)
        {
            using (SHA256 hashAlgorithm = SHA256.Create())
            {
                byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                var sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }
        private bool VerifyHash(string pw, string pwFromDB)
        {
            var hashOfInput = GetHash(pw);

            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, pwFromDB) == 0;
        }
    }
}
