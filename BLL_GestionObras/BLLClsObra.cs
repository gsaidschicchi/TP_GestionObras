using BE_GestionObras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_GestionObras
{
    public class BLLClsObra
    {
        public void CambiarEstadoObra(BEClsObra obra, EstadoObra nuevoEstado)
        {
            obra.Estado = nuevoEstado;
        }

        public void CambiarEstadoSupervision(BEClsObra obra, EstadoSupervision nuevoEstado)
        {
            obra.EstadoSupervision = nuevoEstado;
        }
    }
}
