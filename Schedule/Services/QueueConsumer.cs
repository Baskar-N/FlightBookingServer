using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ScheduleApiService.Services
{
    public class QueueConsumer : BackgroundService
    {
        private readonly ILogger _logger;
        private IConnection _connection;
        private IModel _channel;

        private IScheduleRepositroy _scheduleRepositroy;

        private readonly IServiceProvider _serviceProvider;

        public QueueConsumer(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            this._logger = loggerFactory.CreateLogger<QueueConsumer>();
            this._serviceProvider = serviceProvider;
            InitRabbitMQ();
        }

        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare("service-schedules-consumer",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            var scope = _serviceProvider.CreateScope();
            _scheduleRepositroy = scope.ServiceProvider.GetService<IScheduleRepositroy>();

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var request = JsonConvert.DeserializeObject<Dictionary<string, object>>(message);

                if (request != null && request.ContainsKey("Method"))
                {
                    switch (request["Method"].ToString())
                    {
                        case "SetTicketActiveFlag":
                            var airlineRecId = Convert.ToInt32(request["AirlineRecId"]);
                            var isActive = Convert.ToBoolean(request["IsActive"]);
                            _scheduleRepositroy.SetTicketActiveFlag(airlineRecId, isActive);
                            break;
                        case "ReduceTicketCount":
                            var scheduleRecId = Convert.ToInt32(request["ScheduleRecId"]);
                            var isBcs = Convert.ToBoolean(request["IsBcs"]);
                            var noOfSeats = Convert.ToInt32(request["NoOfSeats"]);
                            var isDecreaseCount = Convert.ToBoolean(request["IsDecreaseCount"]);

                            if(isDecreaseCount)
                            {
                                _scheduleRepositroy.ReduceTicketCount(scheduleRecId, isBcs, noOfSeats);
                            }
                            else
                            {
                                _scheduleRepositroy.IncreaseTicketCount(scheduleRecId, isBcs, noOfSeats);
                            }
                            break;
                    }
                }
            };

            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerConsumerCancelled;

            _channel.BasicConsume("service-schedules-consumer", true, consumer);

            return Task.CompletedTask;
        }

        private void OnConsumerConsumerCancelled(object sender, ConsumerEventArgs e) { }
        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerRegistered(object sender, ConsumerEventArgs e) { }
        private void OnConsumerShutdown(object sender, ShutdownEventArgs e) { }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
