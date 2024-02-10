using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoodDiaryProjectW2024.Models
{
    public class Weather
    {
        
        /// <summary>
        /// this class is to describe the weather input class
        /// </summary>
        [Key]
        public int WeatherId { get; set; } //indicates this is the primary key
        public int WeatherTemperature { get; set; } //to indicate the daily temperature
        public bool WeatherSun {  get; set; } // to indicate whether it is sunny
        public int WeatherClouds { get; set; }// to indicate how cloudy the day is
        public bool WeatherPrecip {  get; set; }// to indicate the daily precipiation (whether it is rainy/snowy or not)
        public DateTime WeatherDay { get; set; } //to indicate the date

        /// <summary>
        /// sets the diaryId as a foreign key in the weather table
        /// 1 to many relationship (1 diary has many weather inputs)
        /// </summary>
        [ForeignKey("Diary")]
        public int DiaryId { get; set; }
        public virtual Diary Diary {  get; set; } 
    }

    public class WeatherDto
    {
        public int WeatherId { get; set; } 
        public int WeatherTemperature { get; set; }
        public bool WeatherSun { get; set; } 
        public int WeatherClouds { get; set; }
        public bool WeatherPrecip { get; set; }
        public DateTime WeatherDay { get; set; } 
        public int DiaryId { get; set; }
    }
}