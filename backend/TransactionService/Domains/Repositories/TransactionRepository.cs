using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransactionService.Domains.Model;

namespace TransactionService.Domains.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void CreateTransaction(TransactionEntity transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentNullException(nameof(transaction));
            }
            _dbContext.Transactions.Add(transaction);
        }
        
        public IEnumerable<TransactionEntity> GetAllTransactions()
        {
           return _dbContext.Transactions.ToList();
        }

        public TransactionEntity GetTransactionById(int id)
        {
            return _dbContext.Transactions.FirstOrDefault(c => c.Id == id);
        }

        public bool SaveChanges()
        {
           return (_dbContext.SaveChanges() >= 0 );
        }

        public async Task<bool>  SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0 );
        }

        public void UpdateTransaction(TransactionEntity transaction)
        {
             _dbContext.Entry(transaction).State = EntityState.Modified;
        }
    }
}