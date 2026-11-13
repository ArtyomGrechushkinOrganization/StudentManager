using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class SchedulerSettings: JobOptions
    {
        public string CallendarUrl { get; set; }
        public DateTime SchedulerStartTime { get; set; }
        public TimeSpan Interval { get; set; }
        public DateTime SchedulerEndTime { get; set; }
        public int DaysAgo { get; set; }

    }
}
