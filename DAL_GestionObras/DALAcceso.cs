using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; // permite usar las clases para conectarte a SQL Server.
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DataTable Leer(string consulta)
        {
            // Creo un comando SQL
            SqlCommand comando = new SqlCommand();

            // Le digo qué consulta ejecutar
            comando.CommandText = consulta;

            // Le indico con qué conexión debe ejecutarla
            comando.Connection = AbrirConexion();

            // Creo un adaptador que ejecutará el comando
            // y traerá los resultados
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);

            // Creo una tabla vacía en memoria
            DataTable tabla = new DataTable();

            // Lleno esa tabla con lo que devolvió SQL Server
            adaptador.Fill(tabla);

            // Cierro la conexión
            CerrarConexion();

            // Devuelvo los datos
            return tabla;
        }

        public bool Escribir(string consulta)
        {
            bool resultadoOperacion = false;

            SqlCommand comando = new SqlCommand();

            comando.CommandText = consulta;
            comando.Connection = AbrirConexion();

            int filasAfectadas = comando.ExecuteNonQuery();

            if (filasAfectadas > 0)
            {
                resultadoOperacion = true;
            }

            CerrarConexion();

            return resultadoOperacion;
        }
    }
}
