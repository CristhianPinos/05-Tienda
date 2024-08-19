using _05_Tienda.Config;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_Tienda.Models
{
    internal class CiudadModel
    {
        public int IdCiudad { get; set; }
        public string Detalle { get; set; }
        public int IdPais { get; set; }

        List<CiudadModel> listaCiudades = new List<CiudadModel>();
        private Conexion conexion = new Conexion();
        SqlCommand cmd = new SqlCommand();

        public List<CiudadModel> todos()
        {
            string cadena = "select * from ciudades";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexion.AbrirConexion());
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            foreach (DataRow ciudad in tabla.Rows)
            {
                CiudadModel nuevaCiudad = new CiudadModel
                {
                    IdCiudad = Convert.ToInt32(ciudad["IdCiudad"]),
                    Detalle = ciudad["Detalle"].ToString(),
                    IdPais = Convert.ToInt32(ciudad["IdPais"])
                };
                listaCiudades.Add(nuevaCiudad);
            }

            conexion.CerrarConexcion();
            return listaCiudades;
        }

        public CiudadModel uno(CiudadModel ciudad)
        {
            string cadena = "select * from ciudades where IdCiudad=" + ciudad.IdCiudad;
            cmd = new SqlCommand(cadena, conexion.AbrirConexion());
            SqlDataReader lector = cmd.ExecuteReader();

            lector.Read();
            CiudadModel CiudadRegresa = new CiudadModel
            {
                IdCiudad = Convert.ToInt32(lector["IdCiudad"]),
                Detalle = lector["Detalle"].ToString(),
                IdPais = Convert.ToInt32(lector["IdPais"])
            };
            conexion.CerrarConexcion();
            return CiudadRegresa;
        }

        public string insertar(CiudadModel ciudad)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "insert into ciudades(Detalle, IdPais) values('" + ciudad.Detalle + "', " + ciudad.IdPais + ")";
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexcion();
            }
        }

        public string actualizar(CiudadModel ciudad)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "update ciudades set Detalle='" + ciudad.Detalle + "', IdPais=" + ciudad.IdPais + " where IdCiudad=" + ciudad.IdCiudad;
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexcion();
            }
        }

        public string eliminar(CiudadModel ciudad)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "delete from ciudades where IdCiudad=" + ciudad.IdCiudad;
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexcion();
            }
        }
    }
}
