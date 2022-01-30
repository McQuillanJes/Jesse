using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mine.Services;
using Mine.Views;

namespace Mine
{
    public partial class App : Application
    {

        /// <summary>
        /// I guess this starts the app then
        /// </summary>
        public App()
        {
            InitializeComponent();

            //register the mock data store instead of the database
            //DependencyService.Register<MockDataStore>();

            //Register the database instead of the mockdatastore
            DependencyService.Register<DatabaseService>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
