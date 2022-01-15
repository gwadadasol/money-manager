using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CategoryService.AsyncDataServices;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Model;
using CategoryService.Domains.Repository;
using MediatR;

namespace CategoryService.Contracts.V1.Handlers
{
    public class CreateRuleRequestHandler : IRequestHandler<CreateRuleRequest, RuleDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMessageBusClient _messageBusClient;

        public CreateRuleRequestHandler(ICategoryRepository repository, IMapper mapper, IMessageBusClient messageBusClient)
        {
            _repository = repository;
            _mapper = mapper;
            _messageBusClient = messageBusClient;

        }

        public async Task<RuleDto> Handle(CreateRuleRequest request, CancellationToken cancellationToken)
        {
            var rule = _mapper.Map<RuleEntity>(request.Rule);
        
            _repository.CreateRule(rule);
            await _repository.SaveChangesAsync();
            var ruleDto = _mapper.Map<RuleDto>(rule);

            var rulePublishedDto = _mapper.Map<RulePublishedDto>(ruleDto);
            rulePublishedDto.Event = "New_rule_published";
            _messageBusClient.PublisheNewCategoryRule( rulePublishedDto);
            return  ruleDto;
        }
    }
}