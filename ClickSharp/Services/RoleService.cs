using ClickSharp.Configuration;
using ClickSharp.DataLayer;
using ClickSharp.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ClickSharp.Services
{
    public class RoleService
    {
        private readonly ClickSharpContext _context;
        public RoleService(ClickSharpContext Context)
        {
            _context = Context;
        }
        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Role.Write}")]
        public async Task AddRoles(int id, IEnumerable<string>? roles)
        {
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.Role.Delete}")]
        public async Task RemoveRoles(int id, IEnumerable<string>? roles)
        {
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
    }
}
