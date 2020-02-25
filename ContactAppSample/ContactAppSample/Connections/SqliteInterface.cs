using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


namespace ContactAppSample.Connections
{
    public interface SqliteInterface
    {
        SQLiteConnection GetConnection();

    }
}
