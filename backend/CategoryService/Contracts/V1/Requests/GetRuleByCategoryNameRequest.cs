using CategoryService.Domains.Dtos;
using MediatR;

namespace CategoryService.Contracts.V1.Requests
{
    public class GetRuleByCategoryNameRequest : IRequest<RuleDto>
    {
        public string Category { get; set; }
    }
}