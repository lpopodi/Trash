namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addscheduletocustomermodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schedules", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Schedules", "Customer_Id");
            AddForeignKey("dbo.Schedules", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Schedules", new[] { "Customer_Id" });
            DropColumn("dbo.Schedules", "Customer_Id");
        }
    }
}
