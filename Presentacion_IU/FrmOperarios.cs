using BE_GestionObras;
using BLL_GestionObras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmOperarios : Form
    {
        private readonly BLLClsOperario bllOperario = new BLLClsOperario();
        private List<BEClsOperario> operarios = new List<BEClsOperario>();

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
            this.Size = new Size(780, 590);

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

            lstOperarios = new ListBox();
            lstOperarios.Location = new Point(380, 80);
            lstOperarios.Size = new Size(350, 380);
            lstOperarios.SelectedIndexChanged += lstOperarios_SelectedIndexChanged;

            this.Controls.Add(titulo);
            this.Controls.Add(btnCrear);
            this.Controls.Add(btnModificar);
            this.Controls.Add(btnEliminar);
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

        private BEClsOperario OperarioSeleccionado()
        {
            if (lstOperarios.SelectedIndex < 0)
            {
                throw new Exception("Debe seleccionar un operario.");
            }

            return operarios[lstOperarios.SelectedIndex];
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

                operario.IdCodigo = txtIdCodigo.Text;
                operario.SueldoBase = double.Parse(txtSueldoBase.Text);

                bool resultado = bllOperario.CrearOperario(operario);

                if (resultado)
                {
                    RefrescarLista();
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

        private void btnModificar_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsOperario operario = OperarioSeleccionado();

                operario.DNI = txtDNI.Text;
                operario.Nombre = txtNombre.Text;
                operario.Apellido = txtApellido.Text;
                operario.Telefono = txtTelefono.Text;
                operario.Legajo = int.Parse(txtLegajo.Text);
                operario.Especialidad = txtEspecialidad.Text;
                operario.SueldoBase = double.Parse(txtSueldoBase.Text);

                if (bllOperario.ModificarOperario(operario))
                {
                    RefrescarLista();
                    MessageBox.Show("Operario modificado correctamente.");
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
                BEClsOperario operario = OperarioSeleccionado();

                if (bllOperario.EliminarOperario(operario))
                {
                    RefrescarLista();
                    txtIdCodigo.Text = bllOperario.GenerarIdCodigo();
                    MessageBox.Show("Operario eliminado correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarLista()
        {
            if (lstOperarios == null)
            {
                return;
            }

            operarios = bllOperario.ListarTodo();
            lstOperarios.Items.Clear();

            foreach (BEClsOperario operario in operarios)
            {
                lstOperarios.Items.Add(
                    operario.IdCodigo + " - " +
                    operario.Legajo + " - " +
                    operario.Nombre + " " + operario.Apellido +
                    " | Sueldo: " + operario.CalcularSueldo().ToString("0.00"));
            }
        }

        private void lstOperarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstOperarios.SelectedIndex < 0)
            {
                return;
            }

            BEClsOperario operario = operarios[lstOperarios.SelectedIndex];

            txtIdCodigo.Text = operario.IdCodigo;
            txtDNI.Text = operario.DNI;
            txtNombre.Text = operario.Nombre;
            txtApellido.Text = operario.Apellido;
            txtTelefono.Text = operario.Telefono;
            txtLegajo.Text = operario.Legajo.ToString();
            txtEspecialidad.Text = operario.Especialidad;
            txtSueldoBase.Text = operario.SueldoBase.ToString();
        }
    }
}
