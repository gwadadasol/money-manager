using Microsoft.EntityFrameworkCore;

namespace AnalyzerService.Domains.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<TransactionEntity> Transactions { get; set; } = null!;

        public AppDbContext( DbContextOptions<AppDbContext> opt):base(opt)
        {
            
        }
    }
}
