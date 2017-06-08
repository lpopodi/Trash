namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingonetimepickuptocustomermodelthatIforgot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ExtraPickupDay", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "ExtraPickupDay");
        }
    }
}
