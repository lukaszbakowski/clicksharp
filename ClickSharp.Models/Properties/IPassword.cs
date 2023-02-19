using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Properties
{
    public interface IPassword
    {
        [Required(ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 55 characters.")]
        [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W_]).{6,}$",
            ErrorMessage = "Password must contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        string Password { get; set; }
    }
}
