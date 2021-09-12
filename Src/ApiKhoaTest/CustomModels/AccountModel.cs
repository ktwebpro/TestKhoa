using ApiKhoaTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiKhoaTest.CustomModels
{
    public class AccountModel: Account
    {
        public string GenderName { get; set; }
        public string StatusName { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string ErrorCode { get; set; }
    }
}
