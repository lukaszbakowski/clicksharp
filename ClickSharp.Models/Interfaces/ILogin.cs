using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ClickSharp.Models.Interfaces
{
    public interface ILogin
    {
        [Required(ErrorMessage = "Email is required")]
        [StringLength(50, ErrorMessage = "Incorrect password or email")]
        [EmailAddress(ErrorMessage = "Incorrect password or email")]
        string Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Incorrect password or email")]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{6,}$",
            ErrorMessage = "Incorrect password or email")]
        string Password { get; set; }
    }
}
