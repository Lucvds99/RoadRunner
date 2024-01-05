using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadRunnerApp
{
    public static class Constants
    {
        public const string DatabaseFilename = "database.db";

        public const SQLite.SQLiteOpenFlags Flags = 
            SQLite.SQLiteOpenFlags.ReadWrite |
            SQLite.SQLiteOpenFlags.Create |
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath 
        {
            get
            {
                var basepath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(basepath, DatabaseFilename);
            }
        }
    }
}
