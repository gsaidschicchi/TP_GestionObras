using BE_GestionObras;
using System.Collections.Generic;

namespace Presentacion_IU
{
    // Almacenamiento temporal en memoria para integrar los formularios.
    // Más adelante esta responsabilidad será reemplazada por MPP/DAL/BD.
    public static class ContextoAplicacion
    {
        public static List<BEClsContratista> Contratistas { get; } = new List<BEClsContratista>();
        public static List<BEClsCuadrilla> Cuadrillas { get; } = new List<BEClsCuadrilla>();
        public static List<BEClsOperario> Operarios { get; } = new List<BEClsOperario>();
        public static List<BEClsObra> Obras { get; } = new List<BEClsObra>();
        public static List<BEClsPersona> Personas { get; } = new List<BEClsPersona>();

        // Generación temporal del identificador de Persona.
        // Cuando exista BD, el próximo código deberá resolverse desde persistencia.
        public static string GenerarIdPersona()
        {
            int siguiente = Personas.Count + 1;
            return siguiente.ToString("D6");
        }
    }
}
