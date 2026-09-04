using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion_IU
{
    public class ClsCuadrilla
    {
        // PROPIEDADES
        public int? Codigo { get; set; } // ? -> permite nullear un valor
        public string Nombre { get; set; }
        public ClsObra ObraAsignada { get; set; }
        public List<ClsOperario> Operarios { get; set; }

        // CONSTRUCTOR
        public ClsCuadrilla(int codigo, string nombre, ClsObra obra)
        {
            Codigo = codigo;
            Nombre = nombre;
            ObraAsignada = obra;
            Operarios = new List<ClsOperario>();
        }

        // constructor sin parametros
        public ClsCuadrilla()
        {
            Codigo = null;
            Nombre = string.Empty;
            ObraAsignada = null;
            Operarios = new List<ClsOperario>();
        }

        // METODOS
        public List<ClsOperario> AgregarOperario(ClsOperario operario)
        {
            if (BuscarOperario(operario))
            {
                throw new Exception("El operario ya se encuentra en la cuadrilla.");
            }
            else
            {
                Operarios.Add(operario);
            }
            return Operarios;
        }

        public List<ClsOperario> QuitarOperario(ClsOperario operario)
        {
            if (BuscarOperario(operario))
            {
                Operarios.Remove(operario);
            }
            else
            {
                throw new Exception("El operario no se encuentra en la cuadrilla.");
            }

            return Operarios;
        }
        public void AsignarObra(ClsObra obra)
        {
            if(obra == null)
            {
                throw new Exception("La obra no puede ser nula");
            }

            if(ObraAsignada != null)
            {
                throw new Exception("La cuadrilla ya tiene una obra asignada.");
            }

            ObraAsignada = obra;
        }

        public void DesasignarObra(ClsObra obra)
        {
            if (ObraAsignada == null)
            {
                throw new Exception("La cuadrilla no tiene una obra asignada.");
            }

            if (ObraAsignada != obra)
            {
                throw new Exception("La obra indicada no corresponde a la obra asignada a la cuadrilla.");
            }

            ObraAsignada = null;
        }

        public void IniciarObra()
        {
            if (ObraAsignada == null)
            {
                throw new Exception("La cuadrilla no tiene una obra asignada.");
            }

            if (ObraAsignada.Estado == EstadoObra.FINALIZADA)
            {
                throw new Exception("La obra ya se encuentra finalizada.");
            }

            if (ObraAsignada.Estado == EstadoObra.EN_EJECUCION)
            {
                throw new Exception("La obra ya se encuentra iniciada.");
            }

            ObraAsignada.CambiarEstadoObra(EstadoObra.EN_EJECUCION);
        }

        public void InformarFinalizacionObra()
        {
            if (ObraAsignada == null)
            {
                throw new Exception("La cuadrilla no tiene una obra asignada.");
            }

            if (ObraAsignada.Estado == EstadoObra.PENDIENTE)
            {
                throw new Exception("La obra todavía no fue iniciada.");
            }

            if (ObraAsignada.Estado == EstadoObra.FINALIZADA)
            {
                throw new Exception("La obra ya se encuentra finalizada.");
            }

            ObraAsignada.CambiarEstadoObra(EstadoObra.FINALIZADA);
        }

        // METODOS AUXILIARES
        public bool BuscarOperario(ClsOperario operario)
        {
            foreach (ClsOperario o in Operarios)
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
