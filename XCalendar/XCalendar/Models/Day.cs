using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace XCalendar.Models
{
    public class Day:Item
    {
        public DateTime DateTime { get; }

        public ObservableCollection<Order> Orders { get; set; }

        public bool IsCurrentMonth { get; }

        public string OrdersString
        {
            get
            {
                string result = "";
                if (this.Orders?.Count > 0)
                {
                    foreach(Order order in this.Orders)
                    {
                        result += order.TimeString + " " + order.Name + System.Environment.NewLine;
                    }
                }
                return result;
            }
        }

        public bool IsToday
        {
            get
            {
                 return this.DateTime == DateTime.Today;
            }
        }

        public Color BackgroundColor
        {
            get
            {
                if (this.IsToday)
                {
                    return Color.FromHex("#85a1e5");
                }
                else
                {
                    return Color.Transparent;
                }
            }
        }

        public Day(DateTime dateTime)
        {
            this.DateTime = dateTime;
        }

        public Day(DateTime dateTime, bool isCurrentMonth)
        {
            this.DateTime = dateTime;
            this.IsCurrentMonth = isCurrentMonth;
        }

        public Day AddDays(double value, bool isCurrentMonth = false)
        {
            return new Day(this.DateTime.AddDays(value), isCurrentMonth);
        }        
    }
}
