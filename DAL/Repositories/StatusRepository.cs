using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DAL.Contracts;
using DbContext.Models;

namespace DAL.Repositories
{
    public class StatusRepository : IRepository<Status>
    {
        private readonly CustomerServiceContext _dbContext;

        public StatusRepository(CustomerServiceContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Status> Find(Expression<Func<Status, bool>> predicate)
        {
            IQueryable<Status> result = _dbContext.Status.Where(predicate);

            return result.ToList();

        }

        public Status Get(long id)
        {
            return _dbContext.Status.Find(id);
        }

        public IEnumerable<Status> GetAll()
        {
            return _dbContext.Status;
        }
    }
}
