namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PartsInOrder : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.Orders", "CartId", "dbo.Carts");
            DropIndex("dbo.Carts", new[] { "PartId" });
            DropIndex("dbo.Orders", new[] { "CartId" });
            CreateTable(
                "dbo.PartsInOrder",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.PartId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Parts", t => t.PartId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.PartId);
            
            DropColumn("dbo.Orders", "CartId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "CartId", c => c.Int(nullable: false));
            DropForeignKey("dbo.PartsInOrder", "PartId", "dbo.Parts");
            DropForeignKey("dbo.PartsInOrder", "OrderId", "dbo.Orders");
            DropIndex("dbo.PartsInOrder", new[] { "PartId" });
            DropIndex("dbo.PartsInOrder", new[] { "OrderId" });
            DropTable("dbo.PartsInOrder");
            CreateIndex("dbo.Orders", "CartId");
            CreateIndex("dbo.Carts", "PartId");
            AddForeignKey("dbo.Orders", "CartId", "dbo.Carts", "CartId", cascadeDelete: true);
            AddForeignKey("dbo.Carts", "PartId", "dbo.Parts", "PartId", cascadeDelete: true);
        }
    }
}
