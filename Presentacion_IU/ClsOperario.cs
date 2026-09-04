using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion_IU
{
    public class ClsOperario : ClsPersona
    {
        // PROPIEDADES
        public int Legajo { get; set; }
        public string Especialidad { get; set; }

        // METODOS


        // CONSTRUCTOR
        public ClsOperario(string dni, string nombre, string apellido, string telefono,
                           int legajo, string especialidad) 
            : base(dni, nombre, apellido, telefono)
        {
            Legajo = legajo;
            Especialidad = especialidad;
        }

        // constructor sin parametros
        public ClsOperario() : base(string.Empty, string.Empty, string.Empty, string.Empty)
        {
            Legajo = 0;
            Especialidad = string.Empty;
        }
    }
}
