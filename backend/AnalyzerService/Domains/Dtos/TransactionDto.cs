namespace AnalyzerService.Domains.Dtos
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public decimal Amount { get; set; }
        public string Account { get; set; } = null!;
        public string Category { get; set; } = null!;
        
    }
}