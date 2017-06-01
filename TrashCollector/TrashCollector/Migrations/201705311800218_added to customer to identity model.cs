namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedtocustomertoidentitymodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(),
                        State = c.String(),
                        ZipCode = c.String(nullable: false, maxLength: 5),
                        Phone = c.String(maxLength: 10),
                        Email = c.String(),
                        userId_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.userId_Id)
                .Index(t => t.userId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "userId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Customers", new[] { "userId_Id" });
            DropTable("dbo.Customers");
        }
    }
}
