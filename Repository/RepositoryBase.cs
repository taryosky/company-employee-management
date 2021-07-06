using Contracts;

using Entities;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected ApplicationContext applicationContext;

        public RepositoryBase(ApplicationContext context)
        {
            applicationContext = context;
        }

        public void Create(T entity)
        {
            applicationContext.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            applicationContext.Set<T>().Remove(entity);
        }

        public System.Linq.IQueryable<T> FindAll(bool noTracking)
        {
            var n = noTracking ? applicationContext.Set<T>().AsNoTracking() : applicationContext.Set<T>();
            var b = n.AsAsyncEnumerable();
            return n;
        }

        public System.Linq.IQueryable<T> FindByCondition(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return applicationContext.Set<T>().Where(expression);
        }

        public void Update(T entity)
        {
            applicationContext.Set<T>().Update(entity);
        }
    }
}
