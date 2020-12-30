using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> : IDisposable
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

        void Add(T item);
        void Remove(int id);

        void Edit(T item);

        Task<int> SaveChangesAsync();

        void SaveChanges();
    }
}
