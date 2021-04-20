using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KeyPadMVVM.Views;
namespace KeyPadMVVM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new KeyPadPage();
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
