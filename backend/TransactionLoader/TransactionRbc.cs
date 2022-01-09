using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransactionLoader
{
	public class TransactionRbc
	{
		[Name("Account Number")]
		public string Account { get; set; }

		[Name("Transaction Date")]
		public DateTime Date { get; set; }

		[Name("Description 1")]
		public string Description1 { get; set; }

		[Name("Description 2")]
		public string Description2 { get; set; }

		[Name("CAD$")]
		public double Amount { get; set; }		    
	}
}

