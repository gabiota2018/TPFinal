using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.ViewModel
{
    public class InmuebleViewModel
    {
        public int IdInmueble { get; set; }
        public String Direccion { get; set; }
        public int? Ambientes { get; set; }
        public String Tipo { get; set; }
        public String Uso { get; set; }
        public decimal? Precio { get; set; }
        public int? Disponible { get; set; }

        [Display(Name = "Propietario")]
        public int? IdPropietario { get; set; }
    }
}