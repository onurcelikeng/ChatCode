namespace ChatCode.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deneme : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WebSourceCode = c.String(),
                        Url = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserTable", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectTable", "UserId", "dbo.UserTable");
            DropIndex("dbo.ProjectTable", new[] { "UserId" });
            DropTable("dbo.ProjectTable");
        }
    }
}
