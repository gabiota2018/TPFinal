using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.ViewModel
{
    public class InquilinoViewModel
    {

        
        public int IdInquilino { get; set; }
        public int Dni { get; set; }
        public String Apellido { get; set; }
        public String Nombre { get; set; }
        public String Direccion { get; set; }
        public int Telefono { get; set; }
        public int Borrado { get; set; }
    }
}