using Database.Data;
using Microsoft.EntityFrameworkCore;
using Scheduler.Jobs;

namespace Scheduler;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        
        builder.Services.AddHostedService<JobScheduler>();
        builder.Services.AddHttpClient();
        builder.Services.AddJob<DefaultTimerJob, DefaultTimerOptions>(builder.Configuration);
        builder.Services.AddJob<LessonPlannerJob, SchedulerSettings>(builder.Configuration);
        
        builder.Services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres")));

        builder.Services.Configure<SchedulerSettings>(builder.Configuration.GetSection("SchedulerSettings"));

        var host = builder.Build();
        host.Run();
    }
}