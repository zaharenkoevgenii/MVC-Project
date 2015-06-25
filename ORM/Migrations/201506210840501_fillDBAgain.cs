namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fillDBAgain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Private = c.Boolean(nullable: false),
                        Rating = c.Int(nullable: false),
                        CrationTime = c.DateTime(nullable: false),
                        UserRefId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserRefId, cascadeDelete: true)
                .Index(t => t.UserRefId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RolesUsers",
                c => new
                    {
                        Roles_Id = c.Int(nullable: false),
                        Users_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Roles_Id, t.Users_Id })
                .ForeignKey("dbo.Roles", t => t.Roles_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Users_Id, cascadeDelete: true)
                .Index(t => t.Roles_Id)
                .Index(t => t.Users_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RolesUsers", "Users_Id", "dbo.Users");
            DropForeignKey("dbo.RolesUsers", "Roles_Id", "dbo.Roles");
            DropForeignKey("dbo.Profiles", "Id", "dbo.Users");
            DropForeignKey("dbo.Files", "UserRefId", "dbo.Users");
            DropIndex("dbo.RolesUsers", new[] { "Users_Id" });
            DropIndex("dbo.RolesUsers", new[] { "Roles_Id" });
            DropIndex("dbo.Profiles", new[] { "Id" });
            DropIndex("dbo.Files", new[] { "UserRefId" });
            DropTable("dbo.RolesUsers");
            DropTable("dbo.Roles");
            DropTable("dbo.Profiles");
            DropTable("dbo.Users");
            DropTable("dbo.Files");
        }
    }
}
