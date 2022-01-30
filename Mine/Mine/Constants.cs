using System;
using System.IO;


namespace Mine
{
    public static class Constants
    {
        public const string DatabaseFilename = "mine.db3";

        public const SQLite.SQLiteOpenFlags Flags =
            //Open the databse in read-write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            //Create the databse if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            //Enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

    }
}