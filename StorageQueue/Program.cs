using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Queue; //
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StorageQueue
{

    public class MyQueueClient
    {
        CloudStorageAccount storageAccount;
        CloudQueueClient queueClient;
        public MyQueueClient()
        {
            if (this.storageAccount == null)
            {
                this.storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            }

            if (this.queueClient == null)
            {
                this.queueClient = storageAccount.CreateCloudQueueClient();
            }
        }

        public async Task ProcessQueue()
        {
            CloudQueue queue = this.queueClient.GetQueueReference("mypracticequeue");
            for (var i = 0; i <= 10; i++)
            {
                CloudQueueMessage cloudQueueMessage = new CloudQueueMessage("My message no." + i.ToString());
                await queue.AddMessageAsync(cloudQueueMessage);
                // Async enqueue the message
                Console.WriteLine("Message "+i.ToString()+" added");
            }



            //// Async dequeue the message
            IEnumerable<CloudQueueMessage> retrievedMessages = await queue.GetMessagesAsync(20);
            if (retrievedMessages != null)
            {

                foreach (var item in retrievedMessages)
                {
                    Console.WriteLine("Retrieved message with content {0}", item.AsString);
                    // Async delete the message
                    await queue.DeleteMessageAsync(item);
                    Console.WriteLine("Deleted message");
                }
            }




        }







    }

    class Program
    {
        static void Main(string[] args)
        {
            MyQueueClient queueProcessor = new MyQueueClient();
            queueProcessor.ProcessQueue();
            Console.Read();
        }
    }
}
