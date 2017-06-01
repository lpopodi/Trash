namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class workingonemployeecontroller : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoices", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Invoices", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.InvoiceDetails",
                c => new
                    {
                        LineId = c.Int(nullable: false, identity: true),
                        LineItem = c.String(),
                        LineDate = c.String(),
                        LinePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Invoice_InvoiceId = c.Guid(),
                    })
                .PrimaryKey(t => t.LineId)
                .ForeignKey("dbo.Invoices", t => t.Invoice_InvoiceId)
                .Index(t => t.Invoice_InvoiceId);
            
            DropColumn("dbo.Invoices", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.InvoiceDetails", "Invoice_InvoiceId", "dbo.Invoices");
            DropIndex("dbo.InvoiceDetails", new[] { "Invoice_InvoiceId" });
            DropTable("dbo.InvoiceDetails");
            CreateIndex("dbo.Invoices", "ApplicationUser_Id");
            AddForeignKey("dbo.Invoices", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
