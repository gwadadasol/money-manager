using System;
using System.Collections.Generic;
using TransactionService.Domains;
using TransactionService.Domains.Dtos;

namespace TransactionService.Services
{
    public interface ITransactionService
    {
        public List<TransactionDto> GetTransactions();

        public TransactionDto GetTransactionById(int movementId);

        public bool UpdateTransaction(TransactionDto movement);
        public bool DeleteTransaction(int movementId);

    }
}