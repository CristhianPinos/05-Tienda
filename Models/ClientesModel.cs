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
    internal class ClientesModel
    {
        public int IdCliente { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        List<ClientesModel> listaClientes = new List<ClientesModel>();
        private Conexion conexion = new Conexion();
        SqlCommand cmd = new SqlCommand();

        public List<ClientesModel> todos()
        {
            string cadena = "select * from clientes";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexion.AbrirConexion());
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            foreach (DataRow cliente in tabla.Rows)
            {
                ClientesModel nuevoCliente = new ClientesModel
                {
                    IdCliente = Convert.ToInt32(cliente["IdCliente"]),
                    Nombres = cliente["Nombres"].ToString(),
                    Apellidos = cliente["Apellidos"].ToString(),
                    Direccion = cliente["Direccion"].ToString(),
                    Telefono = cliente["Telefono"].ToString(),
                    Correo = cliente["Correo"].ToString()
                };
                listaClientes.Add(nuevoCliente);
            }

            conexion.CerrarConexcion();
            return listaClientes;
        }

        public ClientesModel uno(ClientesModel cliente)
        {
            string cadena = "select * from clientes where IdCliente=" + cliente.IdCliente;
            cmd = new SqlCommand(cadena, conexion.AbrirConexion());
            SqlDataReader lector = cmd.ExecuteReader();

            lector.Read();
            ClientesModel ClienteRegresa = new ClientesModel
            {
                IdCliente = Convert.ToInt32(lector["IdCliente"]),
                Nombres = lector["Nombres"].ToString(),
                Apellidos = lector["Apellidos"].ToString(),
                Direccion = lector["Direccion"].ToString(),
                Telefono = lector["Telefono"].ToString(),
                Correo = lector["Correo"].ToString()
            };
            conexion.CerrarConexcion();
            return ClienteRegresa;
        }

        public string insertar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "insert into clientes(Nombres, Apellidos, Direccion, Telefono, Correo) values('" + cliente.Nombres + "', '" + cliente.Apellidos + "', '" + cliente.Direccion + "', '" + cliente.Telefono + "', '" + cliente.Correo + "')";
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

        public string actualizar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "update clientes set Nombres='" + cliente.Nombres + "', Apellidos='" + cliente.Apellidos + "', Direccion='" + cliente.Direccion + "', Telefono='" + cliente.Telefono + "', Correo='" + cliente.Correo + "' where IdCliente=" + cliente.IdCliente;
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

        public string eliminar(ClientesModel cliente)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "delete from clientes where IdCliente=" + cliente.IdCliente;
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
