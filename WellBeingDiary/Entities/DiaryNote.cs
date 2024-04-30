﻿using System.ComponentModel.DataAnnotations;

namespace WellBeingDiary.Entities
{
    public class DiaryNote
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool IsPublic { get; set; } = false;
        [Range(1, 10)]
        public int Rating { get; set; }
    }
}
