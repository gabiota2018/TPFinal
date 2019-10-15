using InmobiliariaFramework.Models;
using InmobiliariaFramework.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InmobiliariaFramework.Controllers
{
    public class InmuebleController : Controller
    {
        // GET: Inmueble
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
              List<InmuebleViewModel> lista = new List<InmuebleViewModel>();
              using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
              {

                  lista = (from d in db.inmueble
                           orderby d.idInmueble ascending
                           select new InmuebleViewModel
                           {
                               IdInmueble = d.idInmueble,
                               Direccion = d.direccion,
                               Ambientes = d.ambientes,
                               Tipo = d.tipo,
                               Uso = d.uso,
                               Precio = d.precio,
                               Disponible = d.disponible,
                               IdPropietario= d.idPropietario,
                           }).ToList();
               }
              return View(lista); 
        }
        public ActionResult Nuevo()
        {
            return View();
        }
        public ActionResult ElegirDuenio()
        {
            List<PropietarioViewModel> lista = new List<PropietarioViewModel>();
            using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
            {
                lista = (from d in db.propietario
                         where d.borrado == 0
                         orderby d.apellido ascending
                         select new PropietarioViewModel
                         {
                             IdPropietario = d.idPropietario,
                             Dni = d.dni,
                             Nombre = d.nombre,
                             Apellido = d.apellido,
                             Telefono = d.telefono,
                             Mail = d.mail,
                             Password = d.password,
                         }).ToList();
            }
            return View(lista);
        }
        public ActionResult AgregarDuenio(int id)
        {
            TempData["IdPropietario"] = id;
            return View();
        }
    [HttpPost]
        public ActionResult Save(InmuebleViewModel model)
        {
            int id = Convert.ToInt32(TempData["IdPropietario"]);
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
                {
                    var miInmueble = new inmueble();
                    miInmueble.direccion = model.Direccion; 
                    miInmueble.ambientes = Convert.ToInt32(model.Ambientes);
                    miInmueble.tipo = model.Tipo;
                    miInmueble.uso = model.Uso;
                    miInmueble.precio = Convert.ToDecimal(model.Precio);
                    miInmueble.disponible = 1;
                    miInmueble.idPropietario =id;
                    miInmueble.borrado = 0;
                    db.inmueble.Add(miInmueble);
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }

        public ActionResult Edit(int idInmueble)
        {
           InmuebleViewModel model = new InmuebleViewModel();
            using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
            {
                var oInmueble = db.inmueble.Find(idInmueble);
                model.Direccion = oInmueble.direccion;
                model.Ambientes = oInmueble.ambientes;
                model.Tipo = oInmueble.tipo;
                model.Uso = oInmueble.uso;
                model.Precio = oInmueble.precio;
                model.Disponible = oInmueble.disponible;
                model.IdPropietario = oInmueble.idPropietario;
                model.IdInmueble = oInmueble.idInmueble;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(InmuebleViewModel model)
        {
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1()) //dentro de las llaves que siguen existe la conexión
                {
                    var oInmueble = db.inmueble.Find(model.IdInmueble);
                    oInmueble.idInmueble = model.IdInmueble;
                    oInmueble.direccion = model.Direccion;
                    oInmueble.ambientes=Convert.ToInt32(model.Ambientes);
                    oInmueble.tipo=model.Tipo;
                    oInmueble.uso = model.Uso;
                    oInmueble.precio= Convert.ToInt32(model.Precio);
                    oInmueble.disponible= Convert.ToInt32(model.Disponible);
                    oInmueble.idPropietario = Convert.ToInt32(model.IdPropietario);
                    db.Entry(oInmueble).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1()) //dentro de las llaves que siguen existe la conexión
                {
                    var oInmueble = db.inmueble.Find(Id);
                    db.inmueble.Remove(oInmueble);
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult FormularioBuscar()
        {
            return View();
        }
        public ActionResult Busqueda(int Ambientes,string Tipo,string Uso, decimal Precio)
        {
            List<InmuebleViewModel> lista = new List<InmuebleViewModel>();
             using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
             {

                 lista = (from d in db.inmueble
                          where d.ambientes==Ambientes
                          && d.tipo==Tipo
                          && d.uso== Uso
                          && d.precio==Precio
                          orderby d.idInmueble ascending
                          select new InmuebleViewModel
                          {
                              IdInmueble = d.idInmueble,
                              Direccion = d.direccion,
                              Ambientes = d.ambientes,
                              Tipo = d.tipo,
                              Uso = d.uso,
                              Precio = d.precio,
                              Disponible = d.disponible,
                              IdPropietario = d.idPropietario,
                          }).ToList();
            return View(lista);
           }
        }
        public ActionResult BusquedaPorDuenio(int IdPropietario)
        {
           
            List<InmuebleViewModel> lista = new List<InmuebleViewModel>();
            using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
            {

                lista = (from d in db.inmueble
                         where d.idPropietario==IdPropietario
                         orderby d.idInmueble ascending
                         select new InmuebleViewModel
                         {
                             IdInmueble = d.idInmueble,
                             Direccion = d.direccion,
                             Ambientes = d.ambientes,
                             Tipo = d.tipo,
                             Uso = d.uso,
                             Precio = d.precio,
                             Disponible = d.disponible,
                             IdPropietario = d.idPropietario,
                         }).ToList();
                return View(lista);
            }
        }
    }
}