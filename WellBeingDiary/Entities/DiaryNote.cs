using System.ComponentModel.DataAnnotations.Schema;

namespace WellBeingDiary.Entities
{
    public class DiaryNote
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public string Title { get; set; }    
        public string? Text { get; set; }
        public bool IsPublic { get; set; } = false;

        private int _rating;

        public int Rating
        {
            get { return _rating; }
            set
            {
                if (value < 1 || value > 10)
                {
                    throw new ArgumentOutOfRangeException("Rating must be between 1 and 10.");
                }
                _rating = value;
            }
        }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DiaryNote()
        {
            Title = $"Note from {CreationDate:yyyy-MM-dd HH:mm:ss}";
        }

    }
}
