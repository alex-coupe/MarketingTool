using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Interfaces
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);

        Task<T> Post(T entity);

        Task PostNoReturnContent(T entity);

        Task<T> Put(T entity);

        Task Remove(int id);
    }
}
