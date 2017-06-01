namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edittocustomermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Schedule_Id", c => c.Guid());
            CreateIndex("dbo.Customers", "Schedule_Id");
            AddForeignKey("dbo.Customers", "Schedule_Id", "dbo.Schedules", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "Schedule_Id", "dbo.Schedules");
            DropIndex("dbo.Customers", new[] { "Schedule_Id" });
            DropColumn("dbo.Customers", "Schedule_Id");
        }
    }
}
