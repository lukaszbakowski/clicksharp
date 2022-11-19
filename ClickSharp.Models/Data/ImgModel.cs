using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class ImgModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Payload { get; set; }
    }
}
