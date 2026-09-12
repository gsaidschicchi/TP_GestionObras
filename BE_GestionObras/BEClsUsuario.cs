using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_GestionObras
{
    public class BEClsUsuario
    {
        // PROPIEDADES
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }

        // CONSTRUCTORES
        public BEClsUsuario()
        {
        }

        public BEClsUsuario(int idUsuario, string usuario, string password)
        {
            IdUsuario = idUsuario;
            Usuario = usuario;
            Password = password;
        }
    }


}
