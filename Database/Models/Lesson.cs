using Database.Enums;

namespace Database.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int? Price { get; set; }
        public ELessonStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedAt   { get; set; }
        public DateTime EndAt { get; set; }
        public string? HtmlLink { get; set; }
    }
}
