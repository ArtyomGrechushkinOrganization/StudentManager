using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler.Jobs
{
    public class LessonPlannerJob : IJob
    {
        private readonly ILogger<LessonPlannerJob> logger;
        private readonly IOptions<SchedulerSettings> options;

        public LessonPlannerJob(ILogger<LessonPlannerJob> logger, IOptions<SchedulerSettings> options)
        {
            this.logger = logger;
            this.options = options;
        }

        

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if(DateTime.Now >= options.Value.SchedulerStartTime && DateTime.Now <= options.Value.SchedulerEndTime)
            {
                logger.Log(LogLevel.Information, "Check");
            }
            else
            {
                logger.Log(LogLevel.Information, "Timeout");
            }
            return;
        }
    }
}
