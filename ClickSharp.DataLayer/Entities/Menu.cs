using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClickSharp.Models.Data;

namespace ClickSharp.DataLayer.Entities
{
    public class Menu : MenuModel
    {
        [ForeignKey("PageId")]
        public Page? Page { get; set; }
        [ForeignKey("ParentId")]
        public virtual Menu? Parent { get; set; }
        public virtual List<Menu> Childrens { get; set; } = new List<Menu>();
    }
}
