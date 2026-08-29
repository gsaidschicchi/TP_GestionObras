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
        public override string MostrarDatos()
        {
            return $"DNI: {DNI}, Nombre: {Nombre}, Apellido: {Apellido}, " + 
            $"Telefono: {Telefono}, Legajo: {Legajo}, Especialidad: {Especialidad}";
        }

        // CONSTRUCTOR
        public ClsOperario(string dni, string nombre, string apellido, string telefono,
                           int legajo, string especialidad) 
            : base(dni, nombre, apellido, telefono)
        {
            Legajo = legajo;
            Especialidad = especialidad;
        }
    }
}
