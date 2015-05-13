namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctionGlobal9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Users", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Memberships", "UserId", "dbo.Users");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropIndex("dbo.Memberships", new[] { "UserId" });
            DropIndex("dbo.Memberships", new[] { "ApplicationId" });
            DropIndex("dbo.Users", new[] { "ApplicationId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "ApplicationId" });
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            //DropPrimaryKey("dbo.Applications");
            //DropPrimaryKey("dbo.Memberships");
            //DropPrimaryKey("dbo.Users");
            //DropPrimaryKey("dbo.Profiles");
            //DropPrimaryKey("dbo.Roles");
            //DropPrimaryKey("dbo.UsersInRoles");
            //AlterColumn("dbo.Applications", "ApplicationId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Memberships", "UserId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Memberships", "ApplicationId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Users", "UserId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Users", "ApplicationId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Users", "RoleId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Profiles", "UserId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Roles", "RoleId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.Roles", "ApplicationId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.UsersInRoles", "RoleId", c => c.Guid(nullable: false));
            //AlterColumn("dbo.UsersInRoles", "UserId", c => c.Guid(nullable: false));
            //AddPrimaryKey("dbo.Applications", "ApplicationId");
            //AddPrimaryKey("dbo.Memberships", "UserId");
            //AddPrimaryKey("dbo.Users", "UserId");
            //AddPrimaryKey("dbo.Profiles", "UserId");
            //AddPrimaryKey("dbo.Roles", "RoleId");
            //AddPrimaryKey("dbo.UsersInRoles", new[] { "RoleId", "UserId" });
            CreateIndex("dbo.Memberships", "UserId");
            CreateIndex("dbo.Memberships", "ApplicationId");
            CreateIndex("dbo.Users", "ApplicationId");
            CreateIndex("dbo.Profiles", "UserId");
            CreateIndex("dbo.Roles", "ApplicationId");
            CreateIndex("dbo.UsersInRoles", "RoleId");
            CreateIndex("dbo.UsersInRoles", "UserId");
            AddForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Users", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Memberships", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Profiles", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Memberships", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications");
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.Roles", new[] { "ApplicationId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "ApplicationId" });
            DropIndex("dbo.Memberships", new[] { "ApplicationId" });
            DropIndex("dbo.Memberships", new[] { "UserId" });
            DropPrimaryKey("dbo.UsersInRoles");
            DropPrimaryKey("dbo.Roles");
            DropPrimaryKey("dbo.Profiles");
            DropPrimaryKey("dbo.Users");
            DropPrimaryKey("dbo.Memberships");
            DropPrimaryKey("dbo.Applications");
            AlterColumn("dbo.UsersInRoles", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.UsersInRoles", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Roles", "ApplicationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Roles", "RoleId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Profiles", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "ApplicationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Memberships", "ApplicationId", c => c.Int(nullable: false));
            AlterColumn("dbo.Memberships", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.Applications", "ApplicationId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UsersInRoles", new[] { "RoleId", "UserId" });
            AddPrimaryKey("dbo.Roles", "RoleId");
            AddPrimaryKey("dbo.Profiles", "UserId");
            AddPrimaryKey("dbo.Users", "UserId");
            AddPrimaryKey("dbo.Memberships", "UserId");
            AddPrimaryKey("dbo.Applications", "ApplicationId");
            CreateIndex("dbo.UsersInRoles", "UserId");
            CreateIndex("dbo.UsersInRoles", "RoleId");
            CreateIndex("dbo.Roles", "ApplicationId");
            CreateIndex("dbo.Profiles", "UserId");
            CreateIndex("dbo.Users", "ApplicationId");
            CreateIndex("dbo.Memberships", "ApplicationId");
            CreateIndex("dbo.Memberships", "UserId");
            AddForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Profiles", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Memberships", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Users", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications", "ApplicationId");
        }
    }
}
