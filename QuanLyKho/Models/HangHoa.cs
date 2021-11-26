using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models
{
    [Table("HangHoa")]
    public class HangHoa
    {

        [Key]
        [Required(ErrorMessage = "Mã Hàng không được để trống")]
        [Display(Name = "Mã Hàng")]
        public string MaHang { get; set; }

        [Required(ErrorMessage = "Tên Hàng không được để trống")]
        [Display(Name = "Tên Hàng")]
        public string TenHang { get; set; }

        [Required(ErrorMessage = "size không được để trống")]
        public string Size { get; set; }

        [Display(Name = "Số Lượng")]
        public string SoLuong { get; set; }

        [Display(Name = "Đơn Giá")]
        public string DonGia { get; set; }


        [Display(Name = "Thành Tiền")]
        public string ThanhTien { get; set; }
    }
}