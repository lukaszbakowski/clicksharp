using ClickSharp.Models.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class RegisterModel : LoginModel, IUser
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name must be between 5 and 5 characters", MinimumLength = 5)]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm your password")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string? PasswordConfirm { get; set; }
    }
}
