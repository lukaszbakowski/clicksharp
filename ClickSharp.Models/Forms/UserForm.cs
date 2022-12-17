using ClickSharp.Models.Enums;
using ClickSharp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Forms
{
    public class UserForm
    {
        [StringLength(50)]
        public string Name { get; set; } = "Unknown";
        [StringLength(50)]
        public string Email { get; set; } = "user@example.com";
        [StringLength(255)]
        public string Password { get; set; } = "123Example@Password";
        [StringLength(50)]
        public List<RoleForm> Roles { get; set; } = new List<RoleForm>();
        public class RoleForm
        {
            public string Name { get; set; } = "default";
            public bool IsSet { get; set; } = false;
        }
    }
}
