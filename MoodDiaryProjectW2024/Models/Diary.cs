using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoodDiaryProjectW2024.Models
{
    public class Diary
    {
        //what describes a diary
        [Key]
        public int DiaryId { get; set; } //indicates this is the primary key
        public string DiaryName { get; set; }
        public DateTime DiaryCreation { get; set; }

        /// <summary>
        /// sets the mood class as a data collection in the diary class
        /// </summary>
        public ICollection<Mood> Moods { get; set; }

        // <summary>
        /// sets the weather class as a data collection in the diary class
        /// </summary>
        public ICollection<Weather> Weathers { get; set; }
    }

    public class DiaryDto
    {
        public int DiaryId { get; set; } 
        public string DiaryName { get; set; }
        public DateTime DiaryCreation { get; set; }
    }
}