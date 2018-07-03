using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XCalendar.Models;

namespace XCalendar.ViewModels
{
    public class CalendarDetailViewModel : ViewModel
    {
        public Day Day { get; }

        public ObservableCollection<Order> Orders { get; }

        public CalendarDetailViewModel(Day day)
        {
            this.Day = day;
            this.Title = this.Day.DateTime.ToString("D");
            this.Orders = new ObservableCollection<Order>();
        }

        public async Task LoadOrders()
        {
            this.Orders.Clear();
            List<Order> orders = await App.DbContext.GetOrdersAsync(this.Day);
            foreach (Order order in orders)
            {
                this.Orders.Add(order);
            }
            this.OnPropertyChanged("Orders");
        }
    }
}
