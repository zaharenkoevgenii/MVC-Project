namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctionGlobal7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Applications", "ApplicationName", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Applications", "Description", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Applications", "Description", c => c.String(maxLength: 256));
            AlterColumn("dbo.Applications", "ApplicationName", c => c.String(nullable: false, maxLength: 235));
        }
    }
}
