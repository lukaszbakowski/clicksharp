using ClickSharp.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.DataLayer.Entities
{
    public class User : UserModel
    {
        public virtual List<Privilege>? Privileges { get; set; };
    }
}
