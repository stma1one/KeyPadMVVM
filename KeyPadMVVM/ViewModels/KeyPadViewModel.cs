using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace KeyPadMVVM.ViewModels
{
    public class KeyPadViewModel : INotifyPropertyChanged
    {

        private string formattedString="";
        private string displayText="";
        #region Properties
        public string FormattedString { get=>formattedString; set
            {
                if(formattedString!=value)
                {
                    formattedString = value;
                    OnPropertyChange(nameof(FormattedString));
                    DisplayText = FormatText(formattedString);
                    
                   ((Command)DelCharCommand).ChangeCanExecute();
                }
               
                ;
            } }

        private string FormatText(string str)
        {
            string res = "";
            if (str.Length <= 2)
                return str;
            if (str.Length < 8 &&(formattedString.StartsWith("02") || str.StartsWith("03")))
                res = $"({str.Substring(0, 1)})-{str.Substring(3)}";
            else
                res = $"({str.Substring(0, 3)})-{str.Substring(3)}";

            
            return res;
                    
        }

        public string DisplayText { get => displayText; set
            {
                if(displayText!=value)
                {
                    displayText = value;
                    OnPropertyChange(nameof(DisplayText));
                    
                }    
            } }
        #endregion


        public KeyPadViewModel()
        {
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
        private void OnPropertyChange(string propertyName)
        {
            var args = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, args);
        }

        private void AddChar(string obj)
        {
            FormattedString += obj;
            

        }

        private void DelChar()
        {
            FormattedString = FormattedString.Substring(0, FormattedString.Length - 1);
        }
        private bool CanChange()
        {
            return !(FormattedString.Length>0);
        }
        #region events
        public event PropertyChangedEventHandler PropertyChanged;


        #endregion

        #region ICommands
        // public ICommand AddCharCommand {protected get;set;}
        //public ICommand DelCharCommand {protected get;set;}
        public ICommand AddCharCommand => new Command<string>(AddChar);
        public ICommand DelCharCommand => new Command(DelChar,CanChange);

       



        #endregion

    }
}