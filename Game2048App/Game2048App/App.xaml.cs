using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GameLib;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Game2048App
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MenuPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
