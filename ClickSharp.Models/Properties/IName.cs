using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Properties
{
    public interface IName
    {
        [Required(ErrorMessage ="Name is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage ="Name must be minimum of 5 and maximum 50 characters.")]
        string Name { get; }
    }
}
