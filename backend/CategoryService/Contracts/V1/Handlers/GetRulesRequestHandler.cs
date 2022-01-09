using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CategoryService.Contracts.V1.Requests;
using CategoryService.Domains.Dtos;
using CategoryService.Domains.Repository;
using MediatR;

namespace CategoryService.Contracts.V1.Handlers
{
    public class GetRulesRequestHandler : IRequestHandler<GetRulesRequest, List<RuleDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;

        public GetRulesRequestHandler(IMapper mapper, ICategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<List<RuleDto>> Handle(GetRulesRequest request, CancellationToken cancellationToken)
        {
            var rulesEnt = _repository.GetAllRules();
            var rulesDto = rulesEnt.Select(c => _mapper.Map<RuleDto>(c)).ToList();

            return Task.FromResult(rulesDto);
        }
    }
}