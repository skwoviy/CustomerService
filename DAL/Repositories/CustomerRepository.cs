using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Contracts;
using DbContext.Models;

namespace DAL.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly CustomerServiceContext _dbContext;

        public CustomerRepository(CustomerServiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Customer> Find(Expression<Func<Customer, bool>> predicate)
        {
            IQueryable<Customer> result = _dbContext.Customer.Where(predicate);

            return _dbContext.Customer.Where(predicate).ToList();

        }

        public Customer Get(long id)
        {
            return _dbContext.Customer.Find(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _dbContext.Customer;
        }
    }
}
