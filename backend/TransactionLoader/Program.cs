using CsvHelper;
using CsvHelper.Configuration;
using RestSharp;
using System;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace TransactionLoader
{
    class Program
    {
        static void Main(string[] args)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            using (var reader = new StreamReader("C:\\Users\\phili\\Downloads\\csv15100.csv"))
            using (var csv = new CsvReader(reader, config)) // CultureInfo.InvariantCulture))
            {
                foreach (var record in csv.GetRecords<TransactionRbc>())
                {
                    var transaction = new TransactionApi(record);
                    var client = new RestClient("https://localhost:5001");
                    var request = new RestRequest("api/v1/transactions");
                    request.AddJsonBody(transaction);

                    var response = client.Post(request);

                    Console.WriteLine(response.StatusCode);
                }
            }
        }
    }
}
