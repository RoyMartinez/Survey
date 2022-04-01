using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Interface
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        void EditRange(T[] entitie);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    }
}
