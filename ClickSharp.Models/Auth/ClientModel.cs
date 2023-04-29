using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Auth
{
    public class ClientModel
    {
        protected int _attempts = 0;
        protected DateTime _lastAttempt = DateTime.Now;
        public int Attempts
        {
            get { return _attempts; } 
        }
        public DateTime LastAttempt
        {
            get { return _lastAttempt; }
        }

        public static ClientModel operator ++(ClientModel cm)
        {
            cm._attempts++;
            cm._lastAttempt = DateTime.Now;
            return cm;
        }
        public static ClientModel operator --(ClientModel cm)
        {
            if(cm._attempts > 0)
            {
                cm._attempts--;
                cm._lastAttempt = DateTime.Now;
            }
            return cm;
        }
    }
}
