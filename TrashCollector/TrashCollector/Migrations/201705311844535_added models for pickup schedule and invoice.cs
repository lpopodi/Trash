namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmodelsforpickupscheduleandinvoice : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Guid(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Pickups",
                c => new
                    {
                        PickupId = c.Int(nullable: false, identity: true),
                        PickupDate = c.DateTime(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.PickupId)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DefaultPickupDay = c.DateTime(nullable: false),
                        VacationStartDate = c.DateTime(),
                        VacationEndDate = c.DateTime(),
                        BillDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Schedules", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pickups", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Pickups", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Invoices", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Invoices", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Schedules", new[] { "Id" });
            DropIndex("dbo.Pickups", new[] { "Customer_Id" });
            DropIndex("dbo.Pickups", new[] { "AppUser_Id" });
            DropIndex("dbo.Invoices", new[] { "Customer_Id" });
            DropIndex("dbo.Invoices", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Schedules");
            DropTable("dbo.Pickups");
            DropTable("dbo.Invoices");
        }
    }
}
