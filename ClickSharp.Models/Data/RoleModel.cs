﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickSharp.Models.Data
{
    public class RoleModel
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = "Default";
    }
}
