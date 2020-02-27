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
using Xamarin.Forms.OpenWhatsApp;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Messaging;

namespace ContactAppSample.ViewModels
{
    public class ListContactViewModel: INotifyPropertyChanged
    {
        Frame frame = new Frame();
        
        public event PropertyChangedEventHandler PropertyChanged;
        public List<People> Peoples { get; set; } = new List<People>();
        public ICommand TapFrame { get; set; }
        public ICommand Search { get; set; }
        public ICommand Addbtn { get; set; }
        public ICommand EditContact { get; set; }
        public ICommand DeleteContact { get; set; }            
        public ICommand Refresh { get; set; }
        public string StartLetter { get; set; }
        public bool State { get; set; } 
        public bool StateList { get; set; }
        public People People { get; set; } = new People();

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
            TapFrame = new Command(Tap);

            
           
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


        private async void Tap(object sender)
        {
            People = sender as People;
            var action = await App.Current.MainPage.DisplayActionSheet($"{People.Nombre}", "Cancel", "Call", "Send message via Whatsapp");
            switch (action)
            {
                case "Call":
                    var phoneDialer = CrossMessaging.Current.PhoneDialer;
                    if (phoneDialer.CanMakePhoneCall)
                        phoneDialer.MakePhoneCall(People.Telefono);                 
                    break;

                case "Send message via Whatsapp":
                    string message = await App.Current.MainPage.DisplayPromptAsync("Message", "Type your message here", placeholder: $"Hi, {People.Nombre}");
                    if (message != null)
                    {
                        Chat.Open(People.Telefono, message);
                    }                    
                    break;
            }
        }

       
    }
}
