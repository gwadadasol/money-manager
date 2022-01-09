using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionLoader
{
	public class TransactionApi
	{
		public string Account { get; set; }

		public DateTime Date { get; set; }

		public string Description { get; set; }


		public double Amount { get; set; }

        public string Category { get; set; } = string.Empty;

        public TransactionApi(TransactionRbc transactionRbc)
        {
            Account = transactionRbc.Account;
            Date = transactionRbc.Date;
            Description = transactionRbc.Description1 + transactionRbc.Description2;
            Amount = transactionRbc.Amount;
            Category = string.Empty;

            
        }
	}
}

// {
//     "id": 1,
//     "date": "2021-10-09T21:19:37.903Z",
//     "description": "desc1",
//     "amount": 100,
//     "account": "1-1",
//     "category": ""
//   },
