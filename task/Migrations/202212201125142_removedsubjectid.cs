namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedsubjectid : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Marks", "SubjectId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Marks", "SubjectId", c => c.Int(nullable: false));
        }
    }
}
