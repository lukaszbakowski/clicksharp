using ClickSharp.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ClickSharp.Models.Enums;
using System.Reflection.Metadata;


namespace ClickSharp.DataLayer
{
    public class ClickSharpContext : DbContext
    {
        public ClickSharpContext(DbContextOptions<ClickSharpContext> Options) : base(Options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
                    new User
                    {
                        Id = 1,
                        Name = "Admin",
                        Email = "super@admin.com",
                        Password = "931484e22cdd0299c6d635e1703befcebd179f05df3dd0dbee3de8fc0db959f1",
                        CreationDate = DateTime.Now,
                        Status = UserStatus.IS_ACTIVE
                    });
            modelBuilder.Entity<Role>().HasData(
                    new Role
                    {
                        Id = 1,
                        Name = "admin"
                    });
            modelBuilder.Entity<Privilege>().HasData(
                    new Privilege
                    {
                        RoleId = 1,
                        UserId = 1
                    });
            modelBuilder.Entity<Menu>().HasData(
                    new Menu
                    {
                        Id = 1,
                        DisplayName = "CONTAINER",
                        Index = 0
                    }, new Menu
                    {
                        Id = 2,
                        DisplayName = "MENU",
                        Index = 0,
                        ParentId = 1
                    }, new Menu
                    {
                        Id = 3,
                        DisplayName = "EMPTY",
                        Index = 1,
                        ParentId = 1
                    }, new Menu
                    {
                        Id = 4,
                        DisplayName = "TEST1",
                        Index = 0,
                        ParentId = 3
                    }, new Menu
                    {
                        Id = 5,
                        DisplayName = "TEST2",
                        Index = 1,
                        ParentId = 3
                    }, new Menu
                    {
                        Id = 6,
                        DisplayName = "TEST3",
                        Index = 2,
                        ParentId = 3
                    }, new Menu
                    {
                        Id = 7,
                        DisplayName = "TEST4",
                        Index = 3,
                        ParentId = 3
                    }, new Menu
                    {
                        Id = 8,
                        DisplayName = "TEST5",
                        Index = 4,
                        ParentId = 3
                    });
            modelBuilder.Entity<Page>().HasData(
                    new Page
                    {
                        Id = 1,
                        Content = "example index content",
                        Description = "index",
                        Url = "/",
                        Title = "index",
                        Status = PageStatus.IS_ACTIVE
                    });
        }

        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Page>? Pages { get; set; }
        public DbSet<Menu>? Menu { get; set; }
        public DbSet<Privilege>? Privileges { get; set; }
    }
}
