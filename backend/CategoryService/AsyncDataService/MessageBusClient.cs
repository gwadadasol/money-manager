using System;
using System.Text;
using System.Text.Json;
using CategoryService.Domains.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace CategoryService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        private readonly ILogger<MessageBusClient> _logger;

        public MessageBusClient(IConfiguration configuration, ILogger<MessageBusClient> logger)
        {
            _configuration = configuration;
            _logger =  logger;

            var factory = new ConnectionFactory()
            {
                HostName = _configuration["RabbitMQHost"],
                Port = int.Parse(_configuration["RabbitMQPort"])
            };

            try
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_Connectionhutdown;

                _logger.LogTrace("--> Connected to Message bus");
            }
            catch (Exception ex)
            {
                _logger.LogError($"--> Could not connect to the message bus: {ex.Message}");
            }
        }
        public void PublisheNewCategoryRule(RulePublishedDto rulePublishedDto)
        {
            var message = JsonSerializer.Serialize(rulePublishedDto);

            if (_connection.IsOpen)
            {
                _logger.LogTrace("--> Rabbit conection is open, sendin message...");
                SendMessage(message);
            }
            else
            {
                _logger.LogWarning("--> RabbitMQ connection is closed, not sending");
            }
        }

        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "trigger",
            routingKey: "",
            basicProperties: null,
            body: body);

            _logger.LogTrace($"--> We have sent {message}");
        }

        public void Dispose()
        {
            _logger.LogTrace("Message bus disposed");

            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();

            }
        }
        private void RabbitMQ_Connectionhutdown(object sender, ShutdownEventArgs e)
        {
            _logger.LogTrace("--> RabbitMQ Connection Shutdown");
        }
    }
}