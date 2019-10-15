using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.CarpetaRepo
{
    public class Auxiliar
    {
        [Display(Name = "N°")]
        public int IdAlquiler { get; set; }
       public decimal Precio { get; set; }
       public String Fecha_inicio { get; set; }
        public String Fecha_fin { get; set; }
        [Display(Name = "Código")]
        public int IdInquilino { get; set; }
       
        public int Dni { get; set; }
       
        public String Apellido { get; set; }
       
        public String Nombre { get; set; }
        [Display(Name = "Código")]
        public int IdInmueble { get; set; }
        
        public String Direccion { get; set; }
        [Display(Name = "Código")]
        public int IdPropietario { get; set; }
        [Display(Name = "Dni")]
        public int? DniP { get; set; }
        [Display(Name = "Nombre")]
        public String NombreP { get; set; }
        [Display(Name = "Apellido")]
        public String ApellidoP { get; set; }

    }
}