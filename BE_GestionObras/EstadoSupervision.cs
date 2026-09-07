using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionObras
{
    /*
    Trabajo con enum -> los estados son cerrados y simples. 
    No necesitamos que “Estado” tenga comportamiento propio; 
    solamente queremos impedir valores inválidos. 
    */
    public enum EstadoSupervision
    {
        PENDIENTE,
        RECHAZADO,
        APROBADO
    }
}

