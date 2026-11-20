using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentPrice { get; set; }
        public List<Lesson> Lessons { get; set; }
        public List<StudentLog> Logs { get; set; }
        public List<FindWord> FindWords { get; set; }
        public bool IsTemporary { get; set; }
    }
}
