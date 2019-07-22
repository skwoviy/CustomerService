using System.Collections.Generic;

namespace CustomerService.DTO
{
    public class CustomerDTO
    {
        public long CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Mobile { get; set; }
        public IEnumerable<TransactionDTO> Transactions { get; set; }
    }
}
