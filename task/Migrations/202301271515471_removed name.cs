namespace task.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedname : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Students", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Students", "Name", c => c.String());
        }
    }
}
