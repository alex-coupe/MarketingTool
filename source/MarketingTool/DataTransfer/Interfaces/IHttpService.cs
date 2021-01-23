using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataTransfer.Interfaces
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri);

        Task<T> Post<T>(string uri, object value);

        Task PostNoContentResponse(string uri, object value);

        Task<T> Put<T>(string uri, object value);

        Task<T> Delete<T>(string uri);
    }
}
