namespace MoodDiaryProjectW2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class diaryweather : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Diaries", "DiaryWeather", c => c.Int(nullable: false));
            AlterColumn("dbo.Diaries", "DiaryMood", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Diaries", "DiaryMood", c => c.String());
            DropColumn("dbo.Diaries", "DiaryWeather");
        }
    }
}
