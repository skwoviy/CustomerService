using System;
using System.Collections.Generic;

namespace DbContext.Models
{
    public partial class Transaction
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public long CurrencyId { get; set; }
        public long StatusId { get; set; }
        public decimal CustomerId { get; set; }

        public Currency Currency { get; set; }
        public Customer Customer { get; set; }
        public Status Status { get; set; }
    }
}
