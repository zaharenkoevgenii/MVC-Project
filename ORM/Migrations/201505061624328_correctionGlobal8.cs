namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctionGlobal8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Users", "ApplicationId", "dbo.Applications");

            DropIndex("dbo.Memberships", new[] { "ApplicationId" });
            DropIndex("dbo.Memberships", new[] { "Users_UserId" });
            DropIndex("dbo.Users", new[] { "ApplicationId" });
            DropIndex("dbo.Profiles", new[] { "Users_UserId" });
            DropIndex("dbo.Roles", new[] { "ApplicationId" });

            //DropColumn("dbo.Memberships", "UserId");
            //DropColumn("dbo.Profiles", "UserId");
            //RenameColumn(table: "dbo.Memberships", name: "Users_UserId", newName: "UserId");
            //RenameColumn(table: "dbo.Profiles", name: "Users_UserId", newName: "UserId");
            //DropPrimaryKey("dbo.Applications");
            //DropPrimaryKey("dbo.Memberships");
            //DropPrimaryKey("dbo.Profiles");
            //AlterColumn("dbo.Applications", "ApplicationId", c => c.Int(nullable: false, identity: true));
            //AlterColumn("dbo.Memberships", "UserId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Memberships", "ApplicationId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Users", "ApplicationId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Profiles", "UserId", c => c.Int(nullable: false));
            //AlterColumn("dbo.Roles", "ApplicationId", c => c.Int(nullable: false));
            //AddPrimaryKey("dbo.Applications", "ApplicationId");
            //AddPrimaryKey("dbo.Memberships", "UserId");
            //AddPrimaryKey("dbo.Profiles", "UserId");

            //CreateIndex("dbo.Memberships", "UserId");
            //CreateIndex("dbo.Memberships", "ApplicationId");
            //CreateIndex("dbo.Users", "ApplicationId");
            //CreateIndex("dbo.Profiles", "UserId");
            //CreateIndex("dbo.Roles", "ApplicationId");

            AddForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Users", "ApplicationId", "dbo.Applications", "ApplicationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications");
            DropIndex("dbo.Roles", new[] { "ApplicationId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ApplicationId" });
            DropIndex("dbo.Memberships", new[] { "ApplicationId" });
            DropIndex("dbo.Memberships", new[] { "UserId" });
            DropPrimaryKey("dbo.Profiles");
            DropPrimaryKey("dbo.Memberships");
            DropPrimaryKey("dbo.Applications");
            AlterColumn("dbo.Roles", "ApplicationId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Profiles", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Users", "ApplicationId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Memberships", "ApplicationId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Memberships", "UserId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Applications", "ApplicationId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Profiles", "UserId");
            AddPrimaryKey("dbo.Memberships", "UserId");
            AddPrimaryKey("dbo.Applications", "ApplicationId");
            RenameColumn(table: "dbo.Profiles", name: "UserId", newName: "Users_UserId");
            RenameColumn(table: "dbo.Memberships", name: "UserId", newName: "Users_UserId");
            AddColumn("dbo.Profiles", "UserId", c => c.Guid(nullable: false));
            AddColumn("dbo.Memberships", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Roles", "ApplicationId");
            CreateIndex("dbo.Profiles", "Users_UserId");
            CreateIndex("dbo.Users", "ApplicationId");
            CreateIndex("dbo.Memberships", "Users_UserId");
            CreateIndex("dbo.Memberships", "ApplicationId");
            AddForeignKey("dbo.Users", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications", "ApplicationId");
        }
    }
}
