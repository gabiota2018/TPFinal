using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.ViewModel
{
    public class PropietarioViewModel
    {
        public int IdPropietario { get; set; }
        public int? Dni { get; set; }
        public String Nombre { get; set; }
        public String Apellido { get; set; }
        public int? Telefono { get; set; }
        public String Mail { get; set; }
        public String Password { get; set; }
        public int  Borrado { get; set; }
    }
}