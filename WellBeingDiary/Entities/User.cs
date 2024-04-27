using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WellBeingDiary.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [PasswordPropertyText]
        public required string Password { get; set; }
        public List<DiaryNote>? DiaryNotes { get; set; }
    }
}
