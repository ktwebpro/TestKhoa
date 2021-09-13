using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestKhoaExample.Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? Gender { get; set; }
        public int? Status { get; set; }
        public string Avatar { get; set; }
    }
}
