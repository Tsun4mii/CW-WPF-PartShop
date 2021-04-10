namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        CartId = c.Int(nullable: false, identity: true),
                        PartId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CartId);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        PartId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Quantity = c.Int(nullable: false),
                        ProviderId = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        Description = c.String(),
                        ImageLink = c.String(),
                        CategoryId = c.Int(nullable: false),
                        Cart_CartId = c.Int(),
                    })
                .PrimaryKey(t => t.PartId)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Providers", t => t.ProviderId, cascadeDelete: true)
                .ForeignKey("dbo.Carts", t => t.Cart_CartId)
                .Index(t => t.ProviderId)
                .Index(t => t.CategoryId)
                .Index(t => t.Cart_CartId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        ProviderId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ProviderId);
            
            CreateTable(
                "dbo.Deliveries",
                c => new
                    {
                        DeliveryId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.DeliveryId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        CartId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        DeliveryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Carts", t => t.CartId, cascadeDelete: true)
                .ForeignKey("dbo.Deliveries", t => t.DeliveryId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.UserId)
                .Index(t => t.DeliveryId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Is_admin = c.Boolean(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "UserId", "dbo.Users");
            DropForeignKey("dbo.Orders", "DeliveryId", "dbo.Deliveries");
            DropForeignKey("dbo.Orders", "CartId", "dbo.Carts");
            DropForeignKey("dbo.Parts", "Cart_CartId", "dbo.Carts");
            DropForeignKey("dbo.Parts", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Parts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Orders", new[] { "DeliveryId" });
            DropIndex("dbo.Orders", new[] { "UserId" });
            DropIndex("dbo.Orders", new[] { "CartId" });
            DropIndex("dbo.Parts", new[] { "Cart_CartId" });
            DropIndex("dbo.Parts", new[] { "CategoryId" });
            DropIndex("dbo.Parts", new[] { "ProviderId" });
            DropTable("dbo.Users");
            DropTable("dbo.Orders");
            DropTable("dbo.Deliveries");
            DropTable("dbo.Providers");
            DropTable("dbo.Categories");
            DropTable("dbo.Parts");
            DropTable("dbo.Carts");
        }
    }
}
