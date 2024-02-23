namespace MoodDiaryProjectW2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class datetimeupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Diaries", "DiaryCreation", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Diaries", "DiaryCreation", c => c.DateTime(nullable: false));
        }
    }
}
