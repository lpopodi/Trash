namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editstoschedulemodel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "customer_Id", "dbo.Customers");
            DropIndex("dbo.Schedules", new[] { "customer_Id" });
            DropColumn("dbo.Schedules", "customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "customer_Id", c => c.Int());
            CreateIndex("dbo.Schedules", "customer_Id");
            AddForeignKey("dbo.Schedules", "customer_Id", "dbo.Customers", "Id");
        }
    }
}
