using System;
using System.Collections.Generic;

namespace DbContext.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Transaction = new HashSet<Transaction>();
        }

        public decimal Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal MobileNo { get; set; }

        public ICollection<Transaction> Transaction { get; set; }
    }
}
