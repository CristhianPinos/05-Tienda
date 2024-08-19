
namespace _05_Tienda.Models
{
    using System;
    using System.Collections.Generic;
    using _05_Tienda.Config;
    using System.Data.SqlClient;
    using System.Data;

    class PaisModel
    {
        public int IdPais { get; set; }
        public string Detalle { get; set; }


        List<PaisModel> listaPaises = new List<PaisModel>();
        //instancia de base de datos
        private Conexion conexion = new Conexion();
        SqlCommand cmd = new SqlCommand();

        public List<PaisModel> todos()
        { // select * from
            string cadena = "select *  from paises";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexion.AbrirConexion());
            DataTable tabla = new DataTable();
            adapter.Fill(tabla);
            foreach (DataRow pais in tabla.Rows)
            {
                PaisModel nuevopais = new PaisModel
                {
                    IdPais = Convert.ToInt32(pais["IdPais"]),
                    Detalle = pais["Detalle"].ToString()
                };
                listaPaises.Add(nuevopais);
            }

            conexion.CerrarConexcion();
            return listaPaises;
        }
        public PaisModel uno(PaisModel pais)
        { //select * from where
            string cadena = "select * from paises where IdPais=" + pais.IdPais;
            cmd = new SqlCommand(cadena, conexion.AbrirConexion());
            SqlDataReader lector = cmd.ExecuteReader();

            lector.Read();
            PaisModel Paisregresa = new PaisModel
            {
                IdPais = Convert.ToInt32(lector["IdPais"]),
                Detalle = lector["Detalle"].ToString()
            };
            conexion.CerrarConexcion();
            return Paisregresa;
        }
        public string insertar(PaisModel pais)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "insert into paises(detalle) values('" + pais.Detalle + "')";
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "erorr: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexcion();
            }
        }
        public string actualizar(PaisModel pais)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "update paises set detalle='" + pais.Detalle + "' where IdPais=" + pais.IdPais;
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "erorr: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexcion();
            }
        }
        public string eliminar(PaisModel pais)
        {
            try
            {
                cmd.Connection = conexion.AbrirConexion();
                cmd.CommandText = "delete from paises where idpais =" + pais.IdPais;
                cmd.ExecuteNonQuery();
                return "ok";
            }
            catch (Exception ex)
            {
                return "erorr: " + ex.Message;
            }
            finally
            {
                conexion.CerrarConexcion();
            }
        }
}
}
/* uno() { // select * from where

            }
            insertar()
            {

            }
            actualizar()
            {

            }
            eliminar()
            {

            } */
