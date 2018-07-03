using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Xamarin.Forms;
using XCalendar.Models;
using XCalendar.Views;

namespace XCalendar.ViewModels
{
    public class CalendarViewModel : ViewModel
    {
        public ObservableCollection<Month> Months { get; }

        private CalendarView _calendarView;

        public Month CurrentMonth
        {
            get
            {
                return this.Months[_calendarView.Position];
            }
        }

        public string Date
        {
            get
            {
                return this.CurrentMonth?.CurrentMonth;
            }
        }

        public Command OnItemTappedCommand;

        public CalendarViewModel(CalendarView calendarView)
        {
            _calendarView = calendarView;
            this.Months = new ObservableCollection<Month>();
            DateTime today = DateTime.Today;
            this.Months.Add(new Month(today));
            this.Months.Add(new Month(today.AddMonths(1)));
            this.Months.Insert(0, new Month(today.AddMonths(-1)));
        }

        public async Task LoadMonth(int selectedIndex)
        {
            if (this.Months.Count - 1 == selectedIndex)
            {
                await this.LoadNextMonth();
            }
            else if (0 == selectedIndex)
            {
                await this.LoadPreviousMonth();
            }
            _calendarView.Title = this.Date;
        }

        public async Task LoadOrders()
        {
            foreach (Month month in this.Months)
            {
                await month.LoadOrders();
            }
            this.OnPropertyChanged("Months");
        }

        private async Task LoadNextMonth()
        {
            await Task.Factory.StartNew(() =>
            {
                this.Months.Add(new Month(this.CurrentMonth.DateTime.AddMonths(1)));
                this.Months.RemoveAt(0);
                GC.Collect();
            });
        }

        private async Task LoadPreviousMonth()
        {
            await Task.Factory.StartNew(() =>
            {
                this.Months.Insert(0, new Month(this.CurrentMonth.DateTime.AddMonths(-1)));
                this.Months.RemoveAt(this.Months.Count - 1);
                GC.Collect();
            });
        }
    }
}
