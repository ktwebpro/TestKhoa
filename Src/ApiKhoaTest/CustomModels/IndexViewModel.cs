using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiKhoaTest.CustomModels
{
    public class IndexViewModel
    {
        public string Username { get; set; }
        public int Id { get; set; }
        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public int Gender { get; set; }
        public int Status { get; set; }
        public string Avatar { get; set; }

        public string StatusMessage { get; set; }
    }
}
