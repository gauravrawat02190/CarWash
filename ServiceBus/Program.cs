using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Microsoft.ServiceBus;

namespace ServiceBus
{
    class Program
    {
        static void Main(string[] args)
        {
            //for queue messages
            //ServiceBusQueueClient client = new ServiceBusQueueClient();
            //client.ProcessServiceBus();

            //for topic messages
            ServiceBusTopicClient topicClient = new ServiceBusTopicClient();
            topicClient.SendMessages();
            Console.ReadLine();
        }
    }
}
