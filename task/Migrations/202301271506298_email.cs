namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class email : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Students", "Email", c => c.String(nullable: false));
            AddColumn("dbo.Students", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Students", "Name");
            DropColumn("dbo.Students", "Email");
        }
    }
}
