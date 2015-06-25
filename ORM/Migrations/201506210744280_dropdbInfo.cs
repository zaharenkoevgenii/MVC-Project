namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropdbInfo : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            DropForeignKey("dbo.Memberships", "UserId", "dbo.Users");
            DropForeignKey("dbo.Profiles", "UserId", "dbo.Users");
            DropForeignKey("dbo.RolesUsers", "Roles_RoleId", "dbo.Roles");
            DropForeignKey("dbo.RolesUsers", "Users_UserId", "dbo.Users");
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications");
            DropForeignKey("dbo.Users", "ApplicationId", "dbo.Applications");
            DropIndex("dbo.Memberships", new[] { "UserId" });
            DropIndex("dbo.Memberships", new[] { "ApplicationId" });
            DropIndex("dbo.Users", new[] { "ApplicationId" });
            DropIndex("dbo.Files", new[] { "UserId" });
            DropIndex("dbo.Profiles", new[] { "UserId" });
            DropIndex("dbo.Roles", new[] { "ApplicationId" });
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.RolesUsers", new[] { "Roles_RoleId" });
            DropIndex("dbo.RolesUsers", new[] { "Users_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Files");
            DropTable("dbo.Profiles");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RolesUsers",
                c => new
                    {
                        Roles_RoleId = c.Guid(nullable: false),
                        Users_UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Roles_RoleId, t.Users_UserId });
            
            CreateTable(
                "dbo.UsersInRoles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId });
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        RoleName = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 256),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Profiles",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        PropertyNames = c.String(nullable: false),
                        PropertyValueStrings = c.String(nullable: false),
                        PropertyValueBinary = c.Binary(nullable: false),
                        LastUpdatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Guid(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        CreationTime = c.DateTime(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.FileId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 50),
                        IsAnonymous = c.Boolean(nullable: false),
                        LastActivityDate = c.DateTime(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        UserId = c.Guid(nullable: false),
                        ApplicationId = c.Guid(nullable: false),
                        Password = c.String(nullable: false, maxLength: 128),
                        PasswordFormat = c.Int(nullable: false),
                        PasswordSalt = c.String(maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 256),
                        PasswordQuestion = c.String(maxLength: 256),
                        PasswordAnswer = c.String(maxLength: 128),
                        IsApproved = c.Boolean(nullable: false),
                        IsLockedOut = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastLoginDate = c.DateTime(nullable: false),
                        LastPasswordChangedDate = c.DateTime(nullable: false),
                        LastLockoutDate = c.DateTime(nullable: false),
                        FailedPasswordAttemptCount = c.Int(nullable: false),
                        FailedPasswordAttemptWindowStart = c.DateTime(nullable: false),
                        FailedPasswordAnswerAttemptCount = c.Int(nullable: false),
                        FailedPasswordAnswerAttemptWindowsStart = c.DateTime(nullable: false),
                        Comment = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Applications",
                c => new
                    {
                        ApplicationId = c.Guid(nullable: false),
                        ApplicationName = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 250),
                    })
                .PrimaryKey(t => t.ApplicationId);
            
            CreateIndex("dbo.RolesUsers", "Users_UserId");
            CreateIndex("dbo.RolesUsers", "Roles_RoleId");
            CreateIndex("dbo.UsersInRoles", "RoleId");
            CreateIndex("dbo.UsersInRoles", "UserId");
            CreateIndex("dbo.Roles", "ApplicationId");
            CreateIndex("dbo.Profiles", "UserId");
            CreateIndex("dbo.Files", "UserId");
            CreateIndex("dbo.Users", "ApplicationId");
            CreateIndex("dbo.Memberships", "ApplicationId");
            CreateIndex("dbo.Memberships", "UserId");
            AddForeignKey("dbo.Users", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Roles", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.Memberships", "ApplicationId", "dbo.Applications", "ApplicationId");
            AddForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.RolesUsers", "Users_UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.RolesUsers", "Roles_RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
            AddForeignKey("dbo.Profiles", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Memberships", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Files", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
        }
    }
}
