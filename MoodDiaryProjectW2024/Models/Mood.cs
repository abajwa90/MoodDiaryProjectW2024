using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoodDiaryProjectW2024.Models
{
    public class Mood
    {
        /// <summary>
        /// what describes mood
        /// numerical scale for mood measurement (1-10, 1 = most negative 10 = most positive
        /// </summary>
        [Key]
        public int MoodId { get; set; } //indicates this is the primary key
        public int MoodNum { get; set; }
        public DateTime MoodDay { get; set; }


        /// <summary>
        /// sets the diaryId as a foreign key in the moods table
        /// 1 to many relationship (1 diary has many mood inputs)
        /// </summary>
        [ForeignKey("Diary")] 
        public int DiaryId { get; set; }
        public virtual Diary Diary { get; set; }
    }

    public class MoodDto
    {
        public int MoodId { get; set; }
        public int MoodNum { get; set; }
        public DateTime MoodDay { get; set; }
        public int DiaryId { get; set; }
    }
}