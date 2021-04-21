using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KeyPadMVVM.ViewModels
{
    class DialingPageViewModel:ViewModelBase
    {
       
        public ICommand CancelCommand { get; set; }
        public DialingPageViewModel()
        {
            CancelCommand = new Command(async () => { await Application.Current.MainPage.Navigation.PopModalAsync(); });
        }
       
        private string dialNumber = "";
        public string DialNumber { get => dialNumber;
            set { 
                if(dialNumber!=value)
                {
                    dialNumber = value;
                    OnPropertyChange(nameof(DialNumber));
                }
                    } }
    }
}
