using System.Text.Json;
using CategoryService.Domains.Dtos;
using Azure.Messaging.ServiceBus;

namespace CategoryService.AsyncDataServices;

public class MessageBusClientAzure : IMessageBusClientAzure
{
    private readonly string _connectionString;
    private readonly string _queueName;

    public MessageBusClientAzure(AzureServiceBusConfiguration configuration)
    {
        _connectionString = configuration.ConnectionString;
        _queueName =  configuration.QueueName;
    }

    public async void PublisheNewCategoryRule(RulePublishedDto rulePublishedDto)
    {
        await using (var client = new ServiceBusClient(_connectionString))
        {
            var sender = client.CreateSender(_queueName);

            var json = JsonSerializer.Serialize(rulePublishedDto);
            var message = new ServiceBusMessage(json);

            await sender.SendMessageAsync(message);
        }
    }
    // public void PublisheNewCategoryRule(RulePublishedDto rulePublishedDto)
    // {
    //     var message = JsonSerializer.Serialize(rulePublishedDto);

    //     if (_connection.IsOpen)
    //     {
    //         Console.WriteLine("--> Rabbit conection is open, sendin message...");
    //         SendMessage(message);
    //     }
    //     else
    //     {
    //         Console.WriteLine("--> RabbitMQ connection is closed, not sending");
    //     }
    // }

    // private void SendMessage(string message)
    // {
    //     var body = Encoding.UTF8.GetBytes(message);

    //     _channel.BasicPublish(exchange: "trigger",
    //     routingKey: "",
    //     basicProperties: null,
    //     body: body);

    //     Console.WriteLine($"--> We have sent {message}");
    // }

    // public void Dispose()
    // {

    //     Console.WriteLine("Message bus disposed");

    //     if (_channel.IsOpen)
    //     {
    //         _channel.Close();
    //         _connection.Close();

    //     }

    // }
    // private void RabbitMQ_Connectionhutdown(object sender, ShutdownEventArgs e)
    // {
    //     Console.WriteLine("--> RabbitMQ Connection Shutdown");
    // }
}
