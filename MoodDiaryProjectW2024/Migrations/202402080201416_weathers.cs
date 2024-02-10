namespace MoodDiaryProjectW2024.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class weathers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Weathers",
                c => new
                    {
                        WeatherId = c.Int(nullable: false, identity: true),
                        WeatherTemperature = c.Int(nullable: false),
                        WeatherSun = c.Boolean(nullable: false),
                        WeatherClouds = c.Int(nullable: false),
                        WeatherPrecip = c.Boolean(nullable: false),
                        WeatherDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.WeatherId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Weathers");
        }
    }
}
