using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BE_GestionObras;
using BLL_GestionObras;
using Security_GestionObras;

namespace Presentacion_IU
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            BEClsUsuario usuario = new BEClsUsuario();

            usuario.Usuario = txtUsuario.Text;
            usuario.Password = txtPassword.Text;

            BLLClsUsuario bll = new BLLClsUsuario();
            bool resultado = bll.ValidarUsuario(usuario);

            if (resultado)
            {
                this.Hide();

                FrmPrincipal principal = new FrmPrincipal();
                principal.Show();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.");
            }
        }
    }
}
