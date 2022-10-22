using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class HtmlModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public int? ChildId { get; set; }
        public IDictionary<string, string>? Attributes { get; set; }
        public IDictionary<string, string>? Classes { get; set; }
        public string? Content { get; set; }
    }
}
