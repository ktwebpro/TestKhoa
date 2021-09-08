using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestKhoa.Models.Account
{
    public class ListViewModel:AccountModel
    {
        public string Role { get; set; }
    }
}
