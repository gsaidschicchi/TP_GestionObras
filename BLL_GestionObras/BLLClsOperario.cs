using BE_GestionObras;
using MPP_GestionObras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_GestionObras
{
    public class BLLClsOperario
    {
        public bool CrearOperario(BEClsOperario operario)
        {
            if (operario == null)
            {
                throw new Exception("El operario no puede ser vacio.");
            }

            MPPClsOperario mpp = new MPPClsOperario();

            if (mpp.BuscarOperario(operario))
            {
                throw new Exception("El operario ya se encuentra dado de alta.");
            }

            return mpp.CrearOperario(operario);
        }
    }
}
