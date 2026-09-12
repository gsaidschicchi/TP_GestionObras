using BE_GestionObras;
using MPP_GestionObras;
using System;
using System.Collections.Generic;

namespace BLL_GestionObras
{
    public class BLLClsContratista
    {
        public bool CrearContratista(BEClsContratista contratista)
        {
            if (contratista == null)
            {
                throw new Exception("La contratista no puede ser nula.");
            }

            if (string.IsNullOrWhiteSpace(contratista.CUIT))
            {
                throw new Exception("La contratista debe tener CUIT.");
            }

            if (string.IsNullOrWhiteSpace(contratista.RazonSocial))
            {
                throw new Exception("La contratista debe tener razón social.");
            }

            MPPClsContratista mpp = new MPPClsContratista();

            if (mpp.BuscarContratista(contratista))
            {
                throw new Exception("La contratista ya se encuentra dada de alta.");
            }

            return mpp.CrearContratista(contratista);
        }

        public List<BEClsContratista> ListarTodo()
        {
            MPPClsContratista mpp = new MPPClsContratista();
            return mpp.ListarTodo();
        }

        public bool ModificarContratista(BEClsContratista contratista)
        {
            if (contratista == null)
            {
                throw new Exception("La contratista no puede ser nula.");
            }

            MPPClsContratista mpp = new MPPClsContratista();
            return mpp.ModificarContratista(contratista);
        }

        public bool EliminarContratista(BEClsContratista contratista)
        {
            if (contratista == null)
            {
                throw new Exception("La contratista no puede ser nula.");
            }

            if (contratista.Cuadrillas.Count > 0)
            {
                throw new Exception("No se puede eliminar una contratista que tiene cuadrillas asignadas.");
            }

            MPPClsContratista mpp = new MPPClsContratista();
            return mpp.EliminarContratista(contratista);
        }

        public List<BEClsCuadrilla> AgregarCuadrilla(
            BEClsContratista contratista,
            BEClsCuadrilla cuadrilla)
        {
            if (contratista == null)
            {
                throw new Exception("La contratista no puede ser nula.");
            }

            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (BuscarCuadrilla(contratista, cuadrilla))
            {
                throw new Exception("La cuadrilla ya está incorporada.");
            }

            MPPClsContratista mpp = new MPPClsContratista();
            bool resultado = mpp.AgregarCuadrilla(contratista, cuadrilla);

            if (resultado)
            {
                contratista.Cuadrillas.Add(cuadrilla);
            }

            return contratista.Cuadrillas;
        }

        public List<BEClsCuadrilla> QuitarCuadrilla(
            BEClsContratista contratista,
            BEClsCuadrilla cuadrilla)
        {
            if (contratista == null)
            {
                throw new Exception("La contratista no puede ser nula.");
            }

            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (!BuscarCuadrilla(contratista, cuadrilla))
            {
                throw new Exception("La cuadrilla no se encuentra en el listado.");
            }

            MPPClsContratista mpp = new MPPClsContratista();
            bool resultado = mpp.QuitarCuadrilla(cuadrilla);

            if (resultado)
            {
                BEClsCuadrilla cuadrillaAEliminar = null;

                foreach (BEClsCuadrilla item in contratista.Cuadrillas)
                {
                    if (item.Codigo == cuadrilla.Codigo)
                    {
                        cuadrillaAEliminar = item;
                    }
                }

                if (cuadrillaAEliminar != null)
                {
                    contratista.Cuadrillas.Remove(cuadrillaAEliminar);
                }
            }

            return contratista.Cuadrillas;
        }

        public void AsignarObraACuadrilla(
            BEClsContratista contratista,
            BEClsCuadrilla cuadrilla,
            BEClsObra obra)
        {
            if (contratista == null)
            {
                throw new Exception("La contratista no puede ser nula.");
            }

            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula.");
            }

            if (BuscarCuadrilla(contratista, cuadrilla))
            {
                BLLClsCuadrilla bllCuadrilla = new BLLClsCuadrilla();
                bllCuadrilla.AsignarObra(cuadrilla, obra);
            }
            else
            {
                throw new Exception("La cuadrilla no se encuentra en el listado.");
            }
        }

        public void RecibirFinalizacionObra(BEClsObra obra)
        {
            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula.");
            }

            if (obra.Estado != EstadoObra.FINALIZADA)
            {
                throw new Exception("La obra todavía no fue informada como finalizada por la cuadrilla.");
            }
        }

        public void InformarFinalizacionAlSupervisor(BEClsObra obra)
        {
            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula.");
            }

            if (obra.Estado != EstadoObra.FINALIZADA)
            {
                throw new Exception("La obra todavía no se encuentra finalizada.");
            }

            obra.InformadaAlSupervisor = true;

            BLLClsObra bllObra = new BLLClsObra();
            bllObra.CambiarEstadoSupervision(obra, EstadoSupervision.PENDIENTE);
        }

        public bool BuscarCuadrilla(
            BEClsContratista contratista,
            BEClsCuadrilla cuadrilla)
        {
            if (contratista == null)
            {
                throw new Exception("La contratista no puede ser nula.");
            }

            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            bool encontrada = false;

            foreach (BEClsCuadrilla c in contratista.Cuadrillas)
            {
                if (c.Codigo == cuadrilla.Codigo)
                {
                    encontrada = true;
                    break;
                }
            }

            return encontrada;
        }
    }
}
