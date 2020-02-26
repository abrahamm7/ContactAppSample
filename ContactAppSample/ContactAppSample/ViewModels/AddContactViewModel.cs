﻿using ContactAppSample.Connections;
using ContactAppSample.Models;
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
    public class AddContactViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public People People { get; set; } = new People();       
        public List<People> Peoples { get; set; } = new List<People>();
        public ICommand AddPeoplebtn { get; set; }
        public SQLiteConnection conn;

        public AddContactViewModel()
        {
            AddPeoplebtn = new Command(AddContact);
            conn = DependencyService.Get<SqliteInterface>().GetConnection();
        }

        [Obsolete]
        private async void AddContact()
        {
            if (string.IsNullOrEmpty(People.Nombre) || string.IsNullOrEmpty(People.Apellido) || string.IsNullOrEmpty(People.Celular) || string.IsNullOrEmpty(People.Telefono))
            {
                await App.Current.MainPage.DisplayAlert("Error", "Empty fields", "Ok");
            }
            else
            {
                Peoples.Add(People);
                conn.InsertAll(Peoples);               
                await PopupNavigation.PopAsync();
                var details = (from x in conn.Table<People>() select x).ToList();
                this.Peoples = details;
            }
        }
        
    }
}
