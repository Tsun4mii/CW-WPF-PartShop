namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CardAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cards", "Balance", c => c.Double(nullable: false));
            CreateIndex("dbo.Cards", "UserId");
            AddForeignKey("dbo.Cards", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cards", "UserId", "dbo.Users");
            DropIndex("dbo.Cards", new[] { "UserId" });
            DropColumn("dbo.Cards", "Balance");
        }
    }
}
