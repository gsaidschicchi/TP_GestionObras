using BE_GestionObras;
using BLL_GestionObras;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmOperarios : Form
    {
        private readonly BLLClsOperario bllOperario = new BLLClsOperario();

        private TextBox txtIdCodigo;
        private TextBox txtDNI;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtTelefono;
        private TextBox txtLegajo;
        private TextBox txtEspecialidad;
        private TextBox txtSueldoBase;
        private ListBox lstOperarios;

        public FrmOperarios()
        {
            this.Text = "Gestión de Operarios";
            this.Size = new Size(760, 570);

            Label titulo = new Label();
            titulo.Text = "Gestión de Operarios";
            titulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);

            CrearCampo("Id Código:", 80, out txtIdCodigo);
            txtIdCodigo.ReadOnly = true;
            txtIdCodigo.Text = bllOperario.GenerarIdCodigo();

            CrearCampo("DNI:", 120, out txtDNI);
            CrearCampo("Nombre:", 160, out txtNombre);
            CrearCampo("Apellido:", 200, out txtApellido);
            CrearCampo("Teléfono:", 240, out txtTelefono);
            CrearCampo("Legajo:", 280, out txtLegajo);
            CrearCampo("Especialidad:", 320, out txtEspecialidad);
            CrearCampo("Sueldo Base:", 360, out txtSueldoBase);

            Button btnCrear = new Button();
            btnCrear.Text = "Crear Operario";
            btnCrear.Location = new Point(150, 410);
            btnCrear.Size = new Size(160, 40);
            btnCrear.Click += btnCrear_Click;

            lstOperarios = new ListBox();
            lstOperarios.Location = new Point(380, 80);
            lstOperarios.Size = new Size(330, 370);

            this.Controls.Add(titulo);
            this.Controls.Add(btnCrear);
            this.Controls.Add(lstOperarios);

            RefrescarLista();
        }

        private void CrearCampo(string texto, int y, out TextBox textBox)
        {
            Label label = new Label();
            label.Text = texto;
            label.Location = new Point(30, y);

            textBox = new TextBox();
            textBox.Location = new Point(150, y - 3);
            textBox.Width = 200;

            this.Controls.Add(label);
            this.Controls.Add(textBox);
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsOperario operario = new BEClsOperario(
                    txtDNI.Text,
                    txtNombre.Text,
                    txtApellido.Text,
                    txtTelefono.Text,
                    int.Parse(txtLegajo.Text),
                    txtEspecialidad.Text);

                // Nuevos datos definidos en la corrección.
                operario.IdCodigo = txtIdCodigo.Text;
                operario.SueldoBase = double.Parse(txtSueldoBase.Text);

                bool resultado = bllOperario.CrearOperario(operario);

                if (resultado)
                {
                    RefrescarLista();

                    // Prepara el próximo código automático desde los datos persistidos.
                    txtIdCodigo.Text = bllOperario.GenerarIdCodigo();

                    MessageBox.Show("Operario creado correctamente. Sueldo calculado: " +
                                    operario.CalcularSueldo().ToString("0.00"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarLista()
        {
            if (lstOperarios == null) return;

            lstOperarios.Items.Clear();
            foreach (BEClsOperario operario in bllOperario.ListarTodo())
            {
                lstOperarios.Items.Add(
                    operario.IdCodigo + " - " +
                    operario.Legajo + " - " +
                    operario.Nombre + " " + operario.Apellido +
                    " | Sueldo: " + operario.CalcularSueldo().ToString("0.00"));
            }
        }
    }
}
