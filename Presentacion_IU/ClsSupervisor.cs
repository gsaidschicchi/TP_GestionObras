using System;

namespace Presentacion_IU
{
    public class ClsSupervisor : ClsPersona
    {
        // PROPIEDADES
        public int IdSupervisor { get; set; }
        public string Sector { get; set; }

        // CONSTRUCTORES
        public ClsSupervisor(string dni, string nombre, string apellido, string telefono,
                             int idSupervisor, string sector)
            : base(dni, nombre, apellido, telefono)
        {
            IdSupervisor = idSupervisor;
            Sector = sector;
        }

        // Constructor sin parámetros
        public ClsSupervisor()
            : base(string.Empty, string.Empty, string.Empty, string.Empty)
        {
            IdSupervisor = 0;
            Sector = string.Empty;
        }

        // METODOS
        public void SupervisarObra(ClsObra obra)
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

        public void AprobarObra(ClsObra obra)
        {
            SupervisarObra(obra);

            obra.CambiarEstadoSupervision(EstadoSupervision.APROBADO);
        }

        public void RechazarObra(ClsObra obra)
        {
            SupervisarObra(obra);

            obra.CambiarEstadoSupervision(EstadoSupervision.RECHAZADO);
        }
    }
}