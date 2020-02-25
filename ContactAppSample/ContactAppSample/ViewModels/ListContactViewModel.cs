using ContactAppSample.Views;
using Rg.Plugins.Popup.Services;
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

        [Obsolete]
        private async void AddPeople()
        {            
            await PopupNavigation.PushAsync(new AddContactPage());
        }
    }
}
