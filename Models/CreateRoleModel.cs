using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroInternalControlSystem.Models
{
    public class CreateRoleModel
    {
        [Required]
        public string Rolename { get; set; }
    }
}
