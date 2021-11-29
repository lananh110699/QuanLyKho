using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models
{
    [Table("NhapKho")]
    public class NhapKho
    {
        [Key]
        [Required(ErrorMessage = "Mã Phiếu Nhập không được để trống")]
        [Display(Name = "Mã Phiếu Nhập")]
        public string MaPhieuNhap { get; set; }

        [Required(ErrorMessage = "Ngày Xuất không được để trống")]
        [Display(Name = "Ngày Nhập")]
        public string NgayNhap { get; set; }

        [Required(ErrorMessage = "Mã NCC không được để trống")]
        [Display(Name = "Mã NCC")]
        public string MaNCC { get; set; }
        public NCC NCC { get; set; }


        [Required(ErrorMessage = "Mã Hàng không được để trống")]
        [Display(Name = "Mã Hàng")]
        public string MaHang { get; set; }
        public HangHoa HangHoa { get; set; }

        [Display(Name = "Số Lượng")]
        public string SoLuong { get; set; }

        [Display(Name = "Đơn Giá")]
        public string DonGia { get; set; }

        [Display(Name = "Thành Tiền")]
        public string ThanhTien { get; set; }

        
    }
}