using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.ViewModel
{
    public class AlquilerViewModel
    {
        public int IdAlquiler { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime? Fecha_fin { get; set; }
        [Display(Name = "Código del Inquilino")]
        public int IdInquilino { get; set; }
        [Display(Name = "Código del Inmueble")]
        public int IdInmueble { get; set; }
        public int Borrado { get; set; }
    }
}