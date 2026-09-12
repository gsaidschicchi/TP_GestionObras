using BE_GestionObras;
using MPP_GestionObras;
using Security_GestionObras;
using System;

namespace BLL_GestionObras
{
    public class BLLClsUsuario
    {
        public bool ValidarUsuario(BEClsUsuario usuario)
        {
            if (usuario == null)
            {
                throw new Exception("El usuario no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Usuario))
            {
                throw new Exception("Debe ingresar un usuario.");
            }

            if (string.IsNullOrWhiteSpace(usuario.Password))
            {
                throw new Exception("Debe ingresar una contraseña.");
            }

            usuario.Password = ClsEncriptar.EncriptarSHA(usuario.Password);

            MPPClsUsuario mpp = new MPPClsUsuario();

            return mpp.ValidarUsuario(usuario);
        }
    }
}