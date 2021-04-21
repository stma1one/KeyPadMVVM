using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using KeyPadMVVM.Views;

namespace KeyPadMVVM.ViewModels
{
    public class KeyPadViewModel : INotifyPropertyChanged
    {
        #region properties
        private string formattedString = "";
        private string displayText = "";
        
        public string FormattedString { get => formattedString; set
            {
                if (formattedString != value)
                {
                    formattedString = value;
                    OnPropertyChange(nameof(FormattedString));
                    DisplayText = FormatText(formattedString);

                    ((Command)DelCharCommand).ChangeCanExecute();
                    ((Command)DialCommand).ChangeCanExecute();
                }

                  ;
            } }

        private string FormatText(string str)
        {
            string res = "";
            if (str.Length <= 2)
                return str;
            if (str.Length <=10 && (formattedString.StartsWith("02") || str.StartsWith("03") || str.StartsWith("(03") || str.StartsWith("(02")))
                res = $"({str.Substring(0, 2)})-{str.Substring(3)}";
            else
                res = $"({str.Substring(0, 3)})-{str.Substring(3)}";


            return res;

        }

        public string DisplayText { get => displayText; set
            {
                if (displayText != value)
                {
                    displayText = value;
                    OnPropertyChange(nameof(DisplayText));

                }
            } }
        #endregion

        #region constructor
        public KeyPadViewModel()
        {
            DelCharCommand = new Command(DelChar, CanChange);
            DialCommand = new Command(async () =>
            {
                Page p = new DialingPage
                {
                    BindingContext = new DialingPageViewModel() { DialNumber = DisplayText }
                };
                await Application.Current.MainPage.Navigation.PushModalAsync(p);
            }, CanChange);
           
            
            //AddCharCommand = new Command<string>((key) =>
            //{
            //    // Add the key to the input string.
            //    FormattedString += key;
            //});
            //DelCharCommand = new Command(() =>
            //{
            //    // Strip a character from the input string.
            //    FormattedString = FormattedString.Substring(0, FormattedString.Length - 1);
            //},
            //    () =>
            //    {
            //        // Return true if there's something to delete.
            //        return FormattedString.Length > 0;
            //    });


        }
        #endregion


        #region INOTIFYPROPERTYCHANGED
        private void OnPropertyChange(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        private void AddChar(string obj)
        {
            FormattedString += obj;
            

        }

       
        #region events
       


        #endregion

        #region ICommands
        // public ICommand AddCharCommand {protected get;set;}
        //public ICommand DelCharCommand {protected get;set;}
        public ICommand AddCharCommand => new Command<string>(AddChar);
        public ICommand DelCharCommand {  get; set; }//=>new Command(DelChar,CanChange);

        public ICommand DialCommand { get; set; }//=> new Command(async () =>
    //        {
    //            Page p = new DialingPage
    //            {
    //                BindingContext = new DialingPageViewModel() { DialNumber = DisplayText }
    //            };
    //    await Application.Current.MainPage.Navigation.PushModalAsync(p);
    //}, CanChange);
        private void DelChar()
        {
            FormattedString = FormattedString.Substring(0, FormattedString.Length - 1);
        }
        private bool CanChange()
        {
            return (FormattedString.Length > 0);
        }
        #endregion

    }
}