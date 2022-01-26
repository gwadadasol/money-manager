using AnalyzerService.Contracts.V1.Requests;
using AnalyzerService.Domains.Repository;
using AutoMapper;
using MediatR;

namespace AnalyzerService.Contracts.V1.Handlers;

public class GetAmountsByCategoriesHandler : IRequestHandler<GetAmountsByCategoriesRequest, Unit>
{
    private readonly IMapper _mapper;
    private readonly ITransactionRepository _repository;

    public GetAmountsByCategoriesHandler(IMapper mapper, ITransactionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
    public Task<Unit> Handle(GetAmountsByCategoriesRequest request, CancellationToken cancellationToken)
    {
         var transactionEnt = _repository.GetAllTransactions();
         return  Task.FromResult(Unit.Value) ;
    }
}