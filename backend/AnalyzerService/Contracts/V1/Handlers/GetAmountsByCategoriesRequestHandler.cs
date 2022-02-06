using AnalyzerService.Contracts.V1.Requests;
using AnalyzerService.Domains.Dtos;
using AnalyzerService.Domains.Repository;
using AutoMapper;
using MediatR;

namespace AnalyzerService.Contracts.V1.Handlers;

public class GetAmountsByCategoriesRequestHandler : IRequestHandler<GetAmountsByCategoriesRequest, IEnumerable<CatgoryValueDto>>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _repository;

    public GetAmountsByCategoriesRequestHandler(IMapper mapper, ITransactionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
    public Task<IEnumerable<CatgoryValueDto>> Handle(GetAmountsByCategoriesRequest request, CancellationToken cancellationToken)
    {
         var transactionsEnt = _repository.GetAllTransactions();
         List<TransactionDto> transactions = new ();
         foreach ( var t in transactionsEnt)
         {
             transactions.Add(_mapper.Map<TransactionDto>(t));

         }
         
         var data = transactions
         .GroupBy(t => t.Category)
         .Select(v => new CatgoryValueDto{
             Category = v.Key,
             Value = v.Sum( s => s.Amount)
         });

         foreach (var v in data)
         {
             Console.WriteLine($"{v.Category} => {v.Value}");
         }

         return  Task.FromResult(data) ;
    }
}