using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models
{
    [Table("KhachHang")]
    public class KhachHang
    {
        [Key]
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        [Display(Name = "Mã khách hàng")]
        public string MaKhachHang { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [Display(Name = "Tên khách hàng")]
        public string TenKhachHang { get; set; }

        
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Display(Name = "Số điện thoại")]
        public string SĐT { get; set; }

        [Required(ErrorMessage = "Số tài khoản ngân hàng không được để trống")]
        [Display(Name = "Số tài khoản ngân hàng")]
        public string STK { get; set; }

        [Required(ErrorMessage = "Ngân hàng không được để trống")]
        [Display(Name = "Ngân hàng")]
        public string NganHang { get; set; }

    }
}