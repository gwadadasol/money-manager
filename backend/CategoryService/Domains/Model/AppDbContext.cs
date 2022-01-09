using Microsoft.EntityFrameworkCore;

namespace CategoryService.Domains.Model
{
    public class AppDbContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<RuleEntity> Rules { get; set; }
        // public DbSet<CategoryRuleEntity> CategoryRules { get; set; }

        public AppDbContext( DbContextOptions<AppDbContext> opt):base(opt)
        {
            
        }
    }
}
