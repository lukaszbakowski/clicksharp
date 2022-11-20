using ClickSharp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class PageModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public DateTime ModificationTime { get; set; } = DateTime.UtcNow;
        public string ModifiedBy { get; set; } = string.Empty; //rozwazyc relacje z userem?
        public PageStatus Status { get; set; } = PageStatus.IS_DRAFT;
    }
}
