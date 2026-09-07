using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionObras
{
    public class BEClsSupervisor : BEClsPersona
    {
        // PROPIEDADES
        public int IdSupervisor { get; set; }
        public string Sector { get; set; }

        // CONSTRUCTORES
        public BEClsSupervisor(string dni, string nombre, string apellido, string telefono,
                             int idSupervisor, string sector)
            : base(dni, nombre, apellido, telefono)
        {
            IdSupervisor = idSupervisor;
            Sector = sector;
        }

        // Constructor sin parámetros
        public BEClsSupervisor()
            : base(string.Empty, string.Empty, string.Empty, string.Empty)
        {
            IdSupervisor = 0;
            Sector = string.Empty;
        }
    }
}
