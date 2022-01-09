using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TransactionService.Domains.Dtos;
using TransactionService.Domains.Model;
using TransactionService.Domains.Repository;

namespace TransactionService.Contracts.V1.Handlers
{
    public class UpdateTransactionRequestHandler : IRequestHandler<UpdateTransactionRequest, TransactionDto>
    {

          private readonly IMapper _mapper;
        private readonly ITransactionRepository _repository;

        public UpdateTransactionRequestHandler(IMapper mapper, ITransactionRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

     
        public async Task<TransactionDto> Handle(UpdateTransactionRequest request, CancellationToken cancellationToken)
        {
             var transactionEntity =  _mapper.Map<TransactionEntity>(request.Transaction);
            _repository.UpdateTransaction(transactionEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TransactionDto>(transactionEntity);
        }
    }
}