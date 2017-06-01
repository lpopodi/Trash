namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scheduletocustomernotapplicationuser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "UserId_Id" });
            AddColumn("dbo.Schedules", "customer_Id", c => c.Int());
            CreateIndex("dbo.Schedules", "customer_Id");
            AddForeignKey("dbo.Schedules", "customer_Id", "dbo.Customers", "Id");
            DropColumn("dbo.Schedules", "UserId_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Schedules", "UserId_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Schedules", "customer_Id", "dbo.Customers");
            DropIndex("dbo.Schedules", new[] { "customer_Id" });
            DropColumn("dbo.Schedules", "customer_Id");
            CreateIndex("dbo.Schedules", "UserId_Id");
            AddForeignKey("dbo.Schedules", "UserId_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
