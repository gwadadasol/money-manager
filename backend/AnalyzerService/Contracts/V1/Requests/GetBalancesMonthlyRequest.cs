using AnalyzerService.Domains.Dtos;
using MediatR;

namespace AnalyzerService.Contracts.V1.Requests
{
    public class GetBalancesMonthlyRequest:IRequest<IEnumerable<BalanceMonthlyDto>>
    {
        
    }
}