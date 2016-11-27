namespace WebDiary.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddPageInfoForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "PageInfo_PageNumber", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "PageInfo_PageSize", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "PageInfo_TotalItems", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "PageInfo_TotalItems");
            DropColumn("dbo.AspNetUsers", "PageInfo_PageSize");
            DropColumn("dbo.AspNetUsers", "PageInfo_PageNumber");
        }
    }
}
