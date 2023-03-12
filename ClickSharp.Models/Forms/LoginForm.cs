using ClickSharp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Forms
{
    public class LoginForm : ILogin
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "please use correct email address")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        //[StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
    public class ChangePasswordForm
    {
        [Required(ErrorMessage = "Password is required")]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,255}$",
         ErrorMessage = "Characters are not allowed. At least one small and one big letter with numbers and one special character must pass")]
        //[StringLength(255, ErrorMessage = "Password must be between 5 and 255 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 8 and 255 characters", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
