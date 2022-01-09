using Microsoft.EntityFrameworkCore;

namespace TransactionService.Domains.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; }

        public AppDbContext( DbContextOptions<AppDbContext> opt):base(opt)
        {
            
        }
    }
}
