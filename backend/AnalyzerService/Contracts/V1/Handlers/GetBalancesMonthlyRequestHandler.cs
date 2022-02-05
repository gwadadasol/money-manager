using AnalyzerService.Contracts.V1.Requests;
using AnalyzerService.Domains.Dtos;
using AnalyzerService.Domains.Repository;
using AutoMapper;
using MediatR;

namespace AnalyzerService.Contracts.V1.Handlers
{
    public class GetBalancesMonthlyRequestHandler : IRequestHandler<GetBalancesMonthlyRequest, IEnumerable<BalanceMonthlyDto>>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _repository;

        public GetBalancesMonthlyRequestHandler(IMapper mapper, ITransactionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public Task<IEnumerable<BalanceMonthlyDto>> Handle(GetBalancesMonthlyRequest request, CancellationToken cancellationToken)
        {
             var transactionsEnt = _repository.GetAllTransactions();
         List<TransactionDto> transactions = new ();
         foreach ( var t in transactionsEnt)
         {
             transactions.Add(_mapper.Map<TransactionDto>(t));

         }
         
         var data = transactions
         .GroupBy(t => new {t.Date.Year, t.Date.Month})
         .Select(v => new BalanceMonthlyDto{
             Year = v.Key.Year,
             Month = v.Key.Month, 
             Value = v.Sum( s => s.Amount)
         });

         foreach (var v in data)
         {
             Console.WriteLine($"{v.Year} - {v.Month} => {v.Value}");
         }

         return  Task.FromResult(data) ;
        }
    }
}