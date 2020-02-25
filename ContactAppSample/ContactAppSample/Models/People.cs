using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContactAppSample.Models
{
    public class People
    {
        [PrimaryKey, AutoIncrement]
        public int IdPeople { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
    }
}
