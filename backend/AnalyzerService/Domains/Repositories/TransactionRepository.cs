using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AnalyzerService.Domains.Model;

namespace AnalyzerService.Domains.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _dbContext;

        public TransactionRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<TransactionEntity> GetAllTransactions()
        {
            return _dbContext.Transactions.ToList();
        }

        public TransactionEntity GetTransactionById(int id)
        {
            return _dbContext.Transactions.First(c => c.Id == id);
        }

        public bool SaveChanges()
        {
            return (_dbContext.SaveChanges() >= 0);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}