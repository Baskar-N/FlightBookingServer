using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApiService.Services
{
    public class QueueProducer
    {
        public static void UpdateTicketCount(int scheduleRecId, bool IsBcs, int noOfSeats, bool isDecreaseCount = false)
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

            var message = new { 
                    Method = "ReduceTicketCount", 
                    ScheduleRecId = scheduleRecId, 
                    IsBcs = IsBcs,
                    NoOfSeats = noOfSeats,
                    IsDecreaseCount = isDecreaseCount
            };

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

            channel.BasicPublish("", "service-schedules-consumer", null, body);
        }
    }
}
