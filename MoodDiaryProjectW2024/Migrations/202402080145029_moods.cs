namespace MoodDiaryProjectW2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moods : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Moods",
                c => new
                    {
                        MoodId = c.Int(nullable: false, identity: true),
                        MoodNum = c.Int(nullable: false),
                        MoodDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MoodId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Moods");
        }
    }
}
