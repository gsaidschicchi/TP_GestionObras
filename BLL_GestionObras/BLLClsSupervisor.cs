using BE_GestionObras;
using MPP_GestionObras;
using System;
using System.Collections.Generic;

namespace BLL_GestionObras
{
    public class BLLClsSupervisor : BLLClsPersona
    {
        public override double CalcularSueldo(BEClsPersona persona)
        {
            return persona.CalcularSueldo();
        }

        public bool CrearSupervisor(BEClsSupervisor supervisor)
        {
            if (supervisor == null)
            {
                throw new Exception("El supervisor no puede ser nulo.");
            }

            MPPClsSupervisor mpp = new MPPClsSupervisor();

            if (mpp.BuscarSupervisor(supervisor))
            {
                throw new Exception("El supervisor ya se encuentra dado de alta.");
            }

            return mpp.CrearSupervisor(supervisor);
        }

        public List<BEClsSupervisor> ListarTodo()
        {
            MPPClsSupervisor mpp = new MPPClsSupervisor();
            return mpp.ListarTodo();
        }

        public bool ModificarSupervisor(BEClsSupervisor supervisor)
        {
            if (supervisor == null)
            {
                throw new Exception("El supervisor no puede ser nulo.");
            }

            MPPClsSupervisor mpp = new MPPClsSupervisor();
            return mpp.ModificarSupervisor(supervisor);
        }

        public bool EliminarSupervisor(BEClsSupervisor supervisor)
        {
            if (supervisor == null)
            {
                throw new Exception("El supervisor no puede ser nulo.");
            }

            MPPClsSupervisor mpp = new MPPClsSupervisor();
            return mpp.EliminarSupervisor(supervisor);
        }

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

        public string GenerarIdCodigo()
        {
            MPPClsSupervisor mpp = new MPPClsSupervisor();
            List<BEClsSupervisor> supervisores = mpp.ListarTodo();

            int mayor = 0;

            foreach (BEClsSupervisor supervisor in supervisores)
            {
                int codigo = 0;

                if (int.TryParse(supervisor.IdCodigo, out codigo))
                {
                    if (codigo > mayor)
                    {
                        mayor = codigo;
                    }
                }
            }

            int siguiente = mayor + 1;
            return siguiente.ToString("D6");
        }
    }
}
