using System;
using System.Collections.Generic;
using System.Data;
using BE_GestionObras;
using DAL_GestionObras;

namespace MPP_GestionObras
{
    public class MPPClsCuadrilla
    {
        #region Lectura y Mapeo

        public List<BEClsCuadrilla> ListarTodo()
        {
            DALAcceso acceso = new DALAcceso();

            DataTable tabla = acceso.Leer(
                "SELECT Codigo, Nombre, CodigoObra FROM Cuadrilla"
            );

            List<BEClsCuadrilla> cuadrillas = new List<BEClsCuadrilla>();

            foreach (DataRow fila in tabla.Rows)
            {
                BEClsCuadrilla cuadrilla = MapearCuadrilla(fila);
                cuadrillas.Add(cuadrilla);
            }

            return cuadrillas;
        }

        private BEClsCuadrilla MapearCuadrilla(DataRow fila)
        {
            BEClsCuadrilla cuadrilla = new BEClsCuadrilla();

            cuadrilla.Codigo = Convert.ToInt32(fila["Codigo"]);
            cuadrilla.Nombre = fila["Nombre"].ToString();

            if (fila["CodigoObra"] != DBNull.Value)
            {
                cuadrilla.ObraAsignada = BuscarObra(Convert.ToInt32(fila["CodigoObra"]));
            }

            cuadrilla.Operarios = ListarOperariosDeCuadrilla(cuadrilla);

            return cuadrilla;
        }

        private BEClsObra BuscarObra(int codigoObra)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT * FROM Obra " +
                "WHERE Codigo = " + codigoObra;

            DataTable tabla = acceso.Leer(consulta);

            if (tabla.Rows.Count == 0)
            {
                return null;
            }

            DataRow fila = tabla.Rows[0];
            BEClsObra obra = new BEClsObra();

            obra.Codigo = Convert.ToInt32(fila["Codigo"]);
            obra.Nombre = fila["Nombre"].ToString();
            obra.Direccion = fila["Direccion"].ToString();
            obra.Estado = (EstadoObra)Enum.Parse(typeof(EstadoObra), fila["Estado"].ToString());
            obra.EstadoSupervision = (EstadoSupervision)Enum.Parse(typeof(EstadoSupervision), fila["EstadoSupervision"].ToString());
            obra.InformadaAlSupervisor = Convert.ToBoolean(fila["InformadaAlSupervisor"]);

            return obra;
        }

        private List<BEClsOperario> ListarOperariosDeCuadrilla(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT O.* FROM Operario O " +
                "INNER JOIN Cuadrilla_Operario CO " +
                "ON O.IdCodigo = CO.IdCodigoOperario " +
                "WHERE CO.CodigoCuadrilla = " + cuadrilla.Codigo;

            DataTable tabla = acceso.Leer(consulta);
            List<BEClsOperario> operarios = new List<BEClsOperario>();

            foreach (DataRow fila in tabla.Rows)
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

                operarios.Add(operario);
            }

            return operarios;
        }

        #endregion

        #region Persistencia

        public bool CrearCuadrilla(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "INSERT INTO Cuadrilla (Codigo, Nombre, CodigoObra) " +
                "VALUES (" + cuadrilla.Codigo + ", '" + cuadrilla.Nombre + "', NULL)";

            return acceso.Escribir(consulta);
        }

        public bool BuscarCuadrilla(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT * FROM Cuadrilla " +
                "WHERE Codigo = " + cuadrilla.Codigo;

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


        public List<BEClsCuadrilla> ListarPorContratista(string cuit)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT Codigo, Nombre, CodigoObra FROM Cuadrilla " +
                "WHERE CUITContratista = '" + cuit + "'";

            DataTable tabla = acceso.Leer(consulta);
            List<BEClsCuadrilla> cuadrillas = new List<BEClsCuadrilla>();

            foreach (DataRow fila in tabla.Rows)
            {
                BEClsCuadrilla cuadrilla = MapearCuadrilla(fila);
                cuadrillas.Add(cuadrilla);
            }

            return cuadrillas;
        }

        public bool ModificarCuadrilla(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Cuadrilla " +
                "SET Nombre = '" + cuadrilla.Nombre + "' " +
                "WHERE Codigo = " + cuadrilla.Codigo;

            return acceso.Escribir(consulta);
        }

        public bool EliminarCuadrilla(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "DELETE FROM Cuadrilla " +
                "WHERE Codigo = " + cuadrilla.Codigo;

            return acceso.Escribir(consulta);
        }

        public bool AgregarOperario(BEClsCuadrilla cuadrilla, BEClsOperario operario)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta = "INSERT INTO Cuadrilla_Operario " +
                "(CodigoCuadrilla, IdCodigoOperario) " +
                "VALUES (" +
                cuadrilla.Codigo + ", '" +
                operario.IdCodigo + "')";

            return acceso.Escribir(consulta);
        }

        public bool QuitarOperario(BEClsCuadrilla cuadrilla, BEClsOperario operario)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta = "DELETE FROM Cuadrilla_Operario " +
                "WHERE CodigoCuadrilla = " + cuadrilla.Codigo +
                " AND IdCodigoOperario = '" + operario.IdCodigo + "'";

            return acceso.Escribir(consulta);
        }

        public bool BuscarOperario(BEClsCuadrilla cuadrilla, BEClsOperario operario)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT * FROM Cuadrilla_Operario " +
                "WHERE CodigoCuadrilla = " + cuadrilla.Codigo +
                " AND IdCodigoOperario = '" + operario.IdCodigo + "'";

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

        public bool AsignarObra(BEClsCuadrilla cuadrilla, BEClsObra obra)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Cuadrilla " +
                "SET CodigoObra = " + obra.Codigo + " " +
                "WHERE Codigo = " + cuadrilla.Codigo;

            return acceso.Escribir(consulta);
        }

        public bool DesasignarObra(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Cuadrilla " +
                "SET CodigoObra = NULL " +
                "WHERE Codigo = " + cuadrilla.Codigo;

            return acceso.Escribir(consulta);
        }

        public bool IniciarObra(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Obra " +
                "SET Estado = 'EN_EJECUCION' " +
                "WHERE Codigo = " + cuadrilla.ObraAsignada.Codigo;

            return acceso.Escribir(consulta);
        }

        public bool InformarFinalizacionObra(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Obra " +
                "SET Estado = 'FINALIZADA' " +
                "WHERE Codigo = " + cuadrilla.ObraAsignada.Codigo;

            return acceso.Escribir(consulta);
        }
        #endregion
    }
}
