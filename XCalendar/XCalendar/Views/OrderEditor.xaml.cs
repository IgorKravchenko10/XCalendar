using System;
using System.Collections.Generic;
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
	public partial class OrderEditor : ContentPage
	{
        private OrderEditorViewModel _viewModel;

		public OrderEditor (Day day)
		{
			InitializeComponent ();
            _viewModel = new OrderEditorViewModel(day);
            this.BindingContext = _viewModel;
		}

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await _viewModel.SaveOrder();
            await Navigation.PopAsync(true);
        }
    }
}