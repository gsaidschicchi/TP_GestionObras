using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionObras
{
    public class BEClsContratista
    {
        // PROPIEDADES
        public string CUIT { get; set; }
        public string RazonSocial { get; set; }
        public List<BEClsCuadrilla> Cuadrillas { get; set; }

        // CONSTRUCTOR

        public BEClsContratista(string cuit, string razonSocial)
        {
            CUIT = cuit;
            RazonSocial = razonSocial;
            Cuadrillas = new List<BEClsCuadrilla>();
        }

        // constructor vacio
        public BEClsContratista()
        {
            CUIT = string.Empty;
            RazonSocial = string.Empty;
            Cuadrillas = new List<BEClsCuadrilla>();
        }

    }
}
