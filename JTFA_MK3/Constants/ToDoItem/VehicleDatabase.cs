using JTFA_MK3.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JTFA_MK3.Constants.ToDoItem
{
    public class VehicleDatabase
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public VehicleDatabase()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(Vehicle).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(Vehicle)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Vehicle>> GetItemsAsync()
        {
            return Database.Table<Vehicle>().ToListAsync();
        }

        public Task<List<Vehicle>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            return Database.QueryAsync<Vehicle>("SELECT * FROM [Vehicle]");
        }

        public Task<Vehicle> GetItemAsync(Guid id)
        {
            return Database.Table<Vehicle>().Where(i => i.Vehicle_ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Vehicle item)
        {
            if (item.Vehicle_ID != null && item.Vehicle_ID.ToString() != "00000000-0000-0000-0000-000000000000")
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Vehicle item)
        {
            return Database.DeleteAsync(item);
        }
    }
}
