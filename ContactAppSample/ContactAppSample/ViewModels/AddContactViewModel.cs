using ContactAppSample.Connections;
using ContactAppSample.Models;
using SQLite;
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
        public Items Items { get; set; } = new Items();
        public List<People> Peoples { get; set; } = new List<People>();
        public ICommand AddPeoplebtn { get; set; }

        //public bool PopUpVisible { get; set; }


        public SQLiteConnection conn;

        public AddContactViewModel()
        {
            AddPeoplebtn = new Command(AddContact);
            conn = DependencyService.Get<SqliteInterface>().GetConnection();
            //PopUpVisible = Items.State = true;
            //conn.CreateTable<People>();
        }

        private async void AddContact()
        {
            if (string.IsNullOrEmpty(People.Nombre) || string.IsNullOrEmpty(People.Apellido))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Empty fields", "Ok");
            }
            else
            {
                Peoples.Add(People);
                conn.InsertAll(Peoples);
                //Items.State = false;
                
            }
        }

    }
}
