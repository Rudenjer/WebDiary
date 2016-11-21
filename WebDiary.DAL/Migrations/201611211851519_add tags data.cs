namespace WebDiary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtagsdata : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TagNotes", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.TagNotes", "Note_Id", "dbo.Notes");
            DropIndex("dbo.TagNotes", new[] { "Tag_Id" });
            DropIndex("dbo.TagNotes", new[] { "Note_Id" });
            AddColumn("dbo.Tags", "Note_Id", c => c.Int());
            CreateIndex("dbo.Tags", "Note_Id");
            AddForeignKey("dbo.Tags", "Note_Id", "dbo.Notes", "Id");
            DropTable("dbo.TagNotes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TagNotes",
                c => new
                    {
                        Tag_Id = c.Int(nullable: false),
                        Note_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.Note_Id });
            
            DropForeignKey("dbo.Tags", "Note_Id", "dbo.Notes");
            DropIndex("dbo.Tags", new[] { "Note_Id" });
            DropColumn("dbo.Tags", "Note_Id");
            CreateIndex("dbo.TagNotes", "Note_Id");
            CreateIndex("dbo.TagNotes", "Tag_Id");
            AddForeignKey("dbo.TagNotes", "Note_Id", "dbo.Notes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TagNotes", "Tag_Id", "dbo.Tags", "Id", cascadeDelete: true);
        }
    }
}
