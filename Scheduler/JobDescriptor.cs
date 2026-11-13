namespace Scheduler;

public record JobDescriptor(Type JobType, string Name, Func<IServiceProvider, JobOptions> GetOptions);
