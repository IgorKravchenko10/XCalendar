using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using XCalendar.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace XCalendar.Models
{
    public class Month : Item
    {
        public ObservableCollection<Day> Days { get; private set; }
        public List<Order> Orders { get; private set; }

        public DateTime DateTime { get; }

        public String CurrentMonth { get { return this.DateTime.Date.ToString("y"); } }

        public Month(DateTime dateTime)
        {
            this.DateTime = dateTime;
            this.Days = new ObservableCollection<Day>(this.GetCalendarDaysInMonth(this.DateTime));
        }

        private IEnumerable<Day> AllDatesInMonth(int year, int month, bool isCurrentMonth, int daysCount = 0)
        {
            if (daysCount == 0)
            {
                daysCount = DateTime.DaysInMonth(year, month);
            }
            for (int day = 1; day <= daysCount; day++)
            {
                yield return new Day(new DateTime(year, month, day), isCurrentMonth);
            }
        }

        private const int calendarDaysCount = 42;
        private IEnumerable<Day> GetCalendarDaysInMonth(DateTime date)
        {
            List<Day> days = new List<Day>();
            int daysCount = DateTime.DaysInMonth(date.Year, date.Month);
            for (int day = 1; day <= daysCount; day++)
            {
                days.Add(new Day(new DateTime(date.Year, date.Month, day), true));
            }
            if (days[0].DateTime.DayOfWeek != DayOfWeek.Monday)
            {
                List<Day> prevMonth = this.AllDatesInMonth(date.Year, date.AddMonths(-1).Month, false).ToList();
                if (days[0].DateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    days.InsertRange(0, prevMonth.TakeLast(6));
                }
                else
                {
                    days.InsertRange(0, prevMonth.TakeLast((int)days[0].DateTime.DayOfWeek - 1));
                }
            }
            int additionalDaysCount = calendarDaysCount - days.Count;
            List<Day> additionalDays = this.AllDatesInMonth(date.Year, date.AddMonths(1).Month, false, additionalDaysCount).ToList();
            days.AddRange(additionalDays);
            return days;
        }

        public async Task LoadOrders()
        {
            this.Orders = await App.DbContext.GetOrdersAsync();
            foreach (Day day in this.Days)
            {
                day.Orders = new ObservableCollection<Order>((from qr in this.Orders where day.DateTime == qr.Date select qr).ToList());
                day.OnPropertyChanged("OrdersString");
            }
        }        
    }
}
