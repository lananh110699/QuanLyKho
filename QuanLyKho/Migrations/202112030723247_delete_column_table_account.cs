namespace QuanLyKho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delete_column_table_account : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Account", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Account", "ConfirmPassword", c => c.String());
        }
    }
}
