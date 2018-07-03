using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XCalendar.ViewModels;

namespace XCalendar.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FirstPage : ContentPage
	{
        private FirstPageViewModel _viewModel;

		public FirstPage ()
		{
			InitializeComponent ();
            this.BindingContext = _viewModel = new FirstPageViewModel();
		}

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync(true);
        }
    }
}