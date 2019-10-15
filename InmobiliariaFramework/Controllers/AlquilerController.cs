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
    public class AlquilerController : Controller
    {
        // GET: Alquiler
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Edit (int id)
        {
            Auxiliar p = null;
            InquilinoViewModel i = null;
            PropietarioViewModel a = null;
            InmuebleViewModel m = null;
            using (SqlConnection connection = new SqlConnection("Data Source=gabiota;Initial Catalog=BDInmobiliaria;Integrated Security=True;"))
            {
                //string sql = $"SELECT idAlquiler,a.idInquilino,a.idInmueble,i.dni,i.apellido,i.nombre,p.dni,p.nombre,p.apellido,m.direccion,m.precio,m.idPropietario,m.Ambientes,m.Tipo,m.Uso" +
                //$"FROM Alquiler a, Propietario p, Inmueble m, Inquilino i WHERE a.idInquilino = i.idInquilino and a.idInmueble = m.idInmueble and m.idPropietario = p.idPropietario and a.idAlquiler = @id  ";
                string sql = $"SELECT IdAlquiler,IdInquilino,IdInmueble FROM alquiler WHERE IdAlquiler=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Auxiliar();

                        p.IdAlquiler = reader.GetInt32(0);
                        p.IdInquilino = reader.GetInt32(1);
                        p.IdInmueble = reader.GetInt32(2);
                       
                    }
                    connection.Close();
                }
                id = p.IdInquilino;
                //--------------------------------------------------------
                sql = $"SELECT dni,nombre,apellido FROM inquilino WHERE IdInquilino=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p.Dni = reader.GetInt32(0);
                        p.Nombre = reader.GetString(1);
                        p.Apellido = reader.GetString(2);
                    }
                    connection.Close();
                }
                id = p.IdInmueble;
                //--------------------------------------------------------
                sql = $"SELECT direccion,IdPropietario,precio FROM Inmueble WHERE IdInmueble=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p.Direccion = reader.GetString(0);
                        p.IdPropietario = reader.GetInt32(1);
                        p.Precio= reader.GetDecimal(2);
                    }
                    connection.Close();
                }
                id = p.IdPropietario;
                //--------------------------------------------------------
                sql = $"SELECT dni,nombre,apellido FROM propietario WHERE IdPropietario=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p.DniP = reader.GetInt32(0);
                        p.NombreP = reader.GetString(1);
                        p.ApellidoP = reader.GetString(2);
                    }
                    connection.Close();
                }

            }
         TempData["IdInmueble"] = p.IdInmueble;
         TempData["Precio"] = p.Precio;
         TempData["IdInquilino"] = p.IdInquilino;
         return View(p);
      }
        public ActionResult List2()
        {
            List<Auxiliar> lista = new List<Auxiliar>();
            List<Auxiliar> listado = new List<Auxiliar>();
            List<PropietarioViewModel> propietarios = new List<PropietarioViewModel>();
            List<InquilinoViewModel> inquilinos = new List<InquilinoViewModel>();
            List<InmuebleViewModel> inmuebles = new List<InmuebleViewModel>();
            using (SqlConnection connection = new SqlConnection("Data Source=gabiota;Initial Catalog=BDInmobiliaria;Integrated Security=True;"))
            {
                string sql = $"SELECT IdAlquiler,Precio,Fecha_inicio,Fecha_fin,IdInquilino,IdInmueble" +
                    $" FROM alquiler where borrado=0";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Auxiliar p = new Auxiliar
                        {
                            IdAlquiler = Convert.ToInt32(reader["IdAlquiler"]),
                            Precio = Convert.ToDecimal(reader["Precio"]),
                            Fecha_inicio = Convert.ToString(reader["Fecha_inicio"]),
                            Fecha_fin = Convert.ToString(reader["Fecha_fin"]),
                            IdInquilino = Convert.ToInt32(reader["IdInquilino"]),
                            IdInmueble = Convert.ToInt32(reader["IdInmueble"]),
                        };
                        lista.Add(p);
                    }
                    connection.Close();
                }
                //----------------------------------------------------------------
                sql = $"SELECT IdInquilino,Dni,Nombre,Apellido FROM inquilino where borrado=0";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader1 = command.ExecuteReader();
                    while (reader1.Read())
                    {
                        InquilinoViewModel i = new InquilinoViewModel
                        {
                            IdInquilino= Convert.ToInt32(reader1["IdInquilino"]),
                            Dni = Convert.ToInt32(reader1["Dni"]),
                            Nombre = Convert.ToString(reader1["Nombre"]),
                            Apellido= Convert.ToString(reader1["Apellido"]),
                       };
                        inquilinos.Add(i);
                    }
                    connection.Close();
                }
                foreach (Auxiliar auxs in lista) {
                    foreach (InquilinoViewModel m in inquilinos)
                    {
                        if (auxs.IdInquilino == m.IdInquilino) {
                            auxs.Nombre = m.Nombre;
                            auxs.Apellido = m.Apellido;
                            auxs.Dni = m.Dni;
                            listado.Add(auxs);
                        }
                     }
                }
                //----------------------------------------------------------------
                sql = $"SELECT IdInmueble,Direccion,IdPropietario FROM inmueble where borrado=0";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader2 = command.ExecuteReader();
                    while (reader2.Read())
                    {
                        InmuebleViewModel a= new InmuebleViewModel
                        {
                            IdInmueble = Convert.ToInt32(reader2["IdInmueble"]),
                            Direccion = Convert.ToString(reader2["Direccion"]),
                            IdPropietario = Convert.ToInt32(reader2["IdPropietario"]),
                        };
                        inmuebles.Add(a);
                    }
                    connection.Close();
                }
                lista = new List<Auxiliar>();
                foreach (Auxiliar auxs in listado)
                {
                    foreach (InmuebleViewModel m in inmuebles)
                    {
                        if (auxs.IdInmueble == m.IdInmueble)
                        {
                            auxs.IdInmueble = m.IdInmueble;
                            auxs.Direccion = m.Direccion;
                            auxs.IdPropietario =Convert.ToInt32 (m.IdPropietario);
                            lista.Add(auxs);
                        }
                    }
                }
                //-------------------------------------------------------------------
                sql = $"SELECT IdPropietario,Dni,Nombre,Apellido FROM propietario where borrado=0";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader3 = command.ExecuteReader();
                    while (reader3.Read())
                    {
                        PropietarioViewModel pro = new PropietarioViewModel
                        {
                            IdPropietario = Convert.ToInt32(reader3["IdPropietario"]),
                            Dni = Convert.ToInt32(reader3["Dni"]),
                            Nombre = Convert.ToString(reader3["Nombre"]),
                            Apellido = Convert.ToString(reader3["Apellido"]),
                        };
                        propietarios.Add(pro);
                    }
                    connection.Close();
                }
                listado = new List<Auxiliar>();
                foreach (Auxiliar auxs in lista)
                {
                    foreach (PropietarioViewModel d in propietarios)
                    {
                        if (auxs.IdPropietario == d.IdPropietario)
                        {
                            auxs.NombreP = d.Nombre;
                            auxs.ApellidoP = d.Apellido;
                            auxs.DniP = Convert.ToInt32(d.Dni);
                            listado.Add(auxs);
                        }
                    }
                }
            }
            return View(listado);
        }
     
        [HttpPost]
        public ActionResult Save2(AlquilerViewModel model)
        {
            int IdInmueble = Convert.ToInt32(TempData["IdInmueble"]);
            int IdInquilino = Convert.ToInt32(TempData["IdInquilino"]);
            decimal precio = Convert.ToDecimal(TempData["Precio"]);

            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
                {
                    var oAlquiler = new alquiler();
                    oAlquiler.fecha_inicio = model.Fecha_inicio;
                    oAlquiler.fecha_fin = model.Fecha_fin;
                    oAlquiler.IdInquilino = IdInquilino;
                    oAlquiler.IdInmueble = IdInmueble;
                    oAlquiler.precio = precio;
                    oAlquiler.borrado = 0;
                    db.alquiler.Add(oAlquiler);
                    db.SaveChanges();
                }
                return Content("1");
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }
       
        public ActionResult Rescindir(int id)//rescindir contrato
        {
            try
            {
                using (BDInmobiliariaEntities1 db = new BDInmobiliariaEntities1())
                {
                    var a = db.alquiler.Find(id);
                    a.fecha_fin = DateTime.Today;
                    
                    db.Entry(a).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        public ActionResult VerPagos(int id)
        {
            TempData["IdAlquiler"] = id;
            return RedirectToAction("VerPagos", "Pagos");
        }
    }
}