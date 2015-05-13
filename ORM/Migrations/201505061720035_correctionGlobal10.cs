namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctionGlobal10 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Memberships", "PasswordSalt", c => c.String(maxLength: 128));
            AlterColumn("dbo.Memberships", "Email", c => c.String(nullable: true, maxLength: 256));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Memberships", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.Memberships", "PasswordSalt", c => c.String(nullable: true, maxLength: 128));
        }
    }
}
