namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedfk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Marks", "SubjectId", c => c.Int(nullable: false));
            CreateIndex("dbo.Marks", "SubjectId");
            AddForeignKey("dbo.Marks", "SubjectId", "dbo.Subjects", "CourseId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "SubjectId", "dbo.Subjects");
            DropIndex("dbo.Marks", new[] { "SubjectId" });
            DropColumn("dbo.Marks", "SubjectId");
        }
    }
}
