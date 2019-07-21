using System;
using System.Collections.Generic;

namespace DbContext.Models
{
    public partial class Status
    {
        public Status()
        {
            Transaction = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<Transaction> Transaction { get; set; }
    }
}
