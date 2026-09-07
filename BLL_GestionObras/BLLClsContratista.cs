using BE_GestionObras;
using System;
using System.Collections.Generic;

namespace BLL_GestionObras
{
    public class BLLClsContratista
    {
        // METODOS

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

            if (!BuscarCuadrilla(contratista, cuadrilla))
            {
                contratista.Cuadrillas.Add(cuadrilla);
            }
            else
            {
                throw new Exception("La cuadrilla ya está incorporada.");
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

            if (BuscarCuadrilla(contratista, cuadrilla))
            {
                contratista.Cuadrillas.Remove(cuadrilla);
            }
            else
            {
                throw new Exception("La cuadrilla no se encuentra en el listado.");
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
                // Se crea una instancia de la capa de negocio de Cuadrilla
                // para utilizar sus reglas y métodos.
                BLLClsCuadrilla bllCuadrilla = new BLLClsCuadrilla();

                // Se delega la asignación de la obra a BLLClsCuadrilla,
                // evitando repetir la lógica de negocio.
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
                throw new Exception(
                    "La obra todavía no fue informada como finalizada por la cuadrilla.");
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

            // Se registra que la contratista ya informó
            // la finalización al Supervisor.
            obra.InformadaAlSupervisor = true;

            // Se crea una instancia de la lógica de negocio de Obra.
            BLLClsObra bllObra = new BLLClsObra();

            // La obra queda pendiente de supervisión.
            bllObra.CambiarEstadoSupervision(
                obra,
                EstadoSupervision.PENDIENTE);
        }


        // METODOS AUXILIARES

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
                if (c == cuadrilla)
                {
                    encontrada = true;
                    break;
                }
            }

            return encontrada;
        }
    }
}