﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManagerBackend.Domains
{
    public class MovementEntity
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public double AmountCad { get; set; }
        public string AccountNumber { get; set; }
    }
}
