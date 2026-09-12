using BE_GestionObras;
using DAL_GestionObras;
using System;
using System.Collections.Generic;
using System.Data;

namespace MPP_GestionObras
{
    public class MPPClsContratista
    {
        public List<BEClsContratista> ListarTodo()
        {
            DALAcceso acceso = new DALAcceso();

            List<BEClsContratista> contratistas = new List<BEClsContratista>();

            string consulta = "SELECT CUIT, RazonSocial FROM Contratista";

            DataTable tabla = acceso.Leer(consulta);

            foreach (DataRow fila in tabla.Rows)
            {
                BEClsContratista contratista = MapearContratista(fila);
                contratistas.Add(contratista);
            }

            return contratistas;
        }

        private BEClsContratista MapearContratista(DataRow fila)
        {
            BEClsContratista contratista = new BEClsContratista();

            contratista.CUIT = fila["CUIT"].ToString();
            contratista.RazonSocial = fila["RazonSocial"].ToString();

            MPPClsCuadrilla mppCuadrilla = new MPPClsCuadrilla();
            contratista.Cuadrillas = mppCuadrilla.ListarPorContratista(contratista.CUIT);

            return contratista;
        }

        public bool CrearContratista(BEClsContratista contratista)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "INSERT INTO Contratista (CUIT, RazonSocial) " +
                "VALUES ('" + contratista.CUIT + "', '" + contratista.RazonSocial + "')";

            return acceso.Escribir(consulta);
        }

        public bool BuscarContratista(BEClsContratista contratista)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT * FROM Contratista " +
                "WHERE CUIT = '" + contratista.CUIT + "'";

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

        public bool ModificarContratista(BEClsContratista contratista)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Contratista " +
                "SET RazonSocial = '" + contratista.RazonSocial + "' " +
                "WHERE CUIT = '" + contratista.CUIT + "'";

            return acceso.Escribir(consulta);
        }

        public bool EliminarContratista(BEClsContratista contratista)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "DELETE FROM Contratista " +
                "WHERE CUIT = '" + contratista.CUIT + "'";

            return acceso.Escribir(consulta);
        }

        public bool AgregarCuadrilla(BEClsContratista contratista, BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Cuadrilla " +
                "SET CUITContratista = '" + contratista.CUIT + "' " +
                "WHERE Codigo = " + cuadrilla.Codigo;

            return acceso.Escribir(consulta);
        }

        public bool QuitarCuadrilla(BEClsCuadrilla cuadrilla)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "UPDATE Cuadrilla " +
                "SET CUITContratista = NULL " +
                "WHERE Codigo = " + cuadrilla.Codigo;

            return acceso.Escribir(consulta);
        }
    }
}
