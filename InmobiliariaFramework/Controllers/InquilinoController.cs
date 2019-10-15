using InmobiliariaFramework.Models;
using InmobiliariaFramework.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InmobiliariaFramework.Controllers
{
    public class InquilinoController : Controller
    {
        // GET: Inquilino
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            List<InquilinoViewModel> lista = new List<InquilinoViewModel>();
            using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
            {

                lista = (from d in db.inquilino
                         orderby d.apellido ascending
                         select new InquilinoViewModel
                         {
                             IdInquilino = d.idInquilino,
                             Dni = d.dni,
                             Apellido = d.apellido,
                             Nombre = d.nombre,
                             Direccion=d.direccion,
                             Telefono = d.telefono,
                         }).ToList();
            }
            return View(lista);
        }
        public ActionResult Nuevo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(InquilinoViewModel model)
        {
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1()) 
                {
                    var oInquilino = new inquilino();
                    oInquilino.dni = model.Dni;
                    oInquilino.apellido = model.Apellido;
                    oInquilino.nombre = model.Nombre;
                    oInquilino.direccion = model.Direccion;
                    oInquilino.telefono = model.Telefono;
                    db.inquilino.Add(oInquilino);
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        public ActionResult Edit(int Id)
        {
            InquilinoViewModel model = new InquilinoViewModel();
            using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
            {
               var oInquilino = db.inquilino.Find(Id);
                model.IdInquilino = oInquilino.idInquilino;
               model.Dni= oInquilino.dni ;
               model.Apellido= oInquilino.apellido;
               model.Nombre= oInquilino.nombre;
               model.Direccion= oInquilino.direccion;
               model.Telefono= oInquilino.telefono;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Update(InquilinoViewModel model)
        {
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
                {
                    var miInquilino = db.inquilino.Find(model.IdInquilino);
                    miInquilino.idInquilino = model.IdInquilino;
                    miInquilino.dni = model.Dni;
                    miInquilino.apellido = model.Apellido;
                    miInquilino.nombre = model.Nombre;
                    miInquilino.direccion = model.Direccion;
                    miInquilino.telefono = model.Telefono;
                    db.Entry(miInquilino).State = System.Data.Entity.EntityState.Modified;
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
                    var oInquilino = db.inquilino.Find(Id);
                    db.inquilino.Remove(oInquilino);
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