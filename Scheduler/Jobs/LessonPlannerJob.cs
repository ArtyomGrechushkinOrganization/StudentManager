using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Scheduler.Jobs
{
    public class LessonPlannerJob : IJob
    {
        private readonly ILogger<LessonPlannerJob> logger;
        private readonly IOptions<SchedulerSettings> options;
        private readonly IHttpClientFactory httpClientFactory;

        public LessonPlannerJob(ILogger<LessonPlannerJob> logger, IOptions<SchedulerSettings> options, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.options = options;
            this.httpClientFactory = httpClientFactory;
        }

        

        public async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if(DateTime.Now >= options.Value.SchedulerStartTime && DateTime.Now <= options.Value.SchedulerEndTime)
            {
                string baseUrl = options.Value.CallendarUrl;

                // Создаем билдер
                var uriBuilder = new UriBuilder(baseUrl);
                var query = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

                // Добавляем параметры
                query["timeMin"] = DateTime.Now.AddDays(-options.Value.DaysAgo).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
                query["timeMax"] = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd'T'HH:mm:ss'Z'", CultureInfo.InvariantCulture);
                query["singleEvents"] = "true"; // Пробелы закодируются автоматически
                query["orderBy"] = "startTime";

                uriBuilder.Query = query.ToString();

                string finalUrl = uriBuilder.ToString();

                logger.Log(LogLevel.Information, "Check");
                using HttpClient client = httpClientFactory.CreateClient();
                //client.BaseAddress = new Uri(finalUrl);
                var jsonSchedule = await client.GetFromJsonAsync<ScheduleDayList>(finalUrl);
                
                
            }
            else
            {
                logger.Log(LogLevel.Information, "Timeout");
            }
            return;
        }
    }
}
