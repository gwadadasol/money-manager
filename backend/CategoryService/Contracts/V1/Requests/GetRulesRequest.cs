using System.Collections.Generic;
using CategoryService.Domains.Dtos;
using MediatR;

namespace CategoryService.Contracts.V1.Requests
{
    public class GetRulesRequest : IRequest<List<RuleDto>>
    {
    }
}