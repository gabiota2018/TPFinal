using InmobiliariaFramework.Models;
using InmobiliariaFramework.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InmobiliariaFramework.Controllers
{
    public class PagosController : Controller
    {
        // GET: Pagos
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Alquiler");
        }
        public ActionResult VerPagos()
        {
            TempData["IdAlquiler"] = TempData["IdAlquiler"];
            return View();
        }
        public ActionResult List()
        {
            int id = Convert.ToInt32(TempData["IdAlquiler"]);
             List<PagosViewModel> lista = new List<PagosViewModel>();
            PagosViewModel miPago = new PagosViewModel();
            using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
             {
                 lista = (from d in db.pago
                          orderby d.nroPago ascending
                          where d.borrado == 0 && d.idAlquiler==id
                          select new PagosViewModel
                          {
                              IdPago = d.idPago,
                              NroPago = d.nroPago,
                              IdAlquiler = d.idAlquiler, 
                              Fecha = d.fecha,
                              Importe = d.importe, 
                             }).ToList();
             }
            TempData["IdAlquiler"]=id;
            return View(lista);
        }
        public ActionResult Nuevo()
        {
            List<PagosViewModel> lista = new List<PagosViewModel>();
            PagosViewModel miPago = new PagosViewModel();
            int id = Convert.ToInt32(TempData["IdAlquiler"]);
            miPago.IdAlquiler = id;
            miPago.Fecha = DateTime.Now;
         
            using (SqlConnection connection = new SqlConnection("Data Source=gabiota;Initial Catalog=BDInmobiliaria;Integrated Security=True;"))
            {
                string sql = $"SELECT Max(nroPago) FROM pago WHERE IdAlquiler=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        miPago.NroPago = reader.GetInt32(0)+1;
                   }
                    connection.Close();
                }
                //------------------------------------------------------------
                sql = $"SELECT importe FROM pago WHERE IdAlquiler=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        miPago.Importe = reader.GetDecimal(0);
                    }
                    connection.Close();
                }
                //------------------------------------------------------------
                {
                    sql = $"INSERT INTO pago (nroPago, IdAlquiler,fecha,importe,borrado) " +
                       $"VALUES ('{miPago.NroPago}', '{miPago.IdAlquiler}','{miPago.Fecha}','{miPago.Importe}',0)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.CommandType = CommandType.Text;
                        connection.Open();
                        int res = command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            //------------------------------------------------------------
            using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
            {
                lista = (from d in db.pago
                         orderby d.nroPago ascending
                         where d.borrado == 0 && d.idAlquiler == id
                         select new PagosViewModel
                         {
                             IdPago = d.idPago,
                             NroPago = d.nroPago,
                             IdAlquiler = d.idAlquiler,
                             Fecha = d.fecha,
                             Importe = d.importe,
                         }).ToList();
            }
            TempData["IdAlquiler"] = TempData["IdAlquiler"];
            return RedirectToAction("VerPagos", "Pagos");
          
        }
        public ActionResult Delete(int Id)
        {
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1()) //dentro de las llaves que siguen existe la conexión
                {
                    var miPago = db.pago.Find(Id);
                    db.pago.Remove(miPago);
                    db.SaveChanges();
                }
             }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
            TempData["IdAlquiler"] = TempData["IdAlquiler"];
            return RedirectToAction("VerPagos", "Pagos");
        }
    }
}