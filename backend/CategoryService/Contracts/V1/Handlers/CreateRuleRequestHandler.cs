using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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

        public CreateRuleRequestHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RuleDto> Handle(CreateRuleRequest request, CancellationToken cancellationToken)
        {
            var rule = _mapper.Map<RuleEntity>(request.Rule);
        
            _repository.CreateRule(rule);
            await _repository.SaveChangesAsync();
            return _mapper.Map<RuleDto>(rule);
        }
    }
}