using InmobiliariaFramework.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.CarpetaRepo
{
    public class Contrato
    {
        public int IdAlquiler { get; set; }
        public decimal Precio { get; set; }
        public DateTime Fecha_inicio { get; set; }
        public DateTime? Fecha_fin { get; set; }
        public int IdInquilino { get; set; }
        public InquilinoViewModel Inquilino{ get; set; }
        public int IdInmueble { get; set; }
        public InmuebleModel Inmueble{ get; set; }
        public int Borrado { get; set; }
    }
}