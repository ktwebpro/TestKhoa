using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiKhoaTest.Models
{
    public class AccountRole
    {
        public int AccountRoleId { get; set; }
        public int AccountId { get; set; }
        public int RoleId { get; set; }
    }
}
