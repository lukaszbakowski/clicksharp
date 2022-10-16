using ClickSharp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class UserModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = "Unknown";
        [StringLength(50)]
        public string Email { get; set; } = "user@example.com";
        [StringLength(255)]
        public string Password { get; set; } = "123Example@Password";
        public DateTime Registered { get; set; } = DateTime.UtcNow;
        public Statuses Status { get; set; } = Statuses.IsActive;
    }
}
