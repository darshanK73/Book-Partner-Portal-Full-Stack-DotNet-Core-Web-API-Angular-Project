using Azure.Messaging.EventGrid;
using Azure;
using Azure.Storage.Blobs;
using Book_Portal_API.Models;
using Newtonsoft.Json;
using System.Text;
using Book_Portal_API.Payloads;

namespace Book_Portal_API.Helpers
{
    public class TitlePublishHelper
    {
        //public static async Task<bool> PublishTitleToStorageContainer(IConfiguration config, Title title)
        //{
        //    string blobConnString = config.GetConnectionString("StorAccConnString");

        //    BlobServiceClient client = new BlobServiceClient(blobConnString);

        //    string container = config.GetValue<string>("Container");

        //    var containerClient = client.GetBlobContainerClient(container);

        //    string fileName = "title" + Guid.NewGuid().ToString() + ".json";
        //    // Get a reference to a blob
        //    BlobClient blobClient = containerClient.GetBlobClient(fileName);

        //    //memorystream
        //    using (var stream = new MemoryStream())
        //    {
        //        var serializer = JsonSerializer.Create(new JsonSerializerSettings());

        //        // Use the 'leave open' option to keep the memory stream open after the stream writer is disposed
        //        using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
        //        {
        //            // Serialize the job to the StreamWriter
        //            serializer.Serialize(writer, title);
        //        }

        //        // Rewind the stream to the beginning
        //        stream.Position = 0;

        //        // Upload the job via the stream
        //        await blobClient.UploadAsync(stream, overwrite: true);
        //    }

        //    await PublishToEventGrid(config, title);
        //    return true;
        //}

        public static async Task<bool> PublishTitleToStorageContainer(IConfiguration config, TitlePublishRequest title)
        {
            string blobConnString = config.GetConnectionString("StorAccConnString");

            BlobServiceClient client = new BlobServiceClient(blobConnString);

            string container = config.GetValue<string>("Container");

            var containerClient = client.GetBlobContainerClient(container);

            string fileName = "title" + Guid.NewGuid().ToString() + ".json";
            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            //memorystream
            using (var stream = new MemoryStream())
            {
                var serializer = JsonSerializer.Create(new JsonSerializerSettings());

                // Use the 'leave open' option to keep the memory stream open after the stream writer is disposed
                using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
                {
                    // Serialize the job to the StreamWriter
                    serializer.Serialize(writer, title);
                }

                // Rewind the stream to the beginning
                stream.Position = 0;

                // Upload the job via the stream
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            await PublishToEventGrid(config, title);
            return true;
        }

        private static async Task PublishToEventGrid(IConfiguration config, TitlePublishRequest title)
        {
            var endpoint = config.GetValue<string>("EventGridTopicEndpoint");
            var accessKey = config.GetValue<string>("EventGridAccessKey");

            EventGridPublisherClient client = new EventGridPublisherClient(new Uri(endpoint), new AzureKeyCredential(accessKey));

            var event1 = new EventGridEvent("BookPortal", "BookPortal.TitleEvent", "1.0", JsonConvert.SerializeObject(title));

            event1.Id = (new Guid()).ToString();
            event1.EventTime = DateTime.Now; event1.Topic = config.GetValue<string>("EventGridTopic");
            List<EventGridEvent> eventsList = new List<EventGridEvent> { event1 };

            // Send the events
            await client.SendEventsAsync(eventsList);
        }
    }
}
