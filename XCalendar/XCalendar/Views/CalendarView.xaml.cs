using DLToolkit.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XCalendar.Models;
using XCalendar.ViewModels;

namespace XCalendar.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarView : ContentView
    {
        CalendarViewModel _viewModel;
        private string _title;

        public int Position
        {
            get { return carouselView.Position; }
        }

        public CalendarView()
        {
            InitializeComponent();
            _viewModel = new CalendarViewModel(this);
            this.BindingContext = _viewModel;
            carouselView.PositionSelected += CarouselView_PositionSelected;
        }

        public async Task LoadOrders()
        {
            await _viewModel.LoadOrders();
        }

        public string Title
        {
            get
            {
                return _viewModel.Date;
            }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    this.OnPropertyChanged("Title");
                }
            }
        }

        private async void CarouselView_PositionSelected(object sender, SelectedPositionChangedEventArgs e)
        {
            await _viewModel.LoadMonth((int)e.SelectedPosition);
            this.LoadOrders();
        }

        public event EventHandler<ItemTappedEventArgs> ItemTapped;
        private void FlowListView_FlowItemTapped(object sender, ItemTappedEventArgs e)
        {
            this.ItemTapped?.Invoke(sender, e);
        }
    }
}