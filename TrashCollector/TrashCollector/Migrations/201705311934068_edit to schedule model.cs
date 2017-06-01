namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edittoschedulemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "Id" });
            AddColumn("dbo.Schedules", "UserId_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Schedules", "UserId_Id");
            AddForeignKey("dbo.Schedules", "UserId_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "UserId_Id" });
            DropColumn("dbo.Schedules", "UserId_Id");
            CreateIndex("dbo.Schedules", "Id");
            AddForeignKey("dbo.Schedules", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
