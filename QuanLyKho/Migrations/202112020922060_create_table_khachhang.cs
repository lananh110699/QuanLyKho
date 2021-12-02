namespace QuanLyKho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class create_table_khachhang : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KhachHang",
                c => new
                    {
                        MaKhachHang = c.String(nullable: false, maxLength: 128),
                        TenKhachHang = c.String(nullable: false),
                        DiaChi = c.String(),
                        SĐT = c.String(nullable: false),
                        STK = c.String(nullable: false),
                        NganHang = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.MaKhachHang);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.KhachHang");
        }
    }
}
