namespace MoodDiaryProjectW2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diarymood : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diaries", "DiaryMood", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Diaries", "DiaryMood");
        }
    }
}
