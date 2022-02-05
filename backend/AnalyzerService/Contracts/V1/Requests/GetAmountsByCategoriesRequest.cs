using AnalyzerService.Domains.Dtos;
using MediatR;

namespace AnalyzerService.Contracts.V1.Requests
{
    public class GetAmountsByCategoriesRequest : IRequest<IEnumerable<CatgoryValueDto>> 
    {
    }
}

