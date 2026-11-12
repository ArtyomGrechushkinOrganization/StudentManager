using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class SchedulerSettings
    {
        public string CallendarUrl { get; set; }
        public DateTime SchedulerStartTime { get; set; }
        public TimeSpan Interval { get; set; }
        public DateTime SchedulerEndTime { get; set; }
        public DateTime timeMin { get; set; }

    }
}
