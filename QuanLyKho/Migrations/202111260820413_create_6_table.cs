namespace QuanLyKho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_6_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Account",
                c => new
                    {
                        Username = c.String(nullable: false, maxLength: 50, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        RoleID = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Username);
            
            CreateTable(
                "dbo.HangHoa",
                c => new
                    {
                        MaHang = c.String(nullable: false, maxLength: 128),
                        TenHang = c.String(nullable: false),
                        Size = c.String(nullable: false),
                        SoLuong = c.String(),
                        DonGia = c.String(),
                        ThanhTien = c.String(),
                    })
                .PrimaryKey(t => t.MaHang);
            
            CreateTable(
                "dbo.NhapKho",
                c => new
                    {
                        MaPhieuNhap = c.String(nullable: false, maxLength: 128),
                        NgayNhap = c.String(nullable: false),
                        MaNCC = c.String(nullable: false, maxLength: 128),
                        MaHang = c.String(nullable: false, maxLength: 128),
                        SoLuong = c.String(),
                        DonGia = c.String(),
                        ThanhTien = c.String(),
                    })
                .PrimaryKey(t => t.MaPhieuNhap)
                .ForeignKey("dbo.HangHoa", t => t.MaHang, cascadeDelete: true)
                .ForeignKey("dbo.NCC", t => t.MaNCC, cascadeDelete: true)
                .Index(t => t.MaNCC)
                .Index(t => t.MaHang);
            
            CreateTable(
                "dbo.NCC",
                c => new
                    {
                        MaNCC = c.String(nullable: false, maxLength: 128),
                        TenNCC = c.String(nullable: false),
                        TenHang = c.String(nullable: false),
                        DiaChi = c.String(nullable: false),
                        SĐT = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaNCC);
            
            CreateTable(
                "dbo.XuatKho",
                c => new
                    {
                        MaPhieuXuat = c.String(nullable: false, maxLength: 128),
                        NgayXuat = c.String(nullable: false),
                        MaHang = c.String(nullable: false, maxLength: 128),
                        SoLuong = c.String(),
                        DonGia = c.String(),
                        ThanhTien = c.String(),
                    })
                .PrimaryKey(t => t.MaPhieuXuat)
                .ForeignKey("dbo.HangHoa", t => t.MaHang, cascadeDelete: true)
                .Index(t => t.MaHang);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 10),
                        RoleName = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.XuatKho", "MaHang", "dbo.HangHoa");
            DropForeignKey("dbo.NhapKho", "MaNCC", "dbo.NCC");
            DropForeignKey("dbo.NhapKho", "MaHang", "dbo.HangHoa");
            DropIndex("dbo.XuatKho", new[] { "MaHang" });
            DropIndex("dbo.NhapKho", new[] { "MaHang" });
            DropIndex("dbo.NhapKho", new[] { "MaNCC" });
            DropTable("dbo.Roles");
            DropTable("dbo.XuatKho");
            DropTable("dbo.NCC");
            DropTable("dbo.NhapKho");
            DropTable("dbo.HangHoa");
            DropTable("dbo.Account");
        }
    }
}
