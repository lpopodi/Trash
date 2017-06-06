namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edittopickup : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Pickups", name: "AppUser_Id", newName: "userId_Id");
            RenameIndex(table: "dbo.Pickups", name: "IX_AppUser_Id", newName: "IX_userId_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Pickups", name: "IX_userId_Id", newName: "IX_AppUser_Id");
            RenameColumn(table: "dbo.Pickups", name: "userId_Id", newName: "AppUser_Id");
        }
    }
}
