using ContactAppSample.Connections;
using ContactAppSample.Models;
using ContactAppSample.Views;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactAppSample.ViewModels
{
    public class ListContactViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public List<People> People { get; set; } = new List<People>();

        public ICommand Addbtn { get; set; }
        public SQLiteConnection conn;


        public ListContactViewModel()
        {
            Addbtn = new Command(AddPeople);
            conn = DependencyService.Get<SqliteInterface>().GetConnection();
            conn.CreateTable<People>();
            var details = (from x in conn.Table<People>() select x).ToList();
            this.People = details;
        }

        [Obsolete]
        private async void AddPeople()
        {            
            await PopupNavigation.PushAsync(new AddContactPage());
        }
    }
}
