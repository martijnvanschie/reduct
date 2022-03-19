using Azure;
using Azure.Messaging.EventGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reduct.Azure.EventGrid
{
    public class EventPublisher
    {
        EventGridPublisherClient client;

        public EventPublisher(string topicName, string region, string accessKey)
        {
            client = new EventGridPublisherClient(
                new Uri($"https://{topicName}.{region}.eventgrid.azure.net/api/events"),
                new AzureKeyCredential(accessKey)
                );
        }

        public async Task PublishBinaryDataAsync(BinaryData eventBinary, string? eventId = null)
        {
            var eventData = EventGridEvent.Parse(eventBinary);

            if (eventId != null)
            {
                eventData.Id = eventId;
            }

            await client.SendEventAsync(eventData);
        }
    }
}
