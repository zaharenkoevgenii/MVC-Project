namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctionGlobal5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropIndex("dbo.User", new[] { "RoleId" });
            DropTable("dbo.Role");
            DropTable("dbo.User");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Rolet",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 10, fixedLength: true),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateIndex("dbo.User", "RoleId");
            AddForeignKey("dbo.User", "RoleId", "dbo.Rolet", "RoleId");
        }
    }
}
