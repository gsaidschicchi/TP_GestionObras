using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion_IU
{
    internal class ClsObra
    {
        // PROPIEDADES
        public int? Codigo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public EstadoObra Estado { get; set; }
        public EstadoSupervision EstadoSupervision { get; set; }

        // CONSTRUCTOR

        public ClsObra(int codigo, string nombre, string direccion)
        {
            Codigo = codigo;
            Nombre = nombre;
            Direccion = direccion;
            Estado = EstadoObra.PENDIENTE; // al crear la obra, se crea con estado de obra pendiente
            EstadoSupervision = EstadoSupervision.PENDIENTE; // al crear la obra, se crea con estado de supervision pendiente
        }

        // constructor sin parametros

        public ClsObra()
        {
            Codigo = null;
            Nombre = string.Empty;
            Direccion = string.Empty;
            Estado = EstadoObra.PENDIENTE; // al crear la obra, se crea con estado de obra pendiente
            EstadoSupervision = EstadoSupervision.PENDIENTE; // al crear la obra, se crea con estado de supervision pendiente
        }

        // METODOS
    }
}
