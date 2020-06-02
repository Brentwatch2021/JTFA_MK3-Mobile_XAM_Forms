using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JTFA_MK3.Services
{
    public interface IEmployeeStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(long id);
        Task<T> GetItemAsync(long id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
