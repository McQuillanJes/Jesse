using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using SQLite;
using Mine.Models;

namespace Mine.Services
{
    public class DatabaseService : IDataStore<ItemModel>
    {

        static readonly Lazy<SQLiteAsyncConnection> lasyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lasyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            initializeAsync().SafeFireAndForget(false);
        }

        async Task initializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTableAsync(typeof(ItemModel), CreateFlags.None).ConfigureAwait(false);

                }
                initialized = true;
            }
        }

        public Task<bool> CreateAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemModel> ReadAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of items and returns them as a task(?!)
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        async public Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            //I think this asks the db to return the itemModel table
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }


    }

    
}
