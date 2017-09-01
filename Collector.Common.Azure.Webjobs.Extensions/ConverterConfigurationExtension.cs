using Collector.Common.Azure.Webjobs.Extensions.Converter;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.ServiceBus.Messaging;

namespace Collector.Common.Azure.Webjobs.Extensions
{
    public static class ConverterConfigurationExtension
    {
        public static IConverterManager ConfigureJsonConverter(this IConverterManager converterManager)
        {
            var brokeredMessageConverter = new BrokeredMessageConverter<OpenType>();
            converterManager.AddConverter<BrokeredMessage, OpenType>(brokeredMessageConverter.Convert);
            converterManager.AddConverter<OpenType, BrokeredMessage>(brokeredMessageConverter.Convert);
            return converterManager;
        }
    }
}