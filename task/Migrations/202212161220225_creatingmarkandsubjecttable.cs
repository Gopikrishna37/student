namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class creatingmarkandsubjecttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Marks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubjectId = c.Int(nullable: false),
                        Score = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CoursetName = c.String(),
                        Code = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            AddColumn("dbo.Students", "Username", c => c.String());
            AddColumn("dbo.Students", "Password", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Marks", "StudentId", "dbo.Students");
            DropIndex("dbo.Marks", new[] { "StudentId" });
            DropColumn("dbo.Students", "Password");
            DropColumn("dbo.Students", "Username");
            DropTable("dbo.Subjects");
            DropTable("dbo.Marks");
        }
    }
}
