using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _05_Tienda.Config;
using System.Data.SqlClient;
using System.Data;

namespace _05_Tienda.Models
{
    internal class ProveedoresModel
    {
        public int IdProveedor { get; set; }
        public string NombreEmpresa { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        
        List<ProveedoresModel> listaProveedores = new List<ProveedoresModel>();
        private Conexion conexion = new Conexion();
        SqlCommand cmd = new SqlCommand();

        public List<ProveedoresModel> todos()
        {
            string cadena = "select * from proveedores";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexion.AbrirConexion());
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            foreach (DataRow proveedor in tabla.Rows)
            {
                ProveedoresModel nuevoProveedor = new ProveedoresModel
                {
                    IdProveedor = Convert.ToInt32(proveedor["IdProveedor"]),
                    NombreEmpresa = proveedor["NombreEmpresa"].ToString(),
                    Direccion = proveedor["Direccion"].ToString(),
                    Telefono = proveedor["Telefono"].ToString()
                };
                listaProveedores.Add(nuevoProveedor);
            }

            conexion.CerrarConexcion();
            return listaProveedores;
        }

        public ProveedoresModel uno(ProveedoresModel proveedor)
        {
            string cadena = "select * from proveedores where IdProveedor=" + proveedor.IdProveedor;
            cmd = new SqlCommand(cadena, conexion.AbrirConexion());
            SqlDataReader lector = cmd.ExecuteReader();

            lector.Read();
            ProveedoresModel ProveedorRegresa = new ProveedoresModel
            {
                IdProveedor = Convert.ToInt32(lector["IdProveedor"]),
                NombreEmpresa = lector["NombreEmpresa"].ToString(),
                Direccion = lector["Direccion"].ToString(),
                Telefono = lector["Telefono"].ToString()
            };
            conexion.CerrarConexcion();
            return ProveedorRegresa;
        }

        public string insertar(ProveedoresModel proveedor)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "insert into proveedores(NombreEmpresa, Direccion, Telefono) values('" + proveedor.NombreEmpresa + "', '" + proveedor.Direccion + "', '" + proveedor.Telefono + "')";
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

        public string actualizar(ProveedoresModel proveedor)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "update proveedores set NombreEmpresa='" + proveedor.NombreEmpresa + "', Direccion='" + proveedor.Direccion + "', Telefono='" + proveedor.Telefono + "' where IdProveedor=" + proveedor.IdProveedor;
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

        public string eliminar(ProveedoresModel proveedor)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "delete from proveedores where IdProveedor=" + proveedor.IdProveedor;
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
