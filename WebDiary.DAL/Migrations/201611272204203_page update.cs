namespace WebDiary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pageupdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PageInfo_TotalPages", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PageInfo_TotalPages");
        }
    }
}
