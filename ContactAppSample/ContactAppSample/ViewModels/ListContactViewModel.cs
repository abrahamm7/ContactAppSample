using ContactAppSample.Connections;
using ContactAppSample.Models;
using ContactAppSample.Views;
using Rg.Plugins.Popup.Services;
using GalaSoft.MvvmLight.Command;
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
        Frame frame = new Frame();
        ListView list = new ListView();
        public event PropertyChangedEventHandler PropertyChanged;
        public List<People> People { get; set; } = new List<People>();
        public ICommand TapFrame { get; set; }
        public ICommand Addbtn { get; set; }
        public ICommand EditContact { get; set; }
        public ICommand DeleteContact { get; set; }

        public SQLiteConnection conn;

        

        [Obsolete]
        public ListContactViewModel()
        {
            Addbtn = new Command(AddPeople);
            TapFrame = new Command(TapGesture);
            DeleteContact = new Command(Delete);
            EditContact = new Command(Edit);
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

        private void TapGesture(object sender)
        {
            frame.BackgroundColor = Color.White;
            var element = sender as Frame;
            element.BackgroundColor = Color.LightPink;
            frame = element;
        }

        private async void Edit(object sender)
        {
            var onSelect = list.SelectedItem as People;
            await App.Current.MainPage.Navigation.PushAsync(new EditContactPage(onSelect));
        }

        private async void Delete(object sender)
        {
            var onSelect = list.SelectedItem as People;
            await App.Current.MainPage.DisplayAlert("Mensaje", $"Desea eliminar a {onSelect.Nombre} ?", "ok");
            conn.Query<People>($"Delete From People where IdPeople = {onSelect.IdPeople}");
            var details = (from x in conn.Table<People>() select x).ToList();
            this.People = details;
        }

         
    }
}
