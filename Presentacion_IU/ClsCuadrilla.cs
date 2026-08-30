using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion_IU
{
    internal class ClsCuadrilla
    {
        // PROPIEDADES
        public int? Codigo { get; set; } // ? -> permite nullear un valor
        public string Nombre { get; set; }
        public List<ClsOperario> Operarios { get; set; }

        // CONSTRUCTOR
        public ClsCuadrilla(int codigo, string nombre) 
        {
            Codigo = codigo;
            Nombre = nombre;
            Operarios = new List<ClsOperario>();
        }

        // constructor sin parametros
        public ClsCuadrilla()
        {
            Codigo = null;
            Nombre = string.Empty;
            Operarios = new List<ClsOperario>();
        }

        // METODOS
    }
}
