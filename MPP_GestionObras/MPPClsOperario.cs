using BE_GestionObras;
using DAL_GestionObras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_GestionObras
{
    public class MPPClsOperario
    {
        public List<BEClsOperario> ListarTodo()
        {
            DALAcceso acceso = new DALAcceso();

            List<BEClsOperario> operarios = new List<BEClsOperario>(); // CREO MI LISTA DE OPERARIOS

            string consulta = "SELECT * FROM Operario";

            DataTable tabla = acceso.Leer(consulta);

            foreach(DataRow d in tabla.Rows)
            {
                BEClsOperario operario = MapearOperario(d);
                operarios.Add(operario);
            }

            return operarios;

        }

        private BEClsOperario MapearOperario (DataRow fila)
        {
            BEClsOperario operario = new BEClsOperario();

            operario.IdCodigo = fila["IdCodigo"].ToString();
            operario.DNI = fila["DNI"].ToString();
            operario.Nombre = fila["Nombre"].ToString();
            operario.Apellido = fila["Apellido"].ToString();
            operario.Telefono = fila["Telefono"].ToString();
            operario.SueldoBase = Convert.ToDouble(fila["SueldoBase"]);
            operario.Legajo = Convert.ToInt32(fila["Legajo"]);
            operario.Especialidad = fila["Especialidad"].ToString();

            return operario;
        }
        public bool CrearOperario(BEClsOperario operario)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "INSERT INTO Operario " +
                "(IdCodigo, DNI, Nombre, Apellido, Telefono, SueldoBase, Legajo, Especialidad) " +
                "VALUES (" +
                "'" + operario.IdCodigo + "', " +
                "'" + operario.DNI + "', " +
                "'" + operario.Nombre + "', " +
                "'" + operario.Apellido + "', " +
                "'" + operario.Telefono + "', " +
                operario.SueldoBase + ", " +
                operario.Legajo + ", " +
                "'" + operario.Especialidad + "')";

            return acceso.Escribir(consulta);
        }

        public bool BuscarOperario(BEClsOperario operario)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT * FROM Operario " +
                "WHERE IdCodigo = '" + operario.IdCodigo + "'";

            DataTable tabla = acceso.Leer(consulta);

            if (tabla.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
