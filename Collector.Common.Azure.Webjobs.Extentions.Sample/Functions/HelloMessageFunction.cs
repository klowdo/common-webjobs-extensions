using System.IO;
using Collector.Common.Azure.Webjobs.Extentions.Sample.Messages;
using Microsoft.Azure.WebJobs;

namespace Collector.Common.Azure.Webjobs.Extentions.Sample
{
    internal class HelloMessageFunction
    {
        public void ProcessMessage(
            [ServiceBusTrigger(HelloMessage.Queue)] HelloMessage messageIn,
            TextWriter log
        )
        {
            log.Write(messageIn.Message);
        }
    }
}