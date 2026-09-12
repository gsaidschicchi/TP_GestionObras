using BE_GestionObras;
using MPP_GestionObras;
using System;
using System.Collections.Generic;

namespace BLL_GestionObras
{
    public class BLLClsCuadrilla
    {
        public bool CrearCuadrilla(BEClsCuadrilla cuadrilla)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula.");
            }

            if (string.IsNullOrWhiteSpace(cuadrilla.Nombre))
            {
                throw new Exception("La cuadrilla debe tener un nombre.");
            }

            MPPClsCuadrilla mpp = new MPPClsCuadrilla();

            if (mpp.BuscarCuadrilla(cuadrilla))
            {
                throw new Exception("La cuadrilla ya se encuentra dada de alta.");
            }

            return mpp.CrearCuadrilla(cuadrilla);
        }

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

            MPPClsCuadrilla mpp = new MPPClsCuadrilla();

            bool resultado = mpp.AgregarOperario(cuadrilla, operario);

            if (resultado)
            {
                cuadrilla.Operarios.Add(operario);
            }

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

            if (!BuscarOperario(cuadrilla, operario))
            {
                throw new Exception("El operario no se encuentra en la cuadrilla.");
            }

            MPPClsCuadrilla mpp = new MPPClsCuadrilla();
            bool resultado = mpp.QuitarOperario(cuadrilla, operario);

            if (resultado)
            {
                BEClsOperario operarioAEliminar = null;

                foreach (BEClsOperario item in cuadrilla.Operarios)
                {
                    if (item.IdCodigo == operario.IdCodigo)
                    {
                        operarioAEliminar = item;
                    }
                }

                if (operarioAEliminar != null)
                {
                    cuadrilla.Operarios.Remove(operarioAEliminar);
                }
            }

            return cuadrilla.Operarios;
        }

        public bool AsignarObra(BEClsCuadrilla cuadrilla, BEClsObra obra)
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

            MPPClsCuadrilla mpp = new MPPClsCuadrilla();
            bool resultado = mpp.AsignarObra(cuadrilla, obra);

            if (resultado == true)
            {
                cuadrilla.ObraAsignada = obra;
            }

            return resultado;
        }

        public bool DesasignarObra(BEClsCuadrilla cuadrilla, BEClsObra obra)
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

            if (cuadrilla.ObraAsignada.Codigo != obra.Codigo)
            {
                throw new Exception("La obra indicada no corresponde a la obra asignada a la cuadrilla.");
            }

            MPPClsCuadrilla mpp = new MPPClsCuadrilla();
            bool resultado = mpp.DesasignarObra(cuadrilla);

            if (resultado)
            {
                cuadrilla.ObraAsignada = null;
            }

            return resultado;
        }

        public bool IniciarObra(BEClsCuadrilla cuadrilla)
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

            MPPClsCuadrilla mpp = new MPPClsCuadrilla();
            bool resultado = mpp.IniciarObra(cuadrilla);

            if (resultado)
            {
                cuadrilla.ObraAsignada.Estado = EstadoObra.EN_EJECUCION;
            }

            return resultado;
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

            MPPClsCuadrilla mpp = new MPPClsCuadrilla();
            bool resultado = mpp.InformarFinalizacionObra(cuadrilla);

            if (resultado)
            {
                cuadrilla.ObraAsignada.Estado = EstadoObra.FINALIZADA;
            }
        }

        public bool BuscarOperario(BEClsCuadrilla cuadrilla, BEClsOperario operario)
        {
            MPPClsCuadrilla mpp = new MPPClsCuadrilla();
            return mpp.BuscarOperario(cuadrilla, operario);
        }

        public List<BEClsCuadrilla> ListarTodo()
        {
            MPPClsCuadrilla mpp = new MPPClsCuadrilla();
            return mpp.ListarTodo();
        }
    }
}
