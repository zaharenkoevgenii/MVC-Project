namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users");
            DropIndex("dbo.UsersInRoles", new[] { "RoleId" });
            DropIndex("dbo.UsersInRoles", new[] { "UserId" });
            DropTable("dbo.UsersInRoles");

            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Guid(nullable: false),
                        FileName = c.String(nullable: false, maxLength: 50),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsersInRoles",
                c => new
                    {
                        RoleId = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId });
            
            //DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            //DropForeignKey("dbo.Files", "UserId", "dbo.Users");
            //DropIndex("dbo.Files", new[] { "UserId" });
            //DropIndex("dbo.Users", new[] { "RoleId" });
            //DropTable("dbo.Files");
            CreateIndex("dbo.UsersInRoles", "UserId");
            CreateIndex("dbo.UsersInRoles", "RoleId");
            AddForeignKey("dbo.UsersInRoles", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.UsersInRoles", "RoleId", "dbo.Roles", "RoleId", cascadeDelete: true);
        }
    }
}
