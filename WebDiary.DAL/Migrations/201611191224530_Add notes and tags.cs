namespace WebDiary.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Addnotesandtags : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Message = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Privacy = c.Boolean(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TagNotes",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Note_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Note_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.Notes", t => t.Note_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.Note_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Notes", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagNotes", "Note_Id", "dbo.Notes");
            DropForeignKey("dbo.TagNotes", "Tag_Id", "dbo.Tags");
            DropIndex("dbo.TagNotes", new[] { "Note_Id" });
            DropIndex("dbo.TagNotes", new[] { "Tag_Id" });
            DropIndex("dbo.Notes", new[] { "UserId" });
            DropTable("dbo.TagNotes");
            DropTable("dbo.Tags");
            DropTable("dbo.Notes");
        }
    }
}
