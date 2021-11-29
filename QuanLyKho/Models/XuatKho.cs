using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace QuanLyKho.Models
{
    [Table("XuatKho")]
    public class XuatKho
    {

        [Key]
        [Required(ErrorMessage = "Mã Phiếu Xuất không được để trống")]
        [Display(Name = "Mã Phiếu Xuất")]
        public string MaPhieuXuat { get; set; }

        [Required(ErrorMessage = "Ngày xuất không được để trống")]
        [Display(Name = "Ngày Xuất")]
        public string NgayXuat { get; set; }

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