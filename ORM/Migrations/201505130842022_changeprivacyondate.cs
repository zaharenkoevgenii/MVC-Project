namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeprivacyondate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "CreationTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Files", "FilePrivacy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "FilePrivacy", c => c.String(maxLength: 50));
            DropColumn("dbo.Files", "CreationTime");
        }
    }
}
