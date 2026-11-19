using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int? Price { get; set; }
        public ELessonStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? HtmlLink { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
