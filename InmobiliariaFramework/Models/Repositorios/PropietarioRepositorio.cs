using InmobiliariaFramework.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.Repositorios
{
    public class PropietarioRepositorio
    {
        public IList<PropietarioViewModel> ObtenerTodos()
        {
            List<PropietarioViewModel> lista = new List<PropietarioViewModel>();
            using (BDInmobiliariaEntities db = new BDInmobiliariaEntities())
            {
                lista = (from d in db.propietario
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
            return (lista);
         }

        public PropietarioViewModel ObtenerPorId(int id)
        {
            List<PropietarioViewModel> lista = new List<PropietarioViewModel>();
            using (BDInmobiliariaEntities db = new BDInmobiliariaEntities())
            {
              var  miPropietario = (from d in db.propietario
                                     where d.idPropietario==id
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
                                     }).FirstOrDefault();
                return (miPropietario);
            }
            
        }

       
    }
}