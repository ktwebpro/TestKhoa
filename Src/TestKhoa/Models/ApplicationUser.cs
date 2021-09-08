using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TestKhoa.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class AccountModel : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Status { get; set; }
        public string Avatar { get; set; }
    }
}
