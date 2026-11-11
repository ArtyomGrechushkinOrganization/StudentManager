namespace Scheduler;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);
        builder.Services.AddHostedService<Worker>();

        builder.Services.Configure<SchedulerSettings>(builder.Configuration.GetSection("SchedulerSettings"));

        var host = builder.Build();
        host.Run();
    }
}