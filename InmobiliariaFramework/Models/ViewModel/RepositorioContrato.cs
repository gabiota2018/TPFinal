using InmobiliariaFramework.Models.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace InmobiliariaFramework.Models.CarpetaRepo
{
    public class RepositorioContrato
    {
        private SqlConnection connection;
        public RepositorioContrato() 
        {
            try
            {
                connection = new SqlConnection(" Data Source=gabiota;Initial Catalog=BDInmobiliaria;Integrated Security=True;");

            }
            catch (Exception ex) {  }
        }
       
        public int Alta(Contrato p)
        {
            int res = -1;
           // using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"INSERT INTO Alquiler (Precio,Fecha_inicio, Fecha_fin,IdInquilino,IdInmueble,Borrado) " +
                    $"VALUES ('{p.Precio}', '{p.Fecha_inicio}','{p.Fecha_fin}','{p.IdInquilino}','{p.IdInmueble}',0) ;";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    command.CommandText = "SELECT SCOPE_IDENTITY()";
                    var id = command.ExecuteScalar();
                    p.IdAlquiler = Convert.ToInt32(id);
                    connection.Close();
                }
            }
            return res;
        }

        public int Baja(int id)
        {
            int res = -1;
           // using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE Alquiler SET Borrado = 0 WHERE idAlquiler = {id}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public int Modificacion(Contrato p)
        {
            int res = -1;
            //using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"UPDATE alquiler SET FechaFin={p.Fecha_fin} " +
                    $"WHERE Id ={p.IdAlquiler}";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    res = command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return res;
        }

        public Contrato ObtenerPorId(int id)
        {
            Contrato p = null;
           // using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT contrato.IdAlquiler, p.Nombre, p.Apellido, i.Nombre, i.Apellido, mueble.Direccion, contrato.FechaInicio,contrato.FechaFin, contrato.Precio,contrato.Inmueble, mueble.Propietario, i.IdInquilino" +
                    $" FROM Alquiler contrato, Propietario p, Inquilino i, Inmueble mueble WHERE contrato.IdInmueble=mueble.IdInmueble AND mueble.Propietario=p.IdPropietario AND contrato.Inquilino=i.IdInquilino AND contrato.IdAlquiler=@id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        p = new Contrato
                        {
                            IdAlquiler = reader.GetInt32(0),
                            Precio = reader.GetDecimal(1),
                            Fecha_inicio = reader.GetDateTime(2),
                            Fecha_fin = reader.GetDateTime(3),
                            IdInquilino = reader.GetInt32(4),
                            Inquilino = new InquilinoViewModel
                            {
                                IdInquilino = reader.GetInt32(4),
                                Nombre = reader.GetString(5),
                                Apellido = reader.GetString(6)
                            },
                            IdInmueble = reader.GetInt32(7),
                            Inmueble = new InmuebleModel
                            {
                                Duenio = new PropietarioViewModel
                                {
                                    IdPropietario = reader.GetInt32(8),
                                    Nombre = reader.GetString(9),
                                    Apellido = reader.GetString(10)
                                },
                                IdInmueble = reader.GetInt32(7),
                                Direccion = reader.GetString(11),
                                Precio = reader.GetDecimal(12)
                            },

                        };
                    }
                    connection.Close();
                }
            }
            return p;
        }

        public IList<Contrato> ObtenerTodos()
        {
            IList<Contrato> res = new List<Contrato>();
          //  using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"SELECT contrato.IdAlquiler, p.Nombre, p.Apellido, i.Nombre, i.Apellido, mueble.Direccion, contrato.FechaInicio,contrato.FechaFin, contrato.Precio,contrato.Inmueble, mueble.Propietario, i.IdInquilino" +
                    $" FROM Alquiler contrato, Propietario p, Inquilino i, Inmueble mueble WHERE contrato.IdInmueble=mueble.IdInmueble AND mueble.Propietario=p.IdPropietario AND contrato.Inquilino=i.IdInquilino";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.CommandType = CommandType.Text;
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Contrato  contrato = new Contrato
                        {
                            IdAlquiler = reader.GetInt32(0),
                            Precio = reader.GetDecimal(1),
                            Fecha_inicio = reader.GetDateTime(2),
                            Fecha_fin = reader.GetDateTime(3),
                            IdInquilino = reader.GetInt32(4),
                            Inquilino = new InquilinoViewModel
                            {
                                IdInquilino = reader.GetInt32(4),
                                Nombre = reader.GetString(5),
                                Apellido = reader.GetString(6)
                            },
                            IdInmueble = reader.GetInt32(7),
                            Inmueble = new InmuebleModel
                            {
                               Duenio = new PropietarioViewModel
                                {
                                    IdPropietario = reader.GetInt32(8),
                                    Nombre = reader.GetString(9),
                                    Apellido = reader.GetString(10)
                                },
                                Direccion = reader.GetString(11),
                                Precio = reader.GetDecimal(12)
                            },

                        };
                        res.Add(contrato);

                    };
                    connection.Close();
                }
                return res;
            }
            
        }
    }
}