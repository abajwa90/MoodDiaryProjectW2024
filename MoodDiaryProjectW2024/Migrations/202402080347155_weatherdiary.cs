namespace MoodDiaryProjectW2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class weatherdiary : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Weathers", "Diary_DiaryId", "dbo.Diaries");
            DropIndex("dbo.Weathers", new[] { "Diary_DiaryId" });
            RenameColumn(table: "dbo.Weathers", name: "Diary_DiaryId", newName: "DiaryId");
            AlterColumn("dbo.Weathers", "DiaryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Weathers", "DiaryId");
            AddForeignKey("dbo.Weathers", "DiaryId", "dbo.Diaries", "DiaryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Weathers", "DiaryId", "dbo.Diaries");
            DropIndex("dbo.Weathers", new[] { "DiaryId" });
            AlterColumn("dbo.Weathers", "DiaryId", c => c.Int());
            RenameColumn(table: "dbo.Weathers", name: "DiaryId", newName: "Diary_DiaryId");
            CreateIndex("dbo.Weathers", "Diary_DiaryId");
            AddForeignKey("dbo.Weathers", "Diary_DiaryId", "dbo.Diaries", "DiaryId");
        }
    }
}
