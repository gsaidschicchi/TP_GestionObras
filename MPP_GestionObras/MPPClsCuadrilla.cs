using System;
using System.Collections.Generic;
using System.Data;
using BE_GestionObras;
using DAL_GestionObras;

namespace MPP_GestionObras
{
    public class MPPClsCuadrilla
    {
        public List<BEClsCuadrilla> ListarTodo()
        {
            DALAcceso acceso = new DALAcceso();

            DataTable tabla = acceso.Leer(
                "SELECT Codigo, Nombre, CodigoObra FROM Cuadrilla"
            );

            List<BEClsCuadrilla> cuadrillas =
                new List<BEClsCuadrilla>();

            foreach (DataRow fila in tabla.Rows)
            {
                BEClsCuadrilla cuadrilla =
                    MapearCuadrilla(fila);

                cuadrillas.Add(cuadrilla);
            }

            return cuadrillas;
        }


        private BEClsCuadrilla MapearCuadrilla(DataRow fila)
        {
            BEClsCuadrilla cuadrilla =
                new BEClsCuadrilla();

            cuadrilla.Codigo =
                Convert.ToInt32(fila["Codigo"]);

            cuadrilla.Nombre =
                fila["Nombre"].ToString();

            return cuadrilla;
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
                " AND IDCodigoOperario = '" + operario.IdCodigo + "'";

            return acceso.Escribir(consulta);
        }

        public bool BuscarOperario(BEClsCuadrilla cuadrilla, BEClsOperario operario)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT * FROM Cuadrilla_Operario" +
                "WHERE CodigoCuadrilla = " + cuadrilla.Codigo +
                " AND IDCodigoOperario = " + operario.IdCodigo + "'";

            DataTable tabla = acceso.Leer(consulta);

            if (tabla.Rows.Count > 0)
            {
                return true;
            }
            else return false;
        }

        public bool AsignarObra(BEClsCuadrilla cuadrilla, BEClsObra obra)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE * FROM Cuadrilla" +
                "SET CodigoObra = "  +
                "WHERE Codigo = " + cuadrilla.Codigo + "'";

            return acceso.Escribir(consulta);
        }
         
        public bool DesasignarObra(BEClsCuadrilla cuadrilla) // NO NECESITO PASAR UNA OBRA COMO PARAMETRO, AFECTA LA PROPIEDAD DE CUADRILLA
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
    }
}