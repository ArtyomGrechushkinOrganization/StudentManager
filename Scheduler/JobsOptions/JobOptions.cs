namespace Scheduler;

public abstract class JobOptions
{
    public bool IsEnabled { get; set; }
    public TimeSpan Interval { get; set; }
}