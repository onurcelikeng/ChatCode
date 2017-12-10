namespace ChatCode.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedMig : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebsiteTable", "NumberOfDownload", c => c.Int(nullable: false));
            AddColumn("dbo.WebsiteTable", "Rating", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebsiteTable", "Rating");
            DropColumn("dbo.WebsiteTable", "NumberOfDownload");
        }
    }
}
