using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SQLite;
using System.Linq;

namespace XCalendar.Models
{
    public class DbContext
    {
        private SQLiteConnection _database;
        private static object _collisionLock = new object();


        public DbContext(string databaseName)
        {
            _database = DependencyService.Get<IDatabaseConnection>().GetDbConnection(databaseName);
            _database.CreateTable<Order>();
        }

        public int AddOrder(Order order)
        {
            lock (_collisionLock)
            {
                return _database.Insert(order);
            }
        }

        public Task<int> AddOrderAsync(Order order)
        {
            return Task.Factory.StartNew(() => this.AddOrder(order));
        }

        public List<Order> GetOrders(DateTime month)
        {
            List<Order> orders = (from qr in _database.Table<Order>() where qr.Date.Month == month.Month select qr).ToList();
            return orders;
        }

        public List<Order> GetOrders()
        {
            List<Order> orders = _database.Table<Order>().ToList();
            return orders;
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            return Task.Factory.StartNew(() =>
            {
                return this.GetOrders();
            });
        }

        public List<Order> GetOrders(Day day)
        {
            List<Order> orders = (from qr in _database.Table<Order>().ToList() where qr.Date.Day == day.DateTime.Day orderby qr.TimeString select qr).ToList();
            return orders;
        }

        public Task<List<Order>> GetOrdersAsync(Day day)
        {
            return Task.Factory.StartNew(() => this.GetOrders(day));
        }
    }
}
