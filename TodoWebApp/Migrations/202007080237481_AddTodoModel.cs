namespace TodoWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTodoModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TodoTable",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Content = c.String(nullable: false, maxLength: 1024),
                        IsDone = c.Boolean(nullable: false, defaultValue: false),
                        DateAdded = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TodoTable");
        }
    }
}
