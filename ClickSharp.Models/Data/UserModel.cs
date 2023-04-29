using ClickSharp.Models.Enums;
using ClickSharp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class UserModel : IUser, ILogin
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = "Unknown";
        [StringLength(50)]
        public string Email { get; set; } = "user@example.com";
        [StringLength(255)]
        public string Password { get; set; } = "123Example@Password";
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public UserStatus Status { get; set; } = UserStatus.IS_ACTIVE;
        public int Attempts { get; set; } = 0;
        public DateTime LastAuth { get; set; } = DateTime.UtcNow;
        public string IpAddr { get; set; } = string.Empty;
    }
}
