using BE_GestionObras;
using System;
using System.Collections.Generic;

namespace BLL_GestionObras
{
    public class BLLClsCuadrilla
    {
        // METODOS

        public List<BEClsOperario> AgregarOperario(BEClsCuadrilla cuadrilla, BEClsOperario operario)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (operario == null)
            {
                throw new Exception("El operario no puede ser nulo.");
            }

            if (BuscarOperario(cuadrilla, operario))
            {
                throw new Exception("El operario ya se encuentra en la cuadrilla.");
            }

            cuadrilla.Operarios.Add(operario);

            return cuadrilla.Operarios;
        }

        public List<BEClsOperario> QuitarOperario(BEClsCuadrilla cuadrilla, BEClsOperario operario)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (operario == null)
            {
                throw new Exception("El operario no puede ser nulo.");
            }

            if (BuscarOperario(cuadrilla, operario))
            {
                cuadrilla.Operarios.Remove(operario);
            }
            else
            {
                throw new Exception("El operario no se encuentra en la cuadrilla.");
            }

            return cuadrilla.Operarios;
        }

        public void AsignarObra(BEClsCuadrilla cuadrilla, BEClsObra obra)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula.");
            }

            if (cuadrilla.ObraAsignada != null)
            {
                throw new Exception("La cuadrilla ya tiene una obra asignada.");
            }

            cuadrilla.ObraAsignada = obra;
        }

        public void DesasignarObra(BEClsCuadrilla cuadrilla, BEClsObra obra)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula.");
            }

            if (cuadrilla.ObraAsignada == null)
            {
                throw new Exception("La cuadrilla no tiene una obra asignada.");
            }

            if (cuadrilla.ObraAsignada != obra)
            {
                throw new Exception("La obra indicada no corresponde a la obra asignada a la cuadrilla.");
            }

            cuadrilla.ObraAsignada = null;
        }

        public void IniciarObra(BEClsCuadrilla cuadrilla)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (cuadrilla.ObraAsignada == null)
            {
                throw new Exception("La cuadrilla no tiene una obra asignada.");
            }

            if (cuadrilla.Operarios.Count == 0)
            {
                throw new Exception("La cuadrilla debe tener al menos un operario para iniciar la obra.");
            }

            if (cuadrilla.ObraAsignada.Estado == EstadoObra.FINALIZADA)
            {
                throw new Exception("La obra ya se encuentra finalizada.");
            }

            if (cuadrilla.ObraAsignada.Estado == EstadoObra.EN_EJECUCION)
            {
                throw new Exception("La obra ya se encuentra iniciada.");
            }

            cuadrilla.ObraAsignada.Estado = EstadoObra.EN_EJECUCION;
        }

        public void InformarFinalizacionObra(BEClsCuadrilla cuadrilla)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (cuadrilla.ObraAsignada == null)
            {
                throw new Exception("La cuadrilla no tiene una obra asignada.");
            }

            if (cuadrilla.ObraAsignada.Estado == EstadoObra.PENDIENTE)
            {
                throw new Exception("La obra todavía no fue iniciada.");
            }

            if (cuadrilla.ObraAsignada.Estado == EstadoObra.FINALIZADA)
            {
                throw new Exception("La obra ya se encuentra finalizada.");
            }

            cuadrilla.ObraAsignada.Estado = EstadoObra.FINALIZADA;
        }

        // METODO AUXILIAR

        public bool BuscarOperario(BEClsCuadrilla cuadrilla, BEClsOperario operario)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            foreach (BEClsOperario o in cuadrilla.Operarios)
            {
                if (o == operario)
                {
                    return true;
                }
            }

            return false;
        }
    }
}