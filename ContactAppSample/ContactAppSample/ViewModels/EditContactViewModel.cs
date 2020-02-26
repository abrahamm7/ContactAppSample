using ContactAppSample.Models;
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

        public ICommand Savebtn { get; set; }

        public EditContactViewModel(People people)
        {
            People.Nombre = people.Nombre;
            People.Apellido = people.Apellido;
            People.Telefono = people.Telefono;
            People.Celular = people.Celular;
            Savebtn = new Command(Save);
        }

        private void Save()
        {

        }
    }
}
