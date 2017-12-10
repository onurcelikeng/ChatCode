namespace ChatCode.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WebsiteTable", "TemplateName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.WebsiteTable", "TemplateName");
        }
    }
}
