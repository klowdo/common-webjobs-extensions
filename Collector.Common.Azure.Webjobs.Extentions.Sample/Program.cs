using Collector.Common.Azure.Webjobs.Extensions;
using Microsoft.Azure.WebJobs;

namespace Collector.Common.Azure.Webjobs.Extentions.Sample
{
    // To learn more about Microsoft Azure WebJobs SDK, please see https://go.microsoft.com/fwlink/?LinkID=320976
    internal class Program
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        private static void Main()
        {
            var config = new JobHostConfiguration();

            if (config.IsDevelopment)
            {
                config.UseDevelopmentSettings();
            }
            config.ConverterManager.ConfigureJsonConverter();
            using (var host = new JobHost(config))
            {
                host.RunAndBlock();
            }
        }
    }
}