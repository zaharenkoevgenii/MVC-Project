namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApproveFieldToFiles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "Approved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Files", "Approved");
        }
    }
}
