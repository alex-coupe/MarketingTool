using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IRepository<T> : IDisposable
    {
        
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);

        Task<T> GetAsync(Expression<Func<T, bool>> predicate = null);

        void Add(T item);
        void Remove(int id);

        void Edit(T item);

        Task<int> SaveChangesAsync();
    }
}
