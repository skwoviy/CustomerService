using System;
using System.Collections.Generic;

namespace DbContext.Models
{
    public partial class Currency
    {
        public Currency()
        {
            Transaction = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public ICollection<Transaction> Transaction { get; set; }
    }
}
