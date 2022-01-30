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

        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
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

        /// <summary>
        /// Adds an item to the database asynchronously
        /// </summary>
        /// <param name="item"></param>
        /// <returns> returns false if the item passes is null or the database returns an id of 0 </returns>
        async public Task<bool> CreateAsync(ItemModel item)
        {
            //checks if the item is null
            if (item == null)
            {
                return false;
            }

            //inserts item into database and return false if that fails
            var result = await Database.InsertAsync(item);
            if (result == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Send an item to the db for an update
        /// </summary>
        /// <param name="item"></param>
        /// <returns> true if the item successfully updated</returns>
        public async Task<bool> UpdateAsync(ItemModel item)
        {
            //check argument for null
            if(item == null)
            {
                return false;
            }

            //Pass the item to the db and check for error
            var result = await Database.UpdateAsync(item);
            if(result == 0)
            {
                return false;
            }

            return true;
        }

        public Task<bool> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the first matching ItemModel record from the db based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns> an ItemModel </returns>
        public Task<ItemModel> ReadAsync(string id)
        {
            if(id == null)
            {
                return null;
            }

            //give the id to the database and get first the matching record (with the matching ID) using Linq
            var result = Database.Table<ItemModel>().FirstOrDefaultAsync(m => m.Id.Equals(id));
            return result;

        }

        /// <summary>
        /// Gets a list of items and returns them as a task(?!)
        /// </summary>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ItemModel>> IndexAsync(bool forceRefresh = false)
        {
            //I think this asks the db to return the itemModel table
            var result = await Database.Table<ItemModel>().ToListAsync();
            return result;
        }


    }

    
}
