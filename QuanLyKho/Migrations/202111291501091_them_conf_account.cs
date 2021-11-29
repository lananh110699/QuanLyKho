namespace QuanLyKho.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class them_conf_account : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Account", "ConfirmPassword", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Account", "ConfirmPassword");
        }
    }
}
