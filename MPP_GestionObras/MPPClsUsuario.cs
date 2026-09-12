using BE_GestionObras;
using DAL_GestionObras;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPP_GestionObras
{
    public class MPPClsUsuario
    {
        public bool ValidarUsuario(BEClsUsuario usuario)
        {
            DALAcceso acceso = new DALAcceso();

            string consulta =
                "SELECT * FROM Usuario " +
                "WHERE Usuario = '" + usuario.Usuario + "' " +
                "AND Password = '" + usuario.Password + "'";

            DataTable tabla = acceso.Leer(consulta);

            if (tabla.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
