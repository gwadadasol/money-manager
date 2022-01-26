using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnalyzerService.Domains.Model
{
    public class TransactionEntity
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string Description { get; set; } = null!;

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal AmountCad { get; set; }
        
        [Required]
        public string AccountNumber { get; set; } = null!;

        public string Category  { get; set;  }= null!;
    }
}
