namespace ORM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeVirtualToList4 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Files", name: "UserRefId", newName: "UserId");
            RenameColumn(table: "dbo.RolesUsers", name: "Roles_Id", newName: "RoleId");
            RenameColumn(table: "dbo.RolesUsers", name: "Users_Id", newName: "UserId");
            RenameIndex(table: "dbo.Files", name: "IX_UserRefId", newName: "IX_UserId");
            RenameIndex(table: "dbo.RolesUsers", name: "IX_Users_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.RolesUsers", name: "IX_Roles_Id", newName: "IX_RoleId");
            DropPrimaryKey("dbo.RolesUsers");
            AddPrimaryKey("dbo.RolesUsers", new[] { "UserId", "RoleId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.RolesUsers");
            AddPrimaryKey("dbo.RolesUsers", new[] { "Roles_Id", "Users_Id" });
            RenameIndex(table: "dbo.RolesUsers", name: "IX_RoleId", newName: "IX_Roles_Id");
            RenameIndex(table: "dbo.RolesUsers", name: "IX_UserId", newName: "IX_Users_Id");
            RenameIndex(table: "dbo.Files", name: "IX_UserId", newName: "IX_UserRefId");
            RenameColumn(table: "dbo.RolesUsers", name: "UserId", newName: "Users_Id");
            RenameColumn(table: "dbo.RolesUsers", name: "RoleId", newName: "Roles_Id");
            RenameColumn(table: "dbo.Files", name: "UserId", newName: "UserRefId");
        }
    }
}
