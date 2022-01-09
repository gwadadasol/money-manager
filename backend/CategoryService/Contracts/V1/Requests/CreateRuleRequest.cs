using CategoryService.Domains.Dtos;
using MediatR;

namespace CategoryService.Contracts.V1.Requests
{
    public class CreateRuleRequest : IRequest<RuleDto>
    {
        public RuleDto Rule { get; set; }

    }
}