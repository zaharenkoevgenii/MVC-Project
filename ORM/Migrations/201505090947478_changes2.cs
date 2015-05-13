namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changes2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FilePrivacy", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "FilePrivacy");
        }
    }
}
