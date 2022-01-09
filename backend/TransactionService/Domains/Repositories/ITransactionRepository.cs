using System.Collections.Generic;
using System.Threading.Tasks;
using TransactionService.Domains.Model;

namespace TransactionService.Domains.Repository
{
    public interface ITransactionRepository
    {
        bool SaveChanges();
        Task<bool> SaveChangesAsync();


        IEnumerable<TransactionEntity> GetAllTransactions();
        TransactionEntity GetTransactionById(int id);
        void CreateTransaction(TransactionEntity transaction);

        void UpdateTransaction(TransactionEntity transaction);
    }
}