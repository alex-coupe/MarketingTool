using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransfer.Interfaces
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(string id);

        Task<T> GetSingle();

        Task<T> Post(T entity);

        Task PostNoReturnContent(T entity);

        Task<T> Put(T entity);

        Task Remove(int id);
    }
}
