using BE_GestionObras;
using MPP_GestionObras;
using System;
using System.Collections.Generic;

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

        public List<BEClsOperario> ListarTodo()
        {
            MPPClsOperario mpp = new MPPClsOperario();
            return mpp.ListarTodo();
        }

        public string GenerarIdCodigo()
        {
            MPPClsOperario mpp = new MPPClsOperario();
            List<BEClsOperario> operarios = mpp.ListarTodo();

            int mayor = 0;

            foreach (BEClsOperario operario in operarios)
            {
                int codigo = 0;

                if (int.TryParse(operario.IdCodigo, out codigo))
                {
                    if (codigo > mayor)
                    {
                        mayor = codigo;
                    }
                }
            }

            int siguiente = mayor + 1;
            return siguiente.ToString("D6");
        }
    }
}
