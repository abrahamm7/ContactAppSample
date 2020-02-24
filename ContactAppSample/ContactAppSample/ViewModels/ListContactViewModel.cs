using ContactAppSample.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactAppSample.ViewModels
{
    public class ListContactViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Addbtn { get; set; }

        public ListContactViewModel()
        {
            Addbtn = new Command(AddPeople);
        }
        private async void AddPeople()
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddContactPage());
        }
    }
}
