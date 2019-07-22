using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerService.DTO
{
    public class TransactionDTO
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Currensy { get; set; }
        public string Status { get; set; }
    }
}
