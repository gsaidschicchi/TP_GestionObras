using BE_GestionObras;
using BLL_GestionObras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmObras : Form
    {
        private readonly BLLClsObra bllObra = new BLLClsObra();
        private List<BEClsObra> obras = new List<BEClsObra>();

        private TextBox txtCodigo;
        private TextBox txtNombre;
        private TextBox txtDireccion;
        private ListBox lstObras;
        private DataGridView dgvObras;
        private Label lblEstado;

        public FrmObras()
        {
            this.Text = "Gestión de Obras";
            this.Size = new Size(920, 560);

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
            btnCrear.Location = new Point(30, 230);
            btnCrear.Size = new Size(130, 40);
            btnCrear.Click += btnCrear_Click;

            Button btnModificar = new Button();
            btnModificar.Text = "Modificar Obra";
            btnModificar.Location = new Point(170, 230);
            btnModificar.Size = new Size(130, 40);
            btnModificar.Click += btnModificar_Click;

            Button btnEliminar = new Button();
            btnEliminar.Text = "Eliminar Obra";
            btnEliminar.Location = new Point(310, 230);
            btnEliminar.Size = new Size(130, 40);
            btnEliminar.Click += btnEliminar_Click;

            lstObras = new ListBox();
            lstObras.Location = new Point(390, 90);
            lstObras.Size = new Size(260, 220);
            lstObras.SelectedIndexChanged += lstObras_SelectedIndexChanged;

            dgvObras = new DataGridView();
            dgvObras.Location = new Point(30, 375);
            dgvObras.Size = new Size(830, 120);
            dgvObras.ReadOnly = true;
            dgvObras.AutoGenerateColumns = true;
            dgvObras.AllowUserToAddRows = false;

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
            this.Controls.Add(btnModificar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(lstObras);
            this.Controls.Add(dgvObras);
            this.Controls.Add(lblEstado);

            this.Activated += (s, e) => RefrescarLista();
            RefrescarLista();
        }

        private BEClsObra ObraSeleccionada()
        {
            if (lstObras.SelectedIndex < 0)
            {
                throw new Exception("Debe seleccionar una obra.");
            }

            return obras[lstObras.SelectedIndex];
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsObra obra = new BEClsObra(
                    int.Parse(txtCodigo.Text),
                    txtNombre.Text,
                    txtDireccion.Text);

                bool resultado = bllObra.CrearObra(obra);

                if (resultado)
                {
                    RefrescarLista();
                    MessageBox.Show("Obra creada correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsObra obra = ObraSeleccionada();
                obra.Nombre = txtNombre.Text;
                obra.Direccion = txtDireccion.Text;

                bool resultado = bllObra.ModificarObra(obra);

                if (resultado)
                {
                    RefrescarLista();
                    MessageBox.Show("Obra modificada correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsObra obra = ObraSeleccionada();

                bool resultado = bllObra.EliminarObra(obra);

                if (resultado)
                {
                    RefrescarLista();
                    MessageBox.Show("Obra eliminada correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarLista()
        {
            if (lstObras == null)
            {
                return;
            }

            obras = bllObra.ListarTodo();

            lstObras.Items.Clear();

            foreach (BEClsObra obra in obras)
            {
                lstObras.Items.Add(obra.Codigo + " - " + obra.Nombre);
            }

            dgvObras.DataSource = null;
            dgvObras.DataSource = obras;
        }

        private void lstObras_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstObras.SelectedIndex < 0)
            {
                return;
            }

            BEClsObra obra = obras[lstObras.SelectedIndex];

            txtCodigo.Text = obra.Codigo.ToString();
            txtNombre.Text = obra.Nombre;
            txtDireccion.Text = obra.Direccion;

            lblEstado.Text = "Estado Obra: " + obra.Estado +
                             " | Informada Supervisor: " + (obra.InformadaAlSupervisor ? "SI" : "NO") +
                             " | Supervisión: " + obra.EstadoSupervision;
        }
    }
}
