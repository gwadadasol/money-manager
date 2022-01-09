using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Repository;
using MediatR;

namespace CategoryService.Contracts.V1.Handlers
{
    public class GetRuleByCategoryNameRequestHandler : IRequestHandler<GetRuleByCategoryNameRequest, RuleDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetRuleByCategoryNameRequestHandler(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Task<RuleDto> Handle(GetRuleByCategoryNameRequest request, CancellationToken cancellationToken)
        {
              var ruleEnt = _repository.GetRuleByName(request.Category);
                var ruleDto = _mapper.Map<RuleDto>(ruleEnt);
                return Task.FromResult(ruleDto);
        }
    }
}