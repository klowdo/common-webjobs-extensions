using System.IO;
using System.Text;
using System.Threading.Tasks;
using Collector.Common.Azure.Webjobs.Extensions.Exceptions;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace Collector.Common.Azure.Webjobs.Extensions
{
    public static class BrokeredMessageExtensions
    {
        private const string NameType = nameof(NameType);
        private const string FullNameType = nameof(FullNameType);
        private const string ApplicationJson = "application/json";

        public static T Parse<T>(this BrokeredMessage message)
        {
            if (message.ContentType != ApplicationJson) throw new ParseException($"Message is not content type: {ApplicationJson}");
            var parseType = typeof(T);
            if (message.Properties.TryGetValue(FullNameType, out object value) && (string)value != parseType.FullName)
                throw new ParseException("Type does not match");
            var stream = message.GetBody<Stream>();
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                var json = streamReader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public static async Task<T> ParseAsync<T>(this BrokeredMessage message)
        {
            if (message.ContentType != ApplicationJson) throw new ParseException($"Message is not content type: {ApplicationJson}");
            var parseType = typeof(T);
            if (message.Properties.TryGetValue(FullNameType, out object value) && (string)value != parseType.FullName)
                throw new ParseException("Type does not match");
            var stream = message.GetBody<Stream>();
            using (var streamReader = new StreamReader(stream, Encoding.UTF8))
            {
                var json = await streamReader.ReadToEndAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        public static BrokeredMessage CreateBrokeredMessage<T>(this T obj) where T : class
        {
            var json = JsonConvert.SerializeObject(obj);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json), writable: false);
            var message = new BrokeredMessage(stream)
            {
                ContentType = "application/json",
                Properties =
                {
                    [FullNameType] = typeof(T).FullName,
                    [NameType] = typeof(T).Name
                }
            };
            return message;
        }
    }
}