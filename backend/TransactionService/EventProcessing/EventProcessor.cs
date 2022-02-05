using System;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IServiceScopeFactory scopeFactory,
        IMapper mapper,
        IMediator mediator,
        ILogger<EventProcessor> logger)
        {
            _scopeFactory   = scopeFactory;
            _mapper         = mapper;
            _mediator       = mediator;
            _logger         = logger;
        }
        public async void ProcessEvent(string message)
        {
            var eventType = DertermineEvent(message);

            switch (eventType)
            {
                case EventType.RulePublished:
                    _logger.LogTrace("--> Process Event RulePublished ");
                    try
                    {
                        await UpdateCategoryAllTransactions();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogTrace($"--> Exception:{ex.Message}");
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
                    _logger.LogTrace($"transaction.Description: {transaction.Description}");
                    transaction.Category = await _mediator.Send(new GetCategoryRequest { Description = transaction.Description });

                    _logger.LogTrace($"Category: {transaction.Category}");
                    repo.UpdateTransaction(transaction);
                }

                await repo.SaveChangesAsync();

            }
        }

        private EventType DertermineEvent(string notificationMessage)
        {
            _logger.LogTrace("--> Determine event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType.Event)
            {
                case "New_rule_published":
                    _logger.LogTrace("--> New rule published event detected");
                    return EventType.RulePublished;
                default:
                    _logger.LogTrace("--> Unknown Event");
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