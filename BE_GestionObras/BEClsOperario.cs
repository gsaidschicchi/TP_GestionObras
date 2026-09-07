using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionObras
{
    public class BEClsOperario : BEClsPersona
    {
        // PROPIEDADES
        public int Legajo { get; set; }
        public string Especialidad { get; set; }

        // METODOS


        // CONSTRUCTOR
        public BEClsOperario(string dni, string nombre, string apellido, string telefono,
                           int legajo, string especialidad)
            : base(dni, nombre, apellido, telefono)
        {
            Legajo = legajo;
            Especialidad = especialidad;
        }

        // constructor sin parametros
        public BEClsOperario() : base(string.Empty, string.Empty, string.Empty, string.Empty)
        {
            Legajo = 0;
            Especialidad = string.Empty;
        }
    }
}
