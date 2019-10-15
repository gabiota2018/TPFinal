using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InmobiliariaFramework.Models;
using InmobiliariaFramework.Models.ViewModel;

namespace InmobiliariaFramework.Controllers
{
    public class PropietarioController : Controller
    {
        // GET: Propietario
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
           List<PropietarioViewModel> lista = new List<PropietarioViewModel>();
           using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
            {
                lista = (from d in db.propietario
                        orderby d.apellido ascending
                        select new PropietarioViewModel
                        {
                            IdPropietario = d.idPropietario,
                            Dni=d.dni,
                            Nombre=d.nombre,
                            Apellido=d.apellido,
                            Telefono= d.telefono,
                            Mail=d.mail,
                            Password=d.password,
                        }).ToList();
           }
          return View(lista);
        }
        public ActionResult Nuevo()
        {

            ViewBag.Inmueble = TempData["Id"];
            ViewBag.Propietario = TempData["Id"];
            return View();
        }
        [HttpPost]
        public ActionResult Save(PropietarioViewModel model)
        {
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
                {
                    var miPropietario = new propietario();
                    miPropietario.dni = Convert.ToInt32(model.Dni);
                    miPropietario.nombre = model.Nombre;
                    miPropietario.apellido = model.Apellido;
                    miPropietario.telefono = model.Telefono;
                    miPropietario.mail = model.Mail;
                    miPropietario.password = model.Password;
                    db.propietario.Add(miPropietario);
                    db.SaveChanges();
                }
                return Content("1");
               
            }
            catch(Exception ex) {
              return Content(ex.Message);
            }
        }

        public ActionResult Edit(int Id)
        {
            PropietarioViewModel model = new PropietarioViewModel();
            using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
            {
                var oPropietario = db.propietario.Find(Id);
                model.Dni = oPropietario.dni;
                model.Nombre = oPropietario.nombre;
                model.Apellido = oPropietario.apellido;
                model.Telefono = oPropietario.telefono;
                model.Mail = oPropietario.mail;
                model.Password = oPropietario.password;
                model.IdPropietario = oPropietario.idPropietario;
            }
            return View(model);
        }
        [HttpPost]
       
        public ActionResult Update(PropietarioViewModel model)
        {
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
                {
                    var oPropietario = db.propietario.Find(model.IdPropietario);
                    oPropietario.dni = model.Dni;
                    oPropietario.nombre = model.Nombre;
                    oPropietario.apellido = model.Apellido;
                    oPropietario.telefono = model.Telefono;
                    oPropietario.mail = model.Mail;
                    oPropietario.password = model.Password;
                    oPropietario.idPropietario = model.IdPropietario;
                    db.Entry(oPropietario).State = System.Data.Entity.EntityState.Modified;
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
                    var oPropietario = db.propietario.Find(Id);
                    db.propietario.Remove(oPropietario);
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
       
    }
}