using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Properties
{
    public interface IRePassword : IPassword
    {
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        string RePassword { get; set; }
    }
}
