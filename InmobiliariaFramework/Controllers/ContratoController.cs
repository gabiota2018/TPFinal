using InmobiliariaFramework.Models;
using InmobiliariaFramework.Models.CarpetaRepo;
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
   
    public class ContratoController : Controller
    {
        // GET: Contrato
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult List()
        {
            IList<InmuebleModel> lista = new List<InmuebleModel>();
            using (SqlConnection connection = new SqlConnection("Data Source=gabiota;Initial Catalog=BDInmobiliaria;Integrated Security=True;"))
            {
                string sql = $"SELECT IdInmueble, Direccion, Ambientes,Tipo,Uso,Precio,Disponible,i.borrado,p.IdPropietario, p.Nombre, p.Apellido,p.Dni " +
                    $" FROM Inmueble i JOIN Propietario p ON (p.IdPropietario=i.IdPropietario)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        InmuebleModel i = new InmuebleModel
                        {
                            IdInmueble = Convert.ToInt32(reader["IdInmueble"]),
                            Direccion = Convert.ToString(reader["Direccion"]),
                            Ambientes = Convert.ToInt32(reader["Ambientes"]),
                            Tipo = Convert.ToString(reader["Tipo"]),
                            Uso = Convert.ToString(reader["Uso"]),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Disponible = Convert.ToInt32(reader["Disponible"]),
                            Borrado = Convert.ToInt32(reader["Borrado"]),
                            IdPropietario = Convert.ToInt32(reader["IdPropietario"]),
                            Duenio = new PropietarioViewModel()
                            {
                                Dni = Convert.ToInt32(reader["Dni"]),
                                Nombre = Convert.ToString(reader["Nombre"]),
                                Apellido = Convert.ToString(reader["Apellido"]),
                            }

                        };
                        lista.Add(i);
                    }
                    connection.Close();
                }
            }
            return View(lista);
        }
        public ActionResult Nuevo(int id)
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
                             Direccion = d.direccion,
                             Telefono = d.telefono,
                         }).ToList();
            }
            TempData["IdInmueble"] =id;
            return View(lista);
        }
       
        public ActionResult AgregarInquilino(int id)
        {
            PropietarioViewModel miPropietario = new PropietarioViewModel();
            InquilinoViewModel miInquilino = new InquilinoViewModel();
            AlquilerViewModel miAlquiler = new AlquilerViewModel();
            InmuebleViewModel m = new InmuebleViewModel();
            Contrato c = new Contrato();
            int idInmueble = Convert.ToInt32(TempData["IdInmueble"]);
            using (SqlConnection connection = new SqlConnection("Data Source=gabiota;Initial Catalog=BDInmobiliaria;Integrated Security=True;"))
            {
                string sql = $"SELECT IdInmueble, Direccion, Ambientes, Tipo, Uso, Precio, p.IdPropietario,p.Dni, p.Nombre, p.Apellido" +
                     $" FROM Inmueble i INNER JOIN Propietario p ON i.IdPropietario = p.IdPropietario";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        m = new InmuebleViewModel
                        {
                            IdInmueble = Convert.ToInt32(reader["IdInmueble"]),
                            Direccion = Convert.ToString(reader["Direccion"]),
                            Ambientes = Convert.ToInt32(reader["Ambientes"]),
                            Tipo = Convert.ToString(reader["Tipo"]),
                            Uso = Convert.ToString(reader["Uso"]),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                        };
                        miPropietario = new PropietarioViewModel
                        {
                            IdPropietario = Convert.ToInt32(reader["IdPropietario"]),
                            Dni = Convert.ToInt32(reader["Dni"]),
                            Nombre = Convert.ToString(reader["Nombre"]),
                            Apellido = Convert.ToString(reader["Apellido"]),
                        };

                    }
                    connection.Close();
                    string sql1 = $"SELECT  IdInquilino,Dni, Nombre, Apellido FROM Inquilino " +
                    $" WHERE IdInquilino=@id AND Borrado=0";
                    using (SqlCommand command1 = new SqlCommand(sql1, connection))
                    {
                        command1.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        command1.CommandType = CommandType.Text;
                        connection.Open();
                        var reader1 = command1.ExecuteReader();
                        if (reader1.Read())
                        {
                            miInquilino = new InquilinoViewModel
                            {
                                IdInquilino = Convert.ToInt32(reader1["IdInquilino"]),
                                Dni = Convert.ToInt32(reader1["Dni"]),
                                Nombre = Convert.ToString(reader1["Nombre"]),
                                Apellido = Convert.ToString(reader1["Apellido"]),
                            };

                        }
                        connection.Close();
                    }
                }
                TempData["IdInmueble"] = m.IdInmueble;
                TempData["Precio"] = m.Precio;
              
                ViewBag.DniPropietario = miPropietario.Dni;
                ViewBag.NPropietario = miPropietario.Nombre;
                ViewBag.APropietario = miPropietario.Apellido;

                TempData["IdInquilino"] = miInquilino.IdInquilino;
                ViewBag.DniInquilino = miInquilino.Dni;
                ViewBag.NInquilino = miInquilino.Nombre;
                ViewBag.AInquilino = miInquilino.Apellido;

                return View(m);
            }
        }
        
    }
}