using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionObras
{
    public class BEClsCuadrilla
    {
        // PROPIEDADES
        public int? Codigo { get; set; } // ? -> permite nullear un valor
        public string Nombre { get; set; }
        public BEClsObra ObraAsignada { get; set; }
        public List<BEClsOperario> Operarios { get; set; }

        // CONSTRUCTOR
        public BEClsCuadrilla(int codigo, string nombre, BEClsObra obra)
        {
            Codigo = codigo;
            Nombre = nombre;
            ObraAsignada = obra;
            Operarios = new List<BEClsOperario>();
        }

        // constructor sin parametros
        public BEClsCuadrilla()
        {
            Codigo = null;
            Nombre = string.Empty;
            ObraAsignada = null;
            Operarios = new List<BEClsOperario>();
        }
    }
}
