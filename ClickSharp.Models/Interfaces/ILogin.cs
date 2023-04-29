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
        string Email { get; set; }
        string Password { get; set; }
    }
}
