using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionObras
{
    public class BEClsObra
    {
        // PROPIEDADES
        public int? Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public EstadoObra Estado { get; set; }
        public EstadoSupervision EstadoSupervision { get; set; }
        public bool InformadaAlSupervisor { get; set; }

        // CONSTRUCTOR
        public BEClsObra(int codigo, string nombre, string direccion)
        {
            Codigo = codigo;
            Nombre = nombre;
            Direccion = direccion;

            Estado = EstadoObra.PENDIENTE;
            EstadoSupervision = EstadoSupervision.PENDIENTE;
            InformadaAlSupervisor = false;
        }

        // CONSTRUCTOR SIN PARAMETROS
        public BEClsObra()
        {
            Codigo = null;
            Nombre = string.Empty;
            Direccion = string.Empty;

            Estado = EstadoObra.PENDIENTE;
            EstadoSupervision = EstadoSupervision.PENDIENTE;
            InformadaAlSupervisor = false;
        }
    }
}
