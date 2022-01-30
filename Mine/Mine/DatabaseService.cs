using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Mine.Models;


namespace Mine.Services
{
    public class DatabaseService
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

    }

    
}
