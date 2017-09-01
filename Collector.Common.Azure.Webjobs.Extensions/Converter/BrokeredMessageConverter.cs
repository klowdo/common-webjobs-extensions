using Microsoft.Azure.WebJobs;
using Microsoft.ServiceBus.Messaging;

namespace Collector.Common.Azure.Webjobs.Extensions.Converter
{
    public class BrokeredMessageConverter<T> :
        IConverter<BrokeredMessage, T> where T : class

    {
        public T Convert(BrokeredMessage input) => input.Parse<T>();

        public BrokeredMessage Convert(T input) => input.CreateBrokeredMessage();
    }
}