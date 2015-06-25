namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lightCorrectionsinDB : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RolesUsers");
            AddPrimaryKey("dbo.RolesUsers", new[] { "RoleId", "UserId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RolesUsers");
            AddPrimaryKey("dbo.RolesUsers", new[] { "UserId", "RoleId" });
        }
    }
}
