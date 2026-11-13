namespace Scheduler;

public sealed class JobScheduler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IEnumerable<JobDescriptor> _jobs;
    private readonly ILogger<JobScheduler> _logger;

    public JobScheduler
    (
        IServiceProvider serviceProvider,
        IServiceScopeFactory scopeFactory,
        IEnumerable<JobDescriptor> jobs,
        ILogger<JobScheduler> logger
    )
    {
        _serviceProvider = serviceProvider;
        _scopeFactory = scopeFactory;
        _jobs = jobs;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = _jobs.Select(job => RunJobAsync(job, stoppingToken));

        await Task.WhenAll(tasks);
    }

    private async Task RunJobAsync(JobDescriptor descriptor, CancellationToken cancellationToken)
    {
        var options = descriptor.GetOptions(_serviceProvider);

        if (!options.IsEnabled)
        {
            _logger.LogInformation("Job {JobName} is disabled", descriptor.Name);

            return;
        }

        if (options.Interval <= TimeSpan.Zero)
        {
            _logger.LogError("Invalid interval for job {JobName}", descriptor.Name);

            return;
        }

        _logger.LogInformation("Job {JobName} scheduled every {Interval}", descriptor.Name, options.Interval);

        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                await ExecuteJobAsync(descriptor, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Job {JobName} execution failed", descriptor.Name);
            }

            try
            {
                await Task.Delay(options.Interval, cancellationToken);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                break;
            }
        }
    }

    private async Task ExecuteJobAsync(JobDescriptor descriptor, CancellationToken cancellationToken)
    {
        await using var scope = _scopeFactory.CreateAsyncScope();

        var job = (IJob)scope.ServiceProvider.GetRequiredService(descriptor.JobType);

        _logger.LogInformation("Executing job {JobName}", descriptor.Name);

        await job.ExecuteAsync(cancellationToken);

        _logger.LogInformation("Job {JobName} completed", descriptor.Name);
    }
}