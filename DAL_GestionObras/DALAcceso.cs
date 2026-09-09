using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; // permite usar las clases para conectarte a SQL Server.

namespace DAL_GestionObras
{
    public class DALAcceso
    {
        private SqlConnection conexion; // declaro el objeto conexion

        // CONSTRUCTOR

        /*
        Data Source → instancia de SQL Server.
        Initial Catalog → la base GestionObras.
        Integrated Security=True → usa mi usuario de Windows.
        TrustServerCertificate=True → evita problemas con el certificado local. 
        */
        public DALAcceso()
        {
            string cadenaConexion =
                @"Data Source=(local)\SQLEXPRESS;
                  Initial Catalog=GestionObras;
                  Integrated Security=True;
                  TrustServerCertificate=True;";

            conexion = new SqlConnection(cadenaConexion); // instancio el objeto
        }

        // ABRO FISICAMENTE LA CONEXION CON SQL SERVER
        public SqlConnection AbrirConexion()
        {
            conexion.Open();
            
            System.Diagnostics.Debug.WriteLine(
                "Conexión abierta correctamente. Estado: " + conexion.State
            );
            return conexion;
        }

        // CIERRO LA CONEXION

        public void CerrarConexion()
        {

            System.Diagnostics.Debug.WriteLine(
                "Conexión cerrada correctamente. Estado: " + conexion.State
            );

            conexion.Close();
        }
    }
}
