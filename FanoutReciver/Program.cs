using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FanoutReciver
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Connect To RabbitMQ

            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = "localhost";
            factory.VirtualHost = "/";
            factory.Port = 5672;
            factory.UserName = "guest";
            factory.Password = "guest";

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            #endregion

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += Consumer_Received;


            var consumerTag = channel.BasicConsume("class.fanout.queue1", true, consumer);

            Console.WriteLine("Wait For New Message");
            Console.ReadKey();
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Body.ToArray());
            Console.WriteLine("Message Receive :   " + message);
        }
    }
}
