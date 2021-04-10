namespace CourseWork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
    }
}
