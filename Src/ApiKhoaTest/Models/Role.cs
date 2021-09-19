using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiKhoaTest.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [MaxLength(15)]
        public string RoleName { get; set; }
    }
}
