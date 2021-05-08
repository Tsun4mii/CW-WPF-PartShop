namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFullDiscPart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parts", "FullDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parts", "FullDescription");
        }
    }
}
