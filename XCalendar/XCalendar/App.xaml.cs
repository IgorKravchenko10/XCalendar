using DLToolkit.Forms.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XCalendar.Models;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace XCalendar
{
	public partial class App : Application
	{
        private const string databaseName = "Calendar.db";
        private static DbContext _dbContext;

        public static DbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    _dbContext = new DbContext(databaseName);
                }
                return _dbContext;
            }
        }

        public static bool IsFirstTime { get; set; }

		public App ()
		{
			InitializeComponent();
            FlowListView.Init();
            IsFirstTime = true;
            MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
