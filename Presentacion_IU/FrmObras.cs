using BE_GestionObras;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmObras : Form
    {
        private TextBox txtCodigo;
        private TextBox txtNombre;
        private TextBox txtDireccion;
        private ListBox lstObras;
        private Label lblEstado;

        public FrmObras()
        {
            this.Text = "Gestión de Obras";
            this.Size = new Size(700, 450);

            Label titulo = new Label();
            titulo.Text = "Gestión de Obras";
            titulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);

            Label lblCodigo = new Label();
            lblCodigo.Text = "Código:";
            lblCodigo.Location = new Point(30, 100);

            txtCodigo = new TextBox();
            txtCodigo.Location = new Point(150, 97);
            txtCodigo.Width = 200;

            Label lblNombre = new Label();
            lblNombre.Text = "Nombre:";
            lblNombre.Location = new Point(30, 140);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(150, 137);
            txtNombre.Width = 200;

            Label lblDireccion = new Label();
            lblDireccion.Text = "Dirección:";
            lblDireccion.Location = new Point(30, 180);

            txtDireccion = new TextBox();
            txtDireccion.Location = new Point(150, 177);
            txtDireccion.Width = 200;

            Button btnCrear = new Button();
            btnCrear.Text = "Crear Obra";
            btnCrear.Location = new Point(150, 230);
            btnCrear.Size = new Size(150, 40);
            btnCrear.Click += btnCrear_Click;

            lstObras = new ListBox();
            lstObras.Location = new Point(390, 90);
            lstObras.Size = new Size(260, 220);
            lstObras.SelectedIndexChanged += lstObras_SelectedIndexChanged;

            lblEstado = new Label();
            lblEstado.Text = "Seleccione una obra para ver su estado.";
            lblEstado.Location = new Point(30, 320);
            lblEstado.AutoSize = true;

            this.Controls.Add(titulo);
            this.Controls.Add(lblCodigo);
            this.Controls.Add(txtCodigo);
            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblDireccion);
            this.Controls.Add(txtDireccion);
            this.Controls.Add(btnCrear);
            this.Controls.Add(lstObras);
            this.Controls.Add(lblEstado);

            RefrescarLista();
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsObra obra = new BEClsObra(
                    int.Parse(txtCodigo.Text),
                    txtNombre.Text,
                    txtDireccion.Text);

                ContextoAplicacion.Obras.Add(obra);
                RefrescarLista();
                MessageBox.Show("Obra creada correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarLista()
        {
            if (lstObras == null) return;

            lstObras.Items.Clear();
            foreach (BEClsObra obra in ContextoAplicacion.Obras)
            {
                lstObras.Items.Add(obra.Codigo + " - " + obra.Nombre);
            }
        }

        private void lstObras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstObras.SelectedIndex < 0) return;

            BEClsObra obra = ContextoAplicacion.Obras[lstObras.SelectedIndex];
            lblEstado.Text = "Estado Obra: " + obra.Estado +
                             " | Informada Supervisor: " + (obra.InformadaAlSupervisor ? "SI" : "NO") +
                             " | Supervisión: " + obra.EstadoSupervision;
        }
    }
}
