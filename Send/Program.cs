﻿using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("logs", "fanout");

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    for (int i = 0; i < 10; i++)
                    {
                        string message = i.ToString();
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "logs",
                                     routingKey: "",
                                     basicProperties: properties,
                                     body: body);
                        Console.WriteLine(" [x] Sent {0}", message);
                    }
                }
            }

        }
    }
}
