using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion_IU
{
    internal class ClsContratista
    {
        // PROPIEDADES
        public string CUIT { get; set; }
        public string RazonSocial { get; set; }
        public List<ClsCuadrilla> Cuadrillas { get; set; } 

        // CONSTRUCTOR

        public ClsContratista(string cuit, string razonSocial)
        {
            CUIT = cuit;
            RazonSocial = razonSocial;
            Cuadrillas = new List<ClsCuadrilla>();
        }

        // constructor vacio
        public ClsContratista()
        {
            CUIT = string.Empty;
            RazonSocial = string.Empty;
            Cuadrillas = new List<ClsCuadrilla>();
        }

        // METODOS
    }
}
