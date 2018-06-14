using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class ServiceBusQueueClient
    {
        private string ServiceBusConnectionString = System.Configuration.ConfigurationSettings.AppSettings["StorageConnectionString"].ToString();
        const string QueueName = "myservicebusqueue";
        QueueClient _client;

        public ServiceBusQueueClient()
        {
            _client = QueueClient.CreateFromConnectionString(ServiceBusConnectionString, QueueName, ReceiveMode.PeekLock);

        }

        public async Task ProcessServiceBus()
        {
            await this.sendMessage(_client);
            await this.RecieveMessage(_client);

        }

        private async Task sendMessage(QueueClient client)
        {
            for (var i = 0; i < 10; i++)
            {
                BrokeredMessage msg = new BrokeredMessage("message " + Convert.ToString(i));
                await client.SendAsync(msg);
                Console.WriteLine("message " + Convert.ToString(i) + " sent");
            }
        }
        private async Task<bool> RecieveMessage(QueueClient client)
        {

            BrokeredMessage message = await client.ReceiveAsync();
            if (message != null)
            {
                string body = message.GetBody<string>();
                await message.CompleteAsync();
                await message.AbandonAsync();
                Console.WriteLine(body);
                return await this.RecieveMessage(client);
            }

            return false;

        }
    }
}
