using ClickSharp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class PageModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$")]
        [RegularExpression("https://uibakery.io/regex-library/url-regex-csharp")]
        public string Url { get; set; } = string.Empty;
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;
        [StringLength(250)]
        public string Description { get; set; } = string.Empty;
        [StringLength(8000)]
        public string Content { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; } = DateTime.UtcNow;
        public string ModifiedBy { get; set; } = string.Empty;
        public PageStatus Status { get; set; } = PageStatus.IS_DRAFT;
    }
}
