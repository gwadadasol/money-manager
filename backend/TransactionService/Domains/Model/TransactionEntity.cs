using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionService.Domains.Model
{
    public class TransactionEntity
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public DateTime TransactionDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        [Required]
        public decimal AmountCad { get; set; }
        
        [Required]
        public string AccountNumber { get; set; }

        
        public string Category  { get; set; }
    }
}
