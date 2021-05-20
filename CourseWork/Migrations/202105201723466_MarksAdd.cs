namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MarksAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        MarkId = c.Int(nullable: false, identity: true),
                        MarkName = c.String(),
                    })
                .PrimaryKey(t => t.MarkId);
            
            AddColumn("dbo.Parts", "MarkId", c => c.Int(nullable: false));
            CreateIndex("dbo.Parts", "MarkId");
            AddForeignKey("dbo.Parts", "MarkId", "dbo.Marks", "MarkId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parts", "MarkId", "dbo.Marks");
            DropIndex("dbo.Parts", new[] { "MarkId" });
            DropColumn("dbo.Parts", "MarkId");
            DropTable("dbo.Marks");
        }
    }
}
