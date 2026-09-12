using BE_GestionObras;
using BLL_GestionObras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmSupervisores : Form
    {
        private readonly BLLClsSupervisor bllSupervisor = new BLLClsSupervisor();
        private List<BEClsSupervisor> supervisores = new List<BEClsSupervisor>();

        private TextBox txtIdCodigo;
        private TextBox txtDNI;
        private TextBox txtNombre;
        private TextBox txtApellido;
        private TextBox txtTelefono;
        private TextBox txtSueldoBase;
        private TextBox txtIdSupervisor;
        private TextBox txtSector;
        private ListBox lstSupervisores;

        public FrmSupervisores()
        {
            this.Text = "Gestión de Supervisores";
            this.Size = new Size(800, 590);

            Label titulo = new Label();
            titulo.Text = "Gestión de Supervisores";
            titulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);

            CrearCampo("Id Código:", 80, out txtIdCodigo);
            txtIdCodigo.ReadOnly = true;
            txtIdCodigo.Text = bllSupervisor.GenerarIdCodigo();

            CrearCampo("DNI:", 120, out txtDNI);
            CrearCampo("Nombre:", 160, out txtNombre);
            CrearCampo("Apellido:", 200, out txtApellido);
            CrearCampo("Teléfono:", 240, out txtTelefono);
            CrearCampo("Sueldo Base:", 280, out txtSueldoBase);
            CrearCampo("Id Supervisor:", 320, out txtIdSupervisor);
            CrearCampo("Sector:", 360, out txtSector);

            Button btnCrear = new Button();
            btnCrear.Text = "Crear";
            btnCrear.Location = new Point(30, 420);
            btnCrear.Size = new Size(100, 40);
            btnCrear.Click += btnCrear_Click;

            Button btnModificar = new Button();
            btnModificar.Text = "Modificar";
            btnModificar.Location = new Point(140, 420);
            btnModificar.Size = new Size(100, 40);
            btnModificar.Click += btnModificar_Click;

            Button btnEliminar = new Button();
            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new Point(250, 420);
            btnEliminar.Size = new Size(100, 40);
            btnEliminar.Click += btnEliminar_Click;

            lstSupervisores = new ListBox();
            lstSupervisores.Location = new Point(400, 80);
            lstSupervisores.Size = new Size(350, 380);
            lstSupervisores.SelectedIndexChanged += lstSupervisores_SelectedIndexChanged;

            this.Controls.Add(titulo);
            this.Controls.Add(btnCrear);
            this.Controls.Add(btnModificar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(lstSupervisores);

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

        private BEClsSupervisor SupervisorSeleccionado()
        {
            if (lstSupervisores.SelectedIndex < 0)
            {
                throw new Exception("Debe seleccionar un supervisor.");
            }

            return supervisores[lstSupervisores.SelectedIndex];
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsSupervisor supervisor = new BEClsSupervisor();
                supervisor.IdCodigo = txtIdCodigo.Text;
                supervisor.DNI = txtDNI.Text;
                supervisor.Nombre = txtNombre.Text;
                supervisor.Apellido = txtApellido.Text;
                supervisor.Telefono = txtTelefono.Text;
                supervisor.SueldoBase = double.Parse(txtSueldoBase.Text);
                supervisor.IdSupervisor = int.Parse(txtIdSupervisor.Text);
                supervisor.Sector = txtSector.Text;

                if (bllSupervisor.CrearSupervisor(supervisor))
                {
                    RefrescarLista();
                    txtIdCodigo.Text = bllSupervisor.GenerarIdCodigo();
                    MessageBox.Show("Supervisor creado correctamente. Sueldo calculado: " +
                                    bllSupervisor.CalcularSueldo(supervisor).ToString("0.00"));
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
                BEClsSupervisor supervisor = SupervisorSeleccionado();
                supervisor.DNI = txtDNI.Text;
                supervisor.Nombre = txtNombre.Text;
                supervisor.Apellido = txtApellido.Text;
                supervisor.Telefono = txtTelefono.Text;
                supervisor.SueldoBase = double.Parse(txtSueldoBase.Text);
                supervisor.IdSupervisor = int.Parse(txtIdSupervisor.Text);
                supervisor.Sector = txtSector.Text;

                if (bllSupervisor.ModificarSupervisor(supervisor))
                {
                    RefrescarLista();
                    MessageBox.Show("Supervisor modificado correctamente.");
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
                BEClsSupervisor supervisor = SupervisorSeleccionado();

                if (bllSupervisor.EliminarSupervisor(supervisor))
                {
                    RefrescarLista();
                    txtIdCodigo.Text = bllSupervisor.GenerarIdCodigo();
                    MessageBox.Show("Supervisor eliminado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarLista()
        {
            supervisores = bllSupervisor.ListarTodo();
            lstSupervisores.Items.Clear();

            foreach (BEClsSupervisor supervisor in supervisores)
            {
                lstSupervisores.Items.Add(
                    supervisor.IdCodigo + " - " +
                    supervisor.Nombre + " " + supervisor.Apellido +
                    " | " + supervisor.Sector +
                    " | Sueldo: " + bllSupervisor.CalcularSueldo(supervisor).ToString("0.00"));
            }
        }

        private void lstSupervisores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSupervisores.SelectedIndex < 0)
            {
                return;
            }

            BEClsSupervisor supervisor = supervisores[lstSupervisores.SelectedIndex];
            txtIdCodigo.Text = supervisor.IdCodigo;
            txtDNI.Text = supervisor.DNI;
            txtNombre.Text = supervisor.Nombre;
            txtApellido.Text = supervisor.Apellido;
            txtTelefono.Text = supervisor.Telefono;
            txtSueldoBase.Text = supervisor.SueldoBase.ToString();
            txtIdSupervisor.Text = supervisor.IdSupervisor.ToString();
            txtSector.Text = supervisor.Sector;
        }
    }
}
