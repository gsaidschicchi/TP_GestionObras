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
            conexion.Close();

            System.Diagnostics.Debug.WriteLine(
                "Conexión cerrada correctamente. Estado: " + conexion.State
            );
        }

        public DataTable Leer(string consulta)
        {
            SqlCommand comando = new SqlCommand();
            DataTable tabla = new DataTable();

            try
            {
                comando.CommandText = consulta;
                comando.Connection = AbrirConexion();

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                adaptador.Fill(tabla);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    CerrarConexion();
                }
            }

            return tabla;
        }

        public bool Escribir(string consulta)
        {
            bool resultadoOperacion = false;
            SqlCommand comando = new SqlCommand();

            try
            {
                comando.CommandText = consulta;
                comando.Connection = AbrirConexion();

                int filasAfectadas = comando.ExecuteNonQuery();

                if (filasAfectadas > 0)
                {
                    resultadoOperacion = true;
                }
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    CerrarConexion();
                }
            }

            return resultadoOperacion;
        }

        public bool EscribirTransaccion(string consulta1, string consulta2)
        {
            SqlTransaction transaccion = null;

            try
            {
                SqlConnection conexionAbierta = AbrirConexion();
                transaccion = conexionAbierta.BeginTransaction();

                SqlCommand comando1 = new SqlCommand();
                comando1.CommandText = consulta1;
                comando1.Connection = conexionAbierta;
                comando1.Transaction = transaccion;
                comando1.ExecuteNonQuery();

                SqlCommand comando2 = new SqlCommand();
                comando2.CommandText = consulta2;
                comando2.Connection = conexionAbierta;
                comando2.Transaction = transaccion;
                int filasAfectadas = comando2.ExecuteNonQuery();

                transaccion.Commit();
                CerrarConexion();

                if (filasAfectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                if (transaccion != null)
                {
                    transaccion.Rollback();
                }

                if (conexion.State == ConnectionState.Open)
                {
                    CerrarConexion();
                }

                throw;
            }
        }
    }
}
