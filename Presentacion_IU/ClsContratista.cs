using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion_IU
{
    public class ClsContratista
    {
        // PROPIEDADES
        public string CUIT { get; set; }
        public string RazonSocial { get; set; }
        public List<ClsCuadrilla> Cuadrillas { get; set; } 

        // CONSTRUCTOR

        public ClsContratista(string cuit, string razonSocial)
        {
            CUIT = cuit;
            RazonSocial = razonSocial;
            Cuadrillas = new List<ClsCuadrilla>();
        }

        // constructor vacio
        public ClsContratista()
        {
            CUIT = string.Empty;
            RazonSocial = string.Empty;
            Cuadrillas = new List<ClsCuadrilla>();
        }

        // METODOS

        public List<ClsCuadrilla> AgregarCuadrilla(ClsCuadrilla cuadrilla) 
        {
            foreach (ClsCuadrilla c in Cuadrillas)
            {
                if (c == cuadrilla)
                {
                    throw new Exception("La cuadrilla ya esta incorporada.");
                }
            }
            Cuadrillas.Add(cuadrilla);
            return Cuadrillas;
        }

        public void AsignarObraACuadrilla(ClsCuadrilla cuadrilla, ClsObra obra)
        {
            if (cuadrilla == null)
            {
                throw new Exception("La cuadrilla no puede ser nula");
            }

            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula");
            }

            if (BuscarCuadrilla(cuadrilla))
            {
                cuadrilla.AsignarObra(obra);
            }
            else
            {
                throw new Exception("La cuadrilla no se encuentra en el listado");
            }      
        }

        public List<ClsCuadrilla> QuitarCuadrilla(ClsCuadrilla cuadrilla)
        {
            if (BuscarCuadrilla(cuadrilla))
            {
                Cuadrillas.Remove(cuadrilla);
            }
            else
            {
                throw new Exception("La cuadrilla no se encuentra en el listado.");
            }

            return Cuadrillas;
        }

        public void RecibirFinalizacionObra(ClsObra obra)
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

        public void InformarFinalizacionAlSupervisor(ClsObra obra)
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
            obra.CambiarEstadoSupervision(EstadoSupervision.PENDIENTE);
        }

        // METODOS AUXILIARES

        public bool BuscarCuadrilla(ClsCuadrilla cuadrilla)
        {
            bool encontrada = false;

            foreach (ClsCuadrilla c in Cuadrillas)
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
