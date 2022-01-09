using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TransactionService.Contracts.V1.Requests;
using TransactionService.Domains.Dtos;
using TransactionService.Domains.Repository;

namespace TransactionService.Contracts.V1.Handlers
{
    public class GetTransactionsRequestHandle : IRequestHandler<GetTransactionsRequest, List<TransactionDto>>
    {

        private readonly IMapper _mapper;
        private readonly ITransactionRepository _repository;

        public GetTransactionsRequestHandle(IMapper mapper, ITransactionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public Task<List<TransactionDto>> Handle(GetTransactionsRequest request, CancellationToken cancellationToken)
        {
            var transactionEnt = _repository.GetAllTransactions();
            var transactionDto = transactionEnt.Select(c => _mapper.Map<TransactionDto>(c)).ToList();

            return Task.FromResult(transactionDto);
        }
    }
}