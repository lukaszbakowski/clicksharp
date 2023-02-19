using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace ClickSharp.Models.Auth
{
    public class Identity : IIdentity
    {
        public string? AuthenticationType { get; set; }

        public bool IsAuthenticated { get; set; }

        public string? Name { get; set; }
    }
}
