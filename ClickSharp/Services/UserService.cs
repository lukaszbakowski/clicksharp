using ClickSharp.Configuration;
using ClickSharp.DataLayer;
using ClickSharp.DataLayer.Entities;
using ClickSharp.Helpers;
using ClickSharp.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ClickSharp.Services
{
    public class UserService
    {
        private readonly ClickSharpContext _context;
        public UserService(ClickSharpContext Context)
        {
            _context = Context;
        }
        [Authorize(Roles = AppRoles.Admin)]
        public async Task AddUser(UserModel newUser, IEnumerable<string>? roles)
        {
            if (_context.Users != null)
            {
                User? getUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == newUser.Email);
                if (getUser == null && roles != null)
                {
                    getUser = new();
                    getUser.Name = newUser.Name;
                    getUser.Email = newUser.Email;
                    getUser.Password = new HashHelper().GetHash(newUser.Password);
                    getUser.Status = newUser.Status;
                    //if (roles.Any())
                    //{
                    //    foreach (var role in roles)
                    //    {
                    //        getUser.Roles.Add(new Role()
                    //        {
                    //            Name = role
                    //        });
                    //    }
                    //}
                    //else
                    //{
                    //    getUser.Roles.Add(new Role()
                    //    {
                    //        Name = "default"
                    //    });
                    //}

                    _context.Users.Add(getUser);
                    await _context.SaveChangesAsync();
                }
            }
            await Task.Delay(1000);
            await Task.CompletedTask;
        }

        public async Task ModifyUser(UserModel currentUser, UserModel newUser)
        {
            if (_context.Users != null)
            {
                User? getUser = await _context.Users.FirstOrDefaultAsync(x => (x.Email == currentUser.Email) && x.Password == new HashHelper().GetHash(currentUser.Password));
                if (getUser != null)
                {
                    UserModel validateUser = new();
                    getUser.Email = newUser.Email == validateUser.Email ? getUser.Email : newUser.Email;
                    getUser.Name = newUser.Name == validateUser.Name ? getUser.Name : newUser.Name;
                    getUser.Password = newUser.Password == validateUser.Password ? getUser.Password : new HashHelper().GetHash(newUser.Password);
                    await _context.SaveChangesAsync();
                }
            }
            await Task.Delay(1000);
            await Task.CompletedTask;
        }

        [Authorize(Roles = $"{AppRoles.Admin},{AppRoles.User.Delete}")]
        public async Task RemoveUser(int id)
        {
            await Task.Delay(1000);
            await Task.CompletedTask;
        }
    }
}
