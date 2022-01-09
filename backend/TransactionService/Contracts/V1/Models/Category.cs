using System.Text.Json.Serialization;

namespace TransactionService.Contracts.V1.Models;
    public class Category
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
