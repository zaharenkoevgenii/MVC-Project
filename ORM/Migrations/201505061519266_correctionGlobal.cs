namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctionGlobal : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.User", "Role_RoleId", "dbo.Role");
            DropIndex("dbo.User", new[] { "RoleId" });
            DropPrimaryKey("dbo.Role");
            DropPrimaryKey("dbo.User");
            AddColumn("dbo.User", "UserName", c => c.String(nullable: false, maxLength: 50));
            AddPrimaryKey("dbo.Role", "RoleId");
            AddPrimaryKey("dbo.User", "UserId");
            DropColumn("dbo.User", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Name", c => c.String(nullable: false, maxLength: 50));
            DropForeignKey("dbo.User", "Role_RoleId", "dbo.Role");
            DropForeignKey("dbo.Users", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Memberships", "UserId", "dbo.Users");
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.User", new[] { "Role_RoleId" });
            DropIndex("dbo.Roles", new[] { "ApplicationId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ApplicationId" });
            DropIndex("dbo.Memberships", new[] { "ApplicationId" });
            DropIndex("dbo.Memberships", new[] { "UserId" });
            DropPrimaryKey("dbo.User");
            DropPrimaryKey("dbo.Role");
            AlterColumn("dbo.User", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Role", "RoleId", c => c.Int(nullable: false, identity: true));
            DropColumn("dbo.User", "Role_RoleId");
            DropColumn("dbo.User", "UserName");
            DropTable("dbo.UsersInRoles");
            DropTable("dbo.Roles");
            DropTable("dbo.Profiles");
            DropTable("dbo.Users");
            DropTable("dbo.Memberships");
            DropTable("dbo.Applications");
            AddPrimaryKey("dbo.User", "UserId");
            AddPrimaryKey("dbo.Role", "RoleId");
            CreateIndex("dbo.User", "RoleId");
            AddForeignKey("dbo.User", "Role_RoleId", "dbo.Role", "RoleId");
            AddForeignKey("dbo.User", "RoleId", "dbo.Role", "RoleId");
        }
    }
}
