using System.Collections.Generic;
using System.Threading.Tasks;
using AnalyzerService.Domains.Model;

namespace AnalyzerService.Domains.Repository
{
    public interface ITransactionRepository
    {
        bool SaveChanges();
        Task<bool> SaveChangesAsync();


        IEnumerable<TransactionEntity> GetAllTransactions();
        TransactionEntity GetTransactionById(int id);
    }
}