using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interface.Infrastructure.Database
{
    public interface ICacheDbRespository
    {
        Task SetData<T>(string key, T data, TimeSpan? duration = null);
        Task<T> GetData<T>(string key);
        Task RemoveData(string key);
    }
}