using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClickSharp.Models.Properties
{
    public interface IEmail
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage ="Email is required.")]
        [StringLength(50, ErrorMessage ="Email cant be longer than 50 characters.")]
        string Email { get; set; }
    }
}
