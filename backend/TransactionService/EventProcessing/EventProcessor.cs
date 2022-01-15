using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TransactionService.Contracts.V1;
using TransactionService.Contracts.V1.Requests;
using TransactionService.Domains.Dtos;
using TransactionService.Domains.Repository;

namespace TransactionService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EventProcessor(IServiceScopeFactory scopeFactory,
        IMapper mapper,
        IMediator mediator)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async void ProcessEvent(string message)
        {
            var eventType = DertermineEvent(message);

            switch (eventType)
            {
                case EventType.RulePublished:
                    Console.WriteLine("--> Process Event RulePublished ");
                    try
                    {
                        await UpdateCategoryAllTransactions();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"--> Exception:{ex.Message}");
                    }
                    break;
                default:
                    break;
            }
        }

        private async Task UpdateCategoryAllTransactions()
        {
            /** Get the repository this way
                because cannot use the mediator
                because the scope of the eventProcessor is Singleton 
                whereas the scope of the repository is only Transiant or Scoped. 

                Must use the scope factory to get the repository
            */ 
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ITransactionRepository>();
                foreach (var transaction in repo.GetAllTransactions())
                {
                    // Get category
                    System.Console.WriteLine($"transaction.Description: {transaction.Description}");
                    transaction.Category = await _mediator.Send(new GetCategoryRequest { Description = transaction.Description });
                    repo.UpdateTransaction(transaction);
                }

                await repo.SaveChangesAsync();

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