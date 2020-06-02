using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JTFA_MK3.Services
{
    public interface IJTFA_ClientDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(Guid id);
        Task<T> GetItemAsync(Guid id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
