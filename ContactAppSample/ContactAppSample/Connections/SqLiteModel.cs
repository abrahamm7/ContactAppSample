using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ContactAppSample.Connections;
using SQLite;
using SQLitePCL;
using Xamarin.Forms;

[assembly: Dependency(typeof(SqLiteModel))]
namespace ContactAppSample.Connections
{
    public class SqLiteModel: SqliteInterface
    {
        public SQLiteConnection GetConnection()
        {
            var dbase = "ContactsDB";
            var dbpath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
            var path = Path.Combine(dbpath, dbase);
            var connection = new SQLiteConnection(path);
            return connection;

        }
    }
}
