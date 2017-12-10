namespace ChatCode.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updated : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WebsiteTags", "Website_Id", "dbo.WebsiteTable");
            DropForeignKey("dbo.WebsiteTags", "Tag_Id", "dbo.TagTable");
            DropForeignKey("dbo.WebsiteTable", "WebsiteTypeId", "dbo.WebsiteTypeTable");
            DropIndex("dbo.WebsiteTable", new[] { "WebsiteTypeId" });
            DropIndex("dbo.WebsiteTags", new[] { "Website_Id" });
            DropIndex("dbo.WebsiteTags", new[] { "Tag_Id" });
            AddColumn("dbo.WebsiteTable", "Types", c => c.String());
            AddColumn("dbo.WebsiteTable", "Style1", c => c.String());
            AddColumn("dbo.WebsiteTable", "Style2", c => c.String());
            AddColumn("dbo.WebsiteTable", "Decsription", c => c.String());
            AddColumn("dbo.WebsiteTable", "Sex", c => c.String());
            AddColumn("dbo.WebsiteTable", "ScreenShotUrl", c => c.String());
            AddColumn("dbo.WebsiteTable", "Tag_Id", c => c.Int());
            CreateIndex("dbo.WebsiteTable", "Tag_Id");
            AddForeignKey("dbo.WebsiteTable", "Tag_Id", "dbo.TagTable", "Id");
            DropColumn("dbo.WebsiteTable", "WebsiteTypeId");
            DropTable("dbo.WebsiteTypeTable");
            DropTable("dbo.WebsiteTags");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WebsiteTags",
                c => new
                    {
                        Website_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Website_Id, t.Tag_Id });
            
            CreateTable(
                "dbo.WebsiteTypeTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.WebsiteTable", "WebsiteTypeId", c => c.Int(nullable: false));
            DropForeignKey("dbo.WebsiteTable", "Tag_Id", "dbo.TagTable");
            DropIndex("dbo.WebsiteTable", new[] { "Tag_Id" });
            DropColumn("dbo.WebsiteTable", "Tag_Id");
            DropColumn("dbo.WebsiteTable", "ScreenShotUrl");
            DropColumn("dbo.WebsiteTable", "Sex");
            DropColumn("dbo.WebsiteTable", "Decsription");
            DropColumn("dbo.WebsiteTable", "Style2");
            DropColumn("dbo.WebsiteTable", "Style1");
            DropColumn("dbo.WebsiteTable", "Types");
            CreateIndex("dbo.WebsiteTags", "Tag_Id");
            CreateIndex("dbo.WebsiteTags", "Website_Id");
            CreateIndex("dbo.WebsiteTable", "WebsiteTypeId");
            AddForeignKey("dbo.WebsiteTable", "WebsiteTypeId", "dbo.WebsiteTypeTable", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WebsiteTags", "Tag_Id", "dbo.TagTable", "Id", cascadeDelete: true);
            AddForeignKey("dbo.WebsiteTags", "Website_Id", "dbo.WebsiteTable", "Id", cascadeDelete: true);
        }
    }
}
