using System;

namespace MoneyManagerBackend.Domains
{
    public class Movement
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public string Account { get; set; }
        
    }
}