using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XCalendar.Models;
using XCalendar.Views;

namespace XCalendar
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            this.BindingContext = calendarView;
            this.SetBinding(TitleProperty, "Title");
            calendarView.ItemTapped += this.ItemTapped;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await calendarView.LoadOrders();
            if (App.IsFirstTime)
            {
                await Navigation.PushModalAsync(new FirstPage(), true);
                App.IsFirstTime = false;
            }
        }

        private async void ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                await this.Navigation.PushAsync(new CalendarDetailPage(e.Item as Day), true);
                (sender as FlowListView).SelectedItem = null;
            }
            catch(Exception ex)
            {
                await DisplayAlert(ex.GetType().ToString(), ex.Message, "OK");
            }
        }
    }
}
