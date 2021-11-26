using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models
{
    [Table("Account")]
    public class Account
    {

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        [StringLength(50)]
        public string Password { get; set; }
        [StringLength(10)]
        public string RoleID { get; set; }
    }
}