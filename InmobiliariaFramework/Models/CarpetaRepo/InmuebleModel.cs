using InmobiliariaFramework.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.CarpetaRepo
{
    public class InmuebleModel
    {
        [Display(Name = "Código")]
        public int IdInmueble { get; set; }
        public String Direccion { get; set; }
        [Display(Name = "Cantidad de Ambientes")]
        public int? Ambientes { get; set; }
        [Display(Name = "Tipo de inmueble")]
        public String Tipo { get; set; }
        public String Uso { get; set; }
        public decimal? Precio { get; set; }
        public int? Disponible { get; set; }
        public int? IdPropietario { get; set; }
        [Display(Name = "Propietario")]
        public PropietarioViewModel Duenio { get; set; }
        public int Borrado { get; set; }
    }
}