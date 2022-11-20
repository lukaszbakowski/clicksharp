using ClickSharp.Models.Enums;
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
        public int? PageId { get; set; }
        public int? ParentId { get; set; }
        public Tag Tag { get; set; } = Tag.SPAN;
        public int? Index { get; set; }
        public string? Content { get; set; }
    }
}
