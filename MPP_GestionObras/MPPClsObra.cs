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
    public class MPPClsObra
    {
        public List<BEClsObra> ListarObras()
        {
            DALAcceso acceso = new DALAcceso();

            List<BEClsObra> obras = new List<BEClsObra>();

            string consulta = "SELECT Codigo, Nombre, Direccion, Estado, EstadoSupervision, InformadaAlSupervisor FROM Obra";

            DataTable tabla = acceso.Leer(consulta);

            foreach(DataRow d in tabla.Rows)
            {
                BEClsObra obra = MapearObra(d);
                obras.Add(obra);
            }

            return obras;

        }
        public BEClsObra MapearObra(DataRow fila)
        {
            BEClsObra obra = new BEClsObra();

            obra.Codigo = Convert.ToInt32(fila["Codigo"]);
            obra.Nombre = (fila["Nombre"]).ToString();
            obra.Direccion = (fila["Direccion"]).ToString();

            obra.Estado =
                (EstadoObra)Enum.Parse(
                    typeof(EstadoObra),
                    fila["Estado"].ToString()
                );

            obra.EstadoSupervision =
                (EstadoSupervision)Enum.Parse(
                    typeof(EstadoSupervision),
                    fila["EstadoSupervision"].ToString()
                );

            obra.InformadaAlSupervisor =
                Convert.ToBoolean(fila["InformadaAlSupervisor"]);

            return obra;
        }
        public bool CrearObra(BEClsObra obra)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta = "INSERT INTO Obra " +
                "(Codigo, Nombre, Direccion, Estado, EstadoSupervision, InformadaAlSupervisor) " +
                "VALUES (" +
                "'" + obra.Codigo + "', " +
                "'" + obra.Nombre + "', " +
                "'" + obra.Direccion + "', " +
                "'" + obra.Estado + "', " +
                "'" + obra.EstadoSupervision + "', " +
                (obra.InformadaAlSupervisor ? "1" : "0") + ")";

            return acceso.Escribir(consulta);
        }

        public bool BuscarObra(BEClsObra obra)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta = "SELECT * FROM Obra " +
                "WHERE Codigo = '" + obra.Codigo + "'";

            DataTable tabla = acceso.Leer(consulta);

            if (tabla.Rows.Count > 0) { return true; }
            else { return false; }
         }
        public bool ModificarObra(BEClsObra obra)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Obra " +
                "SET Nombre = '" + obra.Nombre + "', " +
                "Direccion = '" + obra.Direccion + "', " +
                "Estado = '" + obra.Estado + "', " +
                "EstadoSupervision = '" + obra.EstadoSupervision + "', " +
                "InformadaAlSupervisor = " + (obra.InformadaAlSupervisor ? "1" : "0") + " " +
                "WHERE Codigo = " + obra.Codigo;

            return acceso.Escribir(consulta);
        }
        public bool EliminarObra(BEClsObra obra)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "DELETE FROM Obra " +
                "WHERE Codigo = " + obra.Codigo;

            return acceso.Escribir(consulta);
        }
    }
}
