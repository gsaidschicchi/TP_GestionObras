using BE_GestionObras;
using MPP_GestionObras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_GestionObras
{
    public class BLLClsObra
    {
        public bool CambiarEstadoObra(BEClsObra obra, EstadoObra nuevoEstado)
        {
            obra.Estado = nuevoEstado;

            return ModificarObra(obra);
        }

        public bool CambiarEstadoSupervision(BEClsObra obra, EstadoSupervision nuevoEstado)
        {
            obra.EstadoSupervision = nuevoEstado;

            return ModificarObra(obra);
        }

        public bool ModificarObra(BEClsObra obra)
        {
            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula.");
            }

            MPPClsObra mpp = new MPPClsObra();

            return mpp.ModificarObra(obra);
        }

        public bool EliminarObra(BEClsObra obra)
        {
            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula.");
            }

            if (obra.Codigo == null)
            {
                throw new Exception("La obra debe tener un código.");
            }

            MPPClsObra mpp = new MPPClsObra();

            return mpp.EliminarObra(obra);
        }
    }
}
