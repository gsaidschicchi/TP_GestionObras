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

        // METODOS
        public override string MostrarDatos()
        {
            return $"DNI: {DNI}, Nombre: {Nombre}, Apellido: {Apellido}," +
            $"Telefono: {Telefono}, IdSupervisor: {IdSupervisor}, Sector: {Sector}.";
        }


    }
}
