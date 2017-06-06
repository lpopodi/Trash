namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removinginvoicemodeladdingaccountbalancetocustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.InvoiceDetails", "Invoice_InvoiceId", "dbo.Invoices");
            DropIndex("dbo.Invoices", new[] { "Customer_Id" });
            DropIndex("dbo.InvoiceDetails", new[] { "Invoice_InvoiceId" });
            AddColumn("dbo.Customers", "AccountBalance", c => c.Decimal(precision: 18, scale: 2));
            DropTable("dbo.Invoices");
            DropTable("dbo.InvoiceDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        LineId = c.String(nullable: false, maxLength: 128),
                        LineItem = c.String(),
                        LineDate = c.String(),
                        LinePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Invoice_InvoiceId = c.Guid(),
                    })
                .PrimaryKey(t => t.LineId);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        InvoiceId = c.Guid(nullable: false),
                        InvoiceDate = c.DateTime(nullable: false),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.InvoiceId);
            
            DropColumn("dbo.Customers", "AccountBalance");
            CreateIndex("dbo.InvoiceDetails", "Invoice_InvoiceId");
            CreateIndex("dbo.Invoices", "Customer_Id");
            AddForeignKey("dbo.InvoiceDetails", "Invoice_InvoiceId", "dbo.Invoices", "InvoiceId");
            AddForeignKey("dbo.Invoices", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
