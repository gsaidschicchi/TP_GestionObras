using BE_GestionObras;
using DAL_GestionObras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace MPP_GestionObras
{
    public class MPPClsSupervisor
    {
        public List<BEClsSupervisor> ListarTodo()
        {
            DALAcceso acceso = new DALAcceso();
            List<BEClsSupervisor> supervisores = new List<BEClsSupervisor>();

            string consulta = "SELECT * FROM Supervisor";
            DataTable tabla = acceso.Leer(consulta);

            foreach (DataRow fila in tabla.Rows)
            {
                BEClsSupervisor supervisor = MapearSupervisor(fila);
                supervisores.Add(supervisor);
            }

            return supervisores;
        }

        private BEClsSupervisor MapearSupervisor(DataRow fila)
        {
            BEClsSupervisor supervisor = new BEClsSupervisor();

            supervisor.IdCodigo = fila["IdCodigo"].ToString();
            supervisor.DNI = fila["DNI"].ToString();
            supervisor.Nombre = fila["Nombre"].ToString();
            supervisor.Apellido = fila["Apellido"].ToString();
            supervisor.Telefono = fila["Telefono"].ToString();
            supervisor.SueldoBase = Convert.ToDouble(fila["SueldoBase"]);
            supervisor.IdSupervisor = Convert.ToInt32(fila["IdSupervisor"]);
            supervisor.Sector = fila["Sector"].ToString();

            return supervisor;
        }

        public bool CrearSupervisor(BEClsSupervisor supervisor)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "INSERT INTO Supervisor " +
                "(IdCodigo, DNI, Nombre, Apellido, Telefono, SueldoBase, IdSupervisor, Sector) " +
                "VALUES (" +
                "'" + supervisor.IdCodigo + "', " +
                "'" + supervisor.DNI + "', " +
                "'" + supervisor.Nombre + "', " +
                "'" + supervisor.Apellido + "', " +
                "'" + supervisor.Telefono + "', " +
                supervisor.SueldoBase.ToString(CultureInfo.InvariantCulture) + ", " +
                supervisor.IdSupervisor + ", " +
                "'" + supervisor.Sector + "')";

            return acceso.Escribir(consulta);
        }

        public bool BuscarSupervisor(BEClsSupervisor supervisor)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT * FROM Supervisor " +
                "WHERE IdCodigo = '" + supervisor.IdCodigo + "'";

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

        public bool ModificarSupervisor(BEClsSupervisor supervisor)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Supervisor " +
                "SET DNI = '" + supervisor.DNI + "', " +
                "Nombre = '" + supervisor.Nombre + "', " +
                "Apellido = '" + supervisor.Apellido + "', " +
                "Telefono = '" + supervisor.Telefono + "', " +
                "SueldoBase = " + supervisor.SueldoBase.ToString(CultureInfo.InvariantCulture) + ", " +
                "IdSupervisor = " + supervisor.IdSupervisor + ", " +
                "Sector = '" + supervisor.Sector + "' " +
                "WHERE IdCodigo = '" + supervisor.IdCodigo + "'";

            return acceso.Escribir(consulta);
        }

        public bool EliminarSupervisor(BEClsSupervisor supervisor)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "DELETE FROM Supervisor " +
                "WHERE IdCodigo = '" + supervisor.IdCodigo + "'";

            return acceso.Escribir(consulta);
        }
    }
}
