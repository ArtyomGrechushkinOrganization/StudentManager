using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class ScheduleDayList
    {
        public List<JsonToObjectLesson> Items { get; set; }
        public DateTime Day { get; set; }
    }
}
