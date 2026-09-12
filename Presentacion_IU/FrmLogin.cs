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
            try
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
                    DialogResult resultadoPrincipal = principal.ShowDialog();

                    if (resultadoPrincipal == DialogResult.Cancel)
                    {
                        this.Close();
                        return;
                    }

                    txtPassword.Clear();
                    this.Show();
                    txtUsuario.Focus();
                }
                else
                {
                    MessageBox.Show("Usuario o contraseña incorrectos.");
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
