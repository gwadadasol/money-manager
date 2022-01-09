using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using TransactionService.Contracts.V1.Requests;
using TransactionService.Domains.Dtos;
using TransactionService.Domains.Model;
using TransactionService.Domains.Repository;

namespace TransactionService.Contracts.V1.Handlers
{
    public class CreateTransactionRequestHandler : IRequestHandler<CreateTransactionRequest, TransactionDto>
    {
        private readonly IMapper _mapper;
        private readonly ITransactionRepository _repository;
        private readonly IMediator _mediator;

        public CreateTransactionRequestHandler(IMapper mapper, ITransactionRepository repository, IMediator mediator)
        {
            _mapper = mapper;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<TransactionDto> Handle(CreateTransactionRequest request, CancellationToken cancellationToken)
        {
            // Get category
            request.Transaction.Category = await _mediator.Send( new GetCategoryRequest {Description = request.Transaction.Description});
             
            var transactionEntity = _mapper.Map<TransactionEntity>(request.Transaction);
            _repository.CreateTransaction(transactionEntity);
            await _repository.SaveChangesAsync();

            return _mapper.Map<TransactionDto>(transactionEntity);

        }
    }
}