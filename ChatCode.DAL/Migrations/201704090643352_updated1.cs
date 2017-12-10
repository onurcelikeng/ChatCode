namespace ChatCode.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebsiteTable", "PublishedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebsiteTable", "PublishedDate");
        }
    }
}
