using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XCalendar.Models;
using XCalendar.ViewModels;

namespace XCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarDetailPage : ContentPage
    {
        private CalendarDetailViewModel _viewModel;

        public CalendarDetailPage(Day day)
        {
            InitializeComponent();
            _viewModel = new CalendarDetailViewModel(day);
            this.BindingContext = _viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadOrders();
        }

        private async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ((ListView)sender).SelectedItem = null;
        }

        private async void AddButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _viewModel.IsBusy = true;
                await Navigation.PushAsync(new OrderEditor(_viewModel.Day), true);
            }
            catch(Exception ex)
            {
                await DisplayAlert(ex.GetType().ToString(), ex.Message, "OK");
            }
            finally
            {
                _viewModel.IsBusy = false;
            }
        }
    }
}
