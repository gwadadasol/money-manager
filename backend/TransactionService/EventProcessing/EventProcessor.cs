using System;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Domains.Dtos;

namespace TransactionService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory,
        IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DertermineEvent(message);

            switch (eventType)
            {
                case EventType.RulePublished:
                    Console.WriteLine("--> Process Event RulePublished ");
                    break;
                default:
                    break;
            }
        }

        private EventType DertermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determine event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "New_rule_published":
                    Console.WriteLine("--> New rule published event detected");
                    return EventType.RulePublished;
                default:
                    Console.WriteLine("--> Unknown Event");
                    return EventType.Undefined;
            }
        }
    }

    enum EventType
    {
        RulePublished,
        Undefined
    }
}