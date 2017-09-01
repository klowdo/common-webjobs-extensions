namespace Collector.Common.Azure.Webjobs.Extentions.Sample.Messages
{
    public class HelloMessage
    {
        public static HelloMessage CreateWith(string message) => new HelloMessage { Message = message };

        public string Message { get; set; }

        public const string Queue = nameof(HelloMessage);
    }
}