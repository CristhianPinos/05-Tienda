﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using
using System.Data.SqlClient;
using System.Data;

namespace _05_Tienda.Config
{
    internal class Conexion
    {
        private SqlConnection con = new SqlConnection("Server=CRISTHIANPINOS;database=Cuarto;uid=sa;pwd=123");

        public SqlConnection AbrirConexion()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            return con;
        }

        public SqlConnection CerrarConexcion()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
            return con;
        }
    }
}
