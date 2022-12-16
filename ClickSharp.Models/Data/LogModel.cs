using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class LogModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime EventTime { get; set; } = DateTime.UtcNow;
    }
}
