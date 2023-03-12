using ClickSharp.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.DataLayer.Entities
{
    public class Menu : MenuModel
    {
        [ForeignKey("PageId")]
        public virtual Page? Page { get; set; }
        [ForeignKey("ParentId")]
        public virtual Menu? Parent { get; set; }
        [ForeignKey("Id")]
        public virtual ICollection<Menu> Childrens { get; set; } = new List<Menu>();
    }
}
