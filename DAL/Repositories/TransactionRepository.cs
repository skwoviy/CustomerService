using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Contracts;
using DbContext.Models;

namespace DAL.Repositories
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly CustomerServiceContext _dbContext;

        public TransactionRepository(CustomerServiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Transaction> Find(Expression<Func<Transaction, bool>> predicate)
        {
            IQueryable<Transaction> result = _dbContext.Transaction.Where(predicate);

            return result.ToList();

        }

        public Transaction Get(long id)
        {
            return _dbContext.Transaction.Find(id);
        }

        public IEnumerable<Transaction> GetAll()
        {
            return _dbContext.Transaction;
        }
    }
}
