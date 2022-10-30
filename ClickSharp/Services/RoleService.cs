using ClickSharp.Configuration;
using ClickSharp.DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ClickSharp.Helpers;

namespace ClickSharp.Services
{
    public class RoleService
    {
        private readonly ClickSharpContext _context;
        public RoleService(ClickSharpContext Context)
        {
            _context = Context;
        }
        [Authorize(Roles = $"{RoleNames.CsAdmin},{RoleNames.CsRoleWrite}")]
        public async Task AddRoles(int id, IEnumerable<string>? roles)
        {
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
        [Authorize(Roles = $"{RoleNames.CsAdmin},{RoleNames.CsRoleDelete}")]
        public async Task RemoveRoles(int id, IEnumerable<string>? roles)
        {
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
    }
}
