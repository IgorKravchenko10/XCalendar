using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using XCalendar.Droid;
using XCalendar.Models;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection))]
namespace XCalendar.Droid
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection GetDbConnection(string databaseName)
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), databaseName);
            return new SQLiteConnection(path);
        }
    }
}