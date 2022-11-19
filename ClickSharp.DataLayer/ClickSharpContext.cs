using ClickSharp.DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ClickSharp.DataLayer
{
    public class ClickSharpContext : DbContext
    {
        public ClickSharpContext(DbContextOptions<ClickSharpContext> Options) : base(Options)
        {

        }
        public DbSet<User>? Users { get; set; }
        public DbSet<Role>? Roles { get; set; }
        public DbSet<Html>? Htmls { get; set; }
        public DbSet<Page>? Pages { get; set; }
        public DbSet<Img>? Images { get; set; }
    }
}
