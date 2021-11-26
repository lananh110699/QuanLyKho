using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models
{
    [Table("NCCs")]
    public class NCC
    {

        [Key]
        [Required(ErrorMessage = "Mã NCC không được để trống")]
        [Display(Name = "Mã NCC")]
        public string MaNCC { get; set; }

        [Required(ErrorMessage = "Tên NCC không được để trống")]
        [Display(Name = "Tên NCC")]
        public string TenNCC { get; set; }

        [Required(ErrorMessage = "Tên Hàng không được để trống")]
        [Display(Name = "Tên Hàng")]
        public string TenHang { get; set; }

        [Required(ErrorMessage = "Địa Chỉ không được để trống")]
        [Display(Name = "Địa Chỉ")]
        public string DiaChi { get; set; }

        [Required(ErrorMessage = "SĐT không được để trống")]
        public string SĐT { get; set; }
    }
}