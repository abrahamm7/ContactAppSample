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
        
        public event PropertyChangedEventHandler PropertyChanged;
        public List<People> Peoples { get; set; } = new List<People>();
        public ICommand TapFrame { get; set; }
        public ICommand Addbtn { get; set; }
        public ICommand EditContact { get; set; }
        public ICommand DeleteContact { get; set; }            
        public ICommand Refresh { get; set; }
        public bool State { get; set; } 
        public bool StateList { get; set; }
        public People SelectPeople { get; set; }

        public SQLiteConnection conn;

        [Obsolete]
        public ListContactViewModel()
        {
            //Refresh ListView//
            Refresh = new Command(() => {
                
                this.Peoples = conn.Query<People>("Select * from People");
                State = true;
                State = false;
            });
            
            Addbtn = new Command(AddPeople);
            //TapFrame = new Command(TapGesture);
            TapFrame = new Command<Frame>((sender) =>
            {
                sender.BackgroundColor = Color.White;
                var element = sender as Frame;
                element.BackgroundColor = Color.Accent;
                sender = element;
            });
            //Delete contact//
            DeleteContact = new Command<People>((sender) =>
            {                
                DisplayMessage(sender);                                    
                this.Peoples = (from x in conn.Table<People>() select x).ToList();
            });
            //Edit contact//
            EditContact = new Command<People>((sender) =>
            {                
                NavigatePage(sender);
            });



            conn = DependencyService.Get<SqliteInterface>().GetConnection();
            conn.CreateTable<People>();
            var details = (from x in conn.Table<People>() select x).ToList();         
            this.Peoples = details;
        }

        //Navigate to Popup//
        [Obsolete]
        private async void AddPeople()
        {            
            await App.Current.MainPage.Navigation.PushAsync(new AddContactPage());
        }
        //Display actionsheet//
        async void DisplayMessage(People people) 
        {
            var action = await App.Current.MainPage.DisplayActionSheet("Message", "Yes", "Cancel", $"Do you want delete {people.Nombre} ?");
            switch (action)
            {
                case "Yes":
                    conn.Query<People>($"Delete From People where IdPeople = {people.IdPeople}");
                    var details = (from x in conn.Table<People>() select x).ToList();
                    this.Peoples = details;
                    break;
            }
            
        }
        //Navigate to edit contact page//
        async void NavigatePage(People people)
        {
            await App.Current.MainPage.Navigation.PushAsync(new EditContactPage(people));
        }
         
    }
}
