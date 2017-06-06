namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refactoringcustomermodeltoincludeschedule : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Schedules", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Schedules", new[] { "Customer_Id" });
            AddColumn("dbo.Customers", "DefaultPickupDay", c => c.DateTime(nullable: false));
            AddColumn("dbo.Customers", "VacationStartDate", c => c.DateTime());
            AddColumn("dbo.Customers", "VacationEndDate", c => c.DateTime());
            AddColumn("dbo.Customers", "BillDate", c => c.DateTime(nullable: false));
            DropTable("dbo.Schedules");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        ScheduleId = c.Guid(nullable: false),
                        DefaultPickupDay = c.DateTime(nullable: false),
                        VacationStartDate = c.DateTime(),
                        VacationEndDate = c.DateTime(),
                        BillDate = c.DateTime(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ScheduleId);
            
            DropColumn("dbo.Customers", "BillDate");
            DropColumn("dbo.Customers", "VacationEndDate");
            DropColumn("dbo.Customers", "VacationStartDate");
            DropColumn("dbo.Customers", "DefaultPickupDay");
            CreateIndex("dbo.Schedules", "Customer_Id");
            AddForeignKey("dbo.Schedules", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
