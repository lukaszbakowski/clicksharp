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
        public int? PageId { get; set; }
        public int? ParentId { get; set; }
        public int? IndexId { get; set; }
        public string? Content { get; set; }
    }
}
