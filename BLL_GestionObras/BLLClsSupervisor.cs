using BE_GestionObras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL_GestionObras
{
    public class BLLClsSupervisor
    {
        // METODOS
        public void SupervisarObra(BEClsObra obra)
        {
            if (obra == null)
            {
                throw new Exception("La obra no puede ser nula.");
            }

            if (obra.Estado != EstadoObra.FINALIZADA)
            {
                throw new Exception("La obra debe finalizarse por la Contratista antes de ser supervisada.");
            }

            if (!obra.InformadaAlSupervisor)
            {
                throw new Exception("La obra todavía no fue informada por la Contratista al Supervisor.");
            }

            if (obra.EstadoSupervision != EstadoSupervision.PENDIENTE)
            {
                throw new Exception("La obra no se encuentra pendiente de supervisión.");
            }
        }

        public void AprobarObra(BEClsObra obra)
        {
            SupervisarObra(obra);

            BLLClsObra bllObra = new BLLClsObra();

            bllObra.CambiarEstadoSupervision(obra, EstadoSupervision.APROBADO);
        }

        public void RechazarObra(BEClsObra obra)
        {
            SupervisarObra(obra);

            BLLClsObra bllObra = new BLLClsObra();

            bllObra.CambiarEstadoSupervision(obra, EstadoSupervision.RECHAZADO);
        }
    }
}
