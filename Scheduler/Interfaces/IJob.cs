namespace Scheduler;

public interface IJob
{
    Task ExecuteAsync(CancellationToken stoppingToken);
}