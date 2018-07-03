using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XCalendar.Models;

namespace XCalendar.ViewModels
{
    public class OrderEditorViewModel : ViewModel
    {
        private Order _order;
        public Order Order
        {
            get
            {
                if (_order == null)
                {
                    _order = new Order();
                }
                return _order;
            }
        }

        public OrderEditorViewModel(Day day)
        {
            this.Order.Date = day.DateTime;
            this.Title = "Add appointment";
        }

        private bool CheckFields()
        {
            return !String.IsNullOrEmpty(Order.Name);
        }

        public async Task SaveOrder()
        {
            if (this.CheckFields())
            {
                await App.DbContext.AddOrderAsync(this.Order);
            }
        }
    }
}
