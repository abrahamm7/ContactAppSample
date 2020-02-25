using ContactAppSample.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactAppSample.ViewModels
{
    public class AddContactViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public People People { get; set; } = new People();
        public ICommand AddPeoplebtn { get; set; }

        public AddContactViewModel()
        {
            AddPeoplebtn = new Command(AddContact);
        }

        private async void AddContact()
        {
            if (string.IsNullOrEmpty(People.Nombre) || string.IsNullOrEmpty(People.Apellido))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Empty fields", "Ok");
            }
            else
            {
                
            }
        }

    }
}
