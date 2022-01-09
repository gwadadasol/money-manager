using System.Collections.Generic;
using MediatR;
using TransactionService.Domains.Dtos;

namespace TransactionService.Contracts.V1.Requests
{
    public class GetTransactionsRequest: IRequest<List<TransactionDto>> { }
}