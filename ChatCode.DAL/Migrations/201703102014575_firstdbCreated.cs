namespace ChatCode.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstdbCreated : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AboutMeTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        NameSurname = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        Description = c.String(nullable: false, maxLength: 140),
                        Image = c.String(nullable: false),
                        Background = c.String(),
                        Foreground = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTable", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeveloperTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 30),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SocialLookupListTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        SocialLookupId = c.Int(nullable: false),
                        Url = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SocialLookUpTable", t => t.SocialLookupId, cascadeDelete: true)
                .ForeignKey("dbo.UserTable", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.SocialLookupId);
            
            CreateTable(
                "dbo.SocialLookUpTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SocialMedia = c.String(nullable: false, maxLength: 15),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebsiteTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SourceCode = c.String(),
                        Price = c.Single(nullable: false),
                        DeveloperId = c.Int(nullable: false),
                        WebsiteTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeveloperTable", t => t.DeveloperId, cascadeDelete: true)
                .ForeignKey("dbo.WebsiteTypeTable", t => t.WebsiteTypeId, cascadeDelete: true)
                .Index(t => t.DeveloperId)
                .Index(t => t.WebsiteTypeId);
            
            CreateTable(
                "dbo.WebsiteTypeTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WebsiteTags",
                c => new
                    {
                        Website_Id = c.Int(nullable: false),
                        Tag_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Website_Id, t.Tag_Id })
                .ForeignKey("dbo.WebsiteTable", t => t.Website_Id, cascadeDelete: true)
                .ForeignKey("dbo.TagTable", t => t.Tag_Id, cascadeDelete: true)
                .Index(t => t.Website_Id)
                .Index(t => t.Tag_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebsiteTable", "WebsiteTypeId", "dbo.WebsiteTypeTable");
            DropForeignKey("dbo.WebsiteTags", "Tag_Id", "dbo.TagTable");
            DropForeignKey("dbo.WebsiteTags", "Website_Id", "dbo.WebsiteTable");
            DropForeignKey("dbo.WebsiteTable", "DeveloperId", "dbo.DeveloperTable");
            DropForeignKey("dbo.SocialLookupListTable", "UserId", "dbo.UserTable");
            DropForeignKey("dbo.SocialLookupListTable", "SocialLookupId", "dbo.SocialLookUpTable");
            DropForeignKey("dbo.AboutMeTable", "UserId", "dbo.UserTable");
            DropIndex("dbo.WebsiteTags", new[] { "Tag_Id" });
            DropIndex("dbo.WebsiteTags", new[] { "Website_Id" });
            DropIndex("dbo.WebsiteTable", new[] { "WebsiteTypeId" });
            DropIndex("dbo.WebsiteTable", new[] { "DeveloperId" });
            DropIndex("dbo.SocialLookupListTable", new[] { "SocialLookupId" });
            DropIndex("dbo.SocialLookupListTable", new[] { "UserId" });
            DropIndex("dbo.AboutMeTable", new[] { "UserId" });
            DropTable("dbo.WebsiteTags");
            DropTable("dbo.WebsiteTypeTable");
            DropTable("dbo.WebsiteTable");
            DropTable("dbo.TagTable");
            DropTable("dbo.SocialLookUpTable");
            DropTable("dbo.SocialLookupListTable");
            DropTable("dbo.DeveloperTable");
            DropTable("dbo.UserTable");
            DropTable("dbo.AboutMeTable");
        }
    }
}
