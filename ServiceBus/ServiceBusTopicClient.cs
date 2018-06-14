using Microsoft.ServiceBus;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBus
{
    public class ServiceBusTopicClient
    {
        private string ServiceBusConnectionString = System.Configuration.ConfigurationSettings.AppSettings["StorageConnectionString"].ToString();
        const string topicName = "mytopic";
        NamespaceManager namespacemanager;
        const string subscriber = "subscriber1";
        const string filteredsubscriber = "filteredsubscriber";
        TopicClient _topicclient;
        SubscriptionClient subc;

        public ServiceBusTopicClient()
        {
            namespacemanager = NamespaceManager.CreateFromConnectionString(ServiceBusConnectionString);
            _topicclient = TopicClient.CreateFromConnectionString(ServiceBusConnectionString, topicName);
            if (!namespacemanager.SubscriptionExists(topicName, filteredsubscriber))
            {
                namespacemanager.CreateSubscription(topicName, subscriber);
            }

            subc = SubscriptionClient.CreateFromConnectionString(ServiceBusConnectionString, topicName, subscriber);


        }


        public async Task FilteredMessages()
        {
            namespacemanager.DeleteSubscription(topicName, filteredsubscriber);
            if (!await namespacemanager.SubscriptionExistsAsync(topicName, filteredsubscriber))
            {
                var filterersubsc = namespacemanager.CreateSubscription(topicName, filteredsubscriber, new SqlFilter("author = 'gaurav'"));
            }

            var subc1 = SubscriptionClient.CreateFromConnectionString(ServiceBusConnectionString, topicName, filteredsubscriber);
            //var message = await subc1.ReceiveAsync();
            var message = subc1.Receive();
            if (message != null)
            {
                string body = message.GetBody<string>();
                await message.CompleteAsync();
                await message.AbandonAsync();
                Console.WriteLine(body);
                await this.FilteredMessages();
            }
        }

        public async Task ReadMessages()
        {
            var message = await subc.ReceiveAsync();
            if (message != null)
            {
                string body = message.GetBody<string>();
                await message.CompleteAsync();
                await message.AbandonAsync();
                Console.WriteLine(body);
                await this.ReadMessages();
            }
        }

        public async Task SendMessages()
        {
            try
            {
                for (var i = 20; i < 30; i++)
                {
                    BrokeredMessage msg = new BrokeredMessage("message " + i);
                    //msg.Properties["count"] = i;
                    msg.Properties["author"] = "gaurav";
                    await _topicclient.SendAsync(msg);
                    Console.WriteLine("message " + i + " sent");
                }

                //await this.ReadMessages();
                await this.FilteredMessages();

            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
