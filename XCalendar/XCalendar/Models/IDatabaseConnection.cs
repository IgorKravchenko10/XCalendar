using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XCalendar.Models
{
    public interface IDatabaseConnection
    {
        SQLiteConnection GetDbConnection(string databaseName);
    }
}
