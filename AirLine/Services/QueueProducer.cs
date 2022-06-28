using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineApiService.Services
{
    class QueueProducer
    {
        public static void SetTicketActiveFlag(int airlineRecId, bool isActive)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare("service-schedules-consumer",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var message = new { Method = "SetTicketActiveFlag", AirlineRecId = airlineRecId, IsActive =  isActive};
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "service-schedules-consumer", null, body);
        }
    }
}
