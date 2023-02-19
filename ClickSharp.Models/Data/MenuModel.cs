using ClickSharp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class MenuModel 
    {
        public int Id { get; set; }
        public int Index { get; set; } = 0;
        public int? ParentId { get; set; }
        public int? PageId { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; } = string.Empty;
    }
}
