using Microsoft.Extensions.Options;

namespace Scheduler;

public class DefaultTimerJob : IJob
{
    private readonly ILogger<DefaultTimerJob> _logger;
    private readonly IOptions<DefaultTimerOptions> _options;

    public DefaultTimerJob(ILogger<DefaultTimerJob> logger, IOptions<DefaultTimerOptions> options)
    {
        _logger = logger;
        _options = options;
    }

    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.Log(LogLevel.Information, "Tick " + _options.Value.Message);
    }
}