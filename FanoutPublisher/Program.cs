using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace FanoutPublisher
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

            //#region Create Exchange

            //channel.ExchangeDeclare("app.fanout", "fanout", true,false,null);

            //#endregion

            //#region Create Queue

            //channel.QueueDeclare("app.fanout.q1", true, false, false, null);
            //channel.QueueDeclare("app.fanout.q2", true, false, false, null);

            //#endregion

            //#region Connect Queue to Exchange

            //channel.QueueBind("app.fanout.q1", "app.fanout","");
            //channel.QueueBind("app.fanout.q2", "app.fanout", "");

            //#endregion

            //channel.BasicPublish("app.fanout","",null,Encoding.UTF8.GetBytes("This is Test from message 1"));
            //channel.BasicPublish("app.fanout", "",null,Encoding.UTF8.GetBytes("This is Test from message 2"));

            #region Delete Queue and Exchange

            channel.QueueDelete("app.fanout.q1");
            channel.QueueDelete("app.fanout.q2");
            channel.ExchangeDelete("app.fanout");

            #endregion




            channel.Close();
            connection.Close();


            Console.WriteLine("Done");
            Console.ReadKey();





        }
    }
}
