using ContactAppSample.Connections;
using ContactAppSample.Models;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ContactAppSample.ViewModels
{
    public class EditContactViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        People People { get; set; } = new People();

        public string name { get; set; }
        public string lastname { get; set; }
        public string tel { get; set; }
        public string phone { get; set; }
        public ICommand Savebtn { get; set; }

        public SQLiteConnection conn;
        public EditContactViewModel(People people)
        {
            this.People = people;
            name = people.Nombre;
            lastname = people.Apellido;
            tel = people.Telefono;
            phone = people.Celular;            
            Savebtn = new Command(Save);
        }

        [Obsolete]
        private async void Save()
        {
            conn = DependencyService.Get<SqliteInterface>().GetConnection();
            conn.Query<People>($"Update People set Nombre = '{name}', Apellido = '{lastname}', Telefono = '{tel}', Celular = '{phone}' where IdPeople = '{People.IdPeople}'");                     
            await App.Current.MainPage.DisplayAlert("Done", "The contact has saved!", "Ok");
        }
    }
}
