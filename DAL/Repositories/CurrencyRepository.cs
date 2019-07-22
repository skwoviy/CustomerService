using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Contracts;
using DbContext.Models;

namespace DAL.Repositories
{
    public class CurrencyRepository : IRepository<Currency>
    {
        private readonly CustomerServiceContext _dbContext;

        public CurrencyRepository(CustomerServiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Currency> Find(Expression<Func<Currency, bool>> predicate)
        {
            IQueryable<Currency> result = _dbContext.Currency.Where(predicate);

            return result.ToList();

        }

        public Currency Get(long id)
        {
            return _dbContext.Currency.Find(id);
        }

        public IEnumerable<Currency> GetAll()
        {
            return _dbContext.Currency;
        }
    }
}
