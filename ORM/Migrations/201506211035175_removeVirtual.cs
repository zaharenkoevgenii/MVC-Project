namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeVirtual : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "CreationTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Files", "CrationTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "CrationTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Files", "CreationTime");
        }
    }
}
