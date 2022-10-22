using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data.Interfaces
{
    public interface IUser
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}
