namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makingextrapickupnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "ExtraPickupDay", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Customers", "ExtraPickupDay", c => c.DateTime(nullable: false));
        }
    }
}
