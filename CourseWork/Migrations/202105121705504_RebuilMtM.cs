namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RebuilMtM : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PartsInOrder", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.PartsInOrder", "PartId", "dbo.Parts");
            DropIndex("dbo.PartsInOrder", new[] { "OrderId" });
            DropIndex("dbo.PartsInOrder", new[] { "PartId" });
            CreateTable(
                "dbo.OrderedParts",
                c => new
                    {
                        PartId = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PartId, t.OrderId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Parts", t => t.PartId, cascadeDelete: true)
                .Index(t => t.PartId)
                .Index(t => t.OrderId);
            
            DropTable("dbo.PartsInOrder");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PartsInOrder",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        PartId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.PartId });
            
            DropForeignKey("dbo.OrderedParts", "PartId", "dbo.Parts");
            DropForeignKey("dbo.OrderedParts", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderedParts", new[] { "OrderId" });
            DropIndex("dbo.OrderedParts", new[] { "PartId" });
            DropTable("dbo.OrderedParts");
            CreateIndex("dbo.PartsInOrder", "PartId");
            CreateIndex("dbo.PartsInOrder", "OrderId");
            AddForeignKey("dbo.PartsInOrder", "PartId", "dbo.Parts", "PartId", cascadeDelete: true);
            AddForeignKey("dbo.PartsInOrder", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
        }
    }
}
