namespace WebDiary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_isDeleted_inNote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "IsDeleted");
        }
    }
}
