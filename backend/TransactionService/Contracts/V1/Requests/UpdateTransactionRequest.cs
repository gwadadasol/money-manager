using MediatR;
using TransactionService.Domains.Dtos;

namespace TransactionService.Contracts.V1
{
    public class UpdateTransactionRequest : IRequest<TransactionDto>
    {
        public TransactionDto Transaction { get; set; }
    }
}