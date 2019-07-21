using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Contracts
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T Get(long id);

        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        //void Create(T item);

        //void Update(T item);

        //void Delete(long id);
    }
}
