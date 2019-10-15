using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.ViewModel
{
    public class PagosViewModel
    {
        [Display(Name = "Comprobante N°")]
        public int IdPago{ get; set; }
        [Display(Name = "Pago N°")]
        public int NroPago { get; set; }
        [Display(Name = "Código de alquiler")]
        public int IdAlquiler{ get; set; }
        public DateTime Fecha { get; set; }
        public decimal Importe { get; set; }
        public int Borrado { get; set; }
    }
}