using Scheduler.Jobs;

namespace Scheduler;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        builder.Services.AddHostedService<JobScheduler>();
        builder.Services.AddJob<DefaultTimerJob, DefaultTimerOptions>(builder.Configuration);
        builder.Services.AddJob<LessonPlannerJob, SchedulerSettings>(builder.Configuration);

        builder.Services.Configure<SchedulerSettings>(builder.Configuration.GetSection("SchedulerSettings"));

        var host = builder.Build();
        host.Run();
    }
}