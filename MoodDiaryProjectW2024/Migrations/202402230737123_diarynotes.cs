namespace MoodDiaryProjectW2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diarynotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diaries", "DiaryNotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Diaries", "DiaryNotes");
        }
    }
}
