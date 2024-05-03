using System.ComponentModel.DataAnnotations;

namespace WellBeingDiary.Dtos.DiaryNote
{
    public class DiaryNoteDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public bool IsPublic { get; set; } = false;
        [Range(1, 10)]
        public int Rating { get; set; }
    }
}
