using Scheduler.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class StudentLog
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public string Value { get; set; }
        public EKindLog Kind { get; set; }
        public ELogStatus? Status { get; set; }
    }
}
