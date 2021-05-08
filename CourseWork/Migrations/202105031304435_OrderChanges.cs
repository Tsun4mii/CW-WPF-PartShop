namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderState", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "OrderState");
        }
    }
}
