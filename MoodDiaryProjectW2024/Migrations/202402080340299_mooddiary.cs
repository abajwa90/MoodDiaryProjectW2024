namespace MoodDiaryProjectW2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mooddiary : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Weathers", "DiaryId", "dbo.Diaries");
            DropIndex("dbo.Weathers", new[] { "DiaryId" });
            RenameColumn(table: "dbo.Weathers", name: "DiaryId", newName: "Diary_DiaryId");
            AlterColumn("dbo.Weathers", "Diary_DiaryId", c => c.Int());
            CreateIndex("dbo.Weathers", "Diary_DiaryId");
            AddForeignKey("dbo.Weathers", "Diary_DiaryId", "dbo.Diaries", "DiaryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Weathers", "Diary_DiaryId", "dbo.Diaries");
            DropIndex("dbo.Weathers", new[] { "Diary_DiaryId" });
            AlterColumn("dbo.Weathers", "Diary_DiaryId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Weathers", name: "Diary_DiaryId", newName: "DiaryId");
            CreateIndex("dbo.Weathers", "DiaryId");
            AddForeignKey("dbo.Weathers", "DiaryId", "dbo.Diaries", "DiaryId", cascadeDelete: true);
        }
    }
}
