namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fileStoreinDB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "File", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "File");
        }
    }
}
