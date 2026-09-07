using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionObras
{
    public abstract class BEClsPersona
    {
        // PROPIEDADES
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }

        // CONSTRUCTOR
        public BEClsPersona(string dni, string nombre, string apellido, string telefono)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
        }

        public BEClsPersona()
        {
            DNI = string.Empty;
            Nombre = string.Empty;
            Apellido = string.Empty;
            Telefono = string.Empty;
        }
    }
}
