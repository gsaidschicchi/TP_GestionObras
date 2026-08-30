using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        // constructor sin parámetros que inicializa la base con valores vacíos
        public ClsSupervisor()
            : base(string.Empty, string.Empty, string.Empty, string.Empty)
        {
            IdSupervisor = 0;
            Sector = string.Empty;
        }

        // METODOS
        public override string MostrarDatos()
        {
            return $"DNI: {DNI}, Nombre: {Nombre}, Apellido: {Apellido}," +
                   $"Telefono: {Telefono}, IdSupervisor: {IdSupervisor}, Sector: {Sector}.";
        }
    }
}
