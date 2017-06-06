namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class schedulemodelchangingidtoscheduleId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.Customers", new[] { "Schedule_Id" });
            DropPrimaryKey("dbo.Schedules");
            AddColumn("dbo.Schedules", "ScheduleId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Schedules", "ScheduleId");
            DropColumn("dbo.Customers", "Schedule_Id");
            DropColumn("dbo.Schedules", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "Id", c => c.Guid(nullable: false));
            AddColumn("dbo.Customers", "Schedule_Id", c => c.Guid());
            DropPrimaryKey("dbo.Schedules");
            DropColumn("dbo.Schedules", "ScheduleId");
            AddPrimaryKey("dbo.Schedules", "Id");
            CreateIndex("dbo.Customers", "Schedule_Id");
            AddForeignKey("dbo.Customers", "Schedule_Id", "dbo.Schedules", "Id");
        }
    }
}
