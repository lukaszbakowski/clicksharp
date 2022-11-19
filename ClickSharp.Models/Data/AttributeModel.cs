using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class AttributeModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int HtmlId { get; set; }
    }
}
