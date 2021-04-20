using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeyPadMVVM.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KeyPadMVVM.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class KeyPadPage : ContentPage
    {
        public KeyPadPage()
        {
            KeyPadViewModel k = new KeyPadViewModel();
            BindingContext = k;
            InitializeComponent();
        }
    }
}