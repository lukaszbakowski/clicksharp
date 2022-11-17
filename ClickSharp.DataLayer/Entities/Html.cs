using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClickSharp.Models.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClickSharp.DataLayer.Entities
{
    public class Html : HtmlModel
    {
        [ForeignKey("PageId")]
        public Page? Page { get; set; }
        [ForeignKey("ParentId")]
        public Html? ParentHtml { get; set; }
        public List<Html> Htmls { get; set; } = new List<Html>();
    }
}
