using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiKhoaTest.Models
{
    public class Account
    {
        [Key]
        public int AccountId { get; set; }
        [Required]
        [DisplayName ("Tên đăng nhập")]
        [MaxLength(50)]
        public string UserName { get; set; }
        [MaxLength(10)]
        public string UserCode { get; set; }
        [Required]
        [DisplayName("Mật khẩu")]
        [MaxLength(100)]
        public string Password { get; set; }
        [DisplayName("Họ tên")]
        [MaxLength(50)]
        public string FullName { get; set; }
        [DisplayName("Địa chỉ")]
        [MaxLength(200)]
        public string Address { get; set; }
        [DisplayName("Email")]
        [MaxLength(50)]
        public string Email { get; set; }
        [DisplayName("Điện thoại")]
        [MaxLength(15)]
        public string Phone { get; set; }
        [DisplayName("Giới tính")]
        [Range(1,5)]
        
        public byte Gender { get; set; }
        [DisplayName("Trạng thái")]
        [Range(1, 5)]
        public sbyte Status { get; set; }
        [DisplayName("Avatar")]
        [MaxLength(50)]
        public string Avatar { get; set; }
    }
}
