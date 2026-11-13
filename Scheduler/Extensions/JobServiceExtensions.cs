using Microsoft.Extensions.Options;

namespace Scheduler;

public static class JobServiceExtensions
{
    /// <summary>
    /// Добавляет джобу
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <typeparam name="TJob">Тип джобы</typeparam>
    /// <typeparam name="TOptions">Тип опций джобы</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Выбрасывается когда в appsettings.json не находит секцию с настройками</exception>
    public static IServiceCollection AddJob<TJob, TOptions>(this IServiceCollection services, IConfiguration configuration)
        where TJob : class, IJob
        where TOptions : JobOptions, new()
    {
        var jobName = typeof(TJob).Name;

        if (!configuration.GetSection($"Jobs:{jobName}").Exists())
        {
            throw new ArgumentException($"Options Jobs:{jobName} does not exist.");
        }

        services.AddOptions<TOptions>().BindConfiguration($"Jobs:{jobName}").ValidateOnStart();

        services.AddScoped<TJob>();

        services.AddSingleton(new JobDescriptor(typeof(TJob), jobName,
            sp => sp.GetRequiredService<IOptionsMonitor<TOptions>>().CurrentValue));

        return services;
    }
}