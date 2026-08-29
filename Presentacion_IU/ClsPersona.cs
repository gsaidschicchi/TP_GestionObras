using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion_IU
{
    public abstract class ClsPersona
    {
        // PROPIEDADES
        public string DNI { get; set;  }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Telefono { get; set; }

        // METODOS
        public abstract string MostrarDatos();

        // CONSTRUCTOR
        public ClsPersona(string dni, string nombre, string apellido, string telefono)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
        }
    }
}
