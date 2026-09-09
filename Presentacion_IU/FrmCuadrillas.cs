using BE_GestionObras;
using BLL_GestionObras;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmCuadrillas : Form
    {
        private readonly BLLClsCuadrilla bllCuadrilla = new BLLClsCuadrilla();

        private TextBox txtCodigo;
        private TextBox txtNombre;
        private ComboBox cboCuadrillas;
        private ComboBox cboOperarios;
        private ComboBox cboObras;
        private ListBox lstEventos;
        private Label lblEstado;

        public FrmCuadrillas()
        {
            this.Text = "Gestión de Cuadrillas";
            this.Size = new Size(850, 600);

            Label titulo = new Label();
            titulo.Text = "Gestión de Cuadrillas";
            titulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);

            Label lblCodigo = new Label();
            lblCodigo.Text = "Código:";
            lblCodigo.Location = new Point(30, 85);

            txtCodigo = new TextBox();
            txtCodigo.Location = new Point(140, 82);
            txtCodigo.Width = 200;

            Label lblNombre = new Label();
            lblNombre.Text = "Nombre:";
            lblNombre.Location = new Point(30, 125);

            txtNombre = new TextBox();
            txtNombre.Location = new Point(140, 122);
            txtNombre.Width = 200;

            Button btnCrear = new Button();
            btnCrear.Text = "Crear Cuadrilla";
            btnCrear.Location = new Point(370, 82);
            btnCrear.Size = new Size(150, 60);
            btnCrear.Click += btnCrear_Click;

            Label lblSeleccion = new Label();
            lblSeleccion.Text = "Cuadrilla:";
            lblSeleccion.Location = new Point(30, 195);

            cboCuadrillas = new ComboBox();
            cboCuadrillas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCuadrillas.Location = new Point(140, 192);
            cboCuadrillas.Width = 250;
            cboCuadrillas.SelectedIndexChanged += (s, e) => ActualizarEstado();

            Label lblOperario = new Label();
            lblOperario.Text = "Operario:";
            lblOperario.Location = new Point(30, 235);

            cboOperarios = new ComboBox();
            cboOperarios.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOperarios.Location = new Point(140, 232);
            cboOperarios.Width = 250;

            Button btnAgregarOperario = new Button();
            btnAgregarOperario.Text = "Agregar Operario";
            btnAgregarOperario.Location = new Point(420, 230);
            btnAgregarOperario.Size = new Size(150, 30);
            btnAgregarOperario.Click += btnAgregarOperario_Click;

            Button btnQuitarOperario = new Button();
            btnQuitarOperario.Text = "Quitar Operario";
            btnQuitarOperario.Location = new Point(590, 230);
            btnQuitarOperario.Size = new Size(150, 30);
            btnQuitarOperario.Click += btnQuitarOperario_Click;

            Label lblObra = new Label();
            lblObra.Text = "Obra:";
            lblObra.Location = new Point(30, 275);

            cboObras = new ComboBox();
            cboObras.DropDownStyle = ComboBoxStyle.DropDownList;
            cboObras.Location = new Point(140, 272);
            cboObras.Width = 250;

            Button btnAsignar = new Button();
            btnAsignar.Text = "Asignar Obra";
            btnAsignar.Location = new Point(420, 270);
            btnAsignar.Size = new Size(150, 30);
            btnAsignar.Click += btnAsignar_Click;

            Button btnDesasignar = new Button();
            btnDesasignar.Text = "Desasignar Obra";
            btnDesasignar.Location = new Point(590, 270);
            btnDesasignar.Size = new Size(150, 30);
            btnDesasignar.Click += btnDesasignar_Click;

            Button btnIniciar = new Button();
            btnIniciar.Text = "Iniciar Obra";
            btnIniciar.Location = new Point(140, 325);
            btnIniciar.Size = new Size(150, 40);
            btnIniciar.Click += btnIniciar_Click;

            Button btnFinalizar = new Button();
            btnFinalizar.Text = "Finalizar Obra";
            btnFinalizar.Location = new Point(310, 325);
            btnFinalizar.Size = new Size(150, 40);
            btnFinalizar.Click += btnFinalizar_Click;

            lblEstado = new Label();
            lblEstado.Location = new Point(30, 390);
            lblEstado.AutoSize = true;
            lblEstado.Text = "Seleccione una cuadrilla.";

            lstEventos = new ListBox();
            lstEventos.Location = new Point(30, 430);
            lstEventos.Size = new Size(770, 100);

            this.Controls.Add(titulo);
            this.Controls.Add(lblCodigo);
            this.Controls.Add(txtCodigo);
            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(btnCrear);
            this.Controls.Add(lblSeleccion);
            this.Controls.Add(cboCuadrillas);
            this.Controls.Add(lblOperario);
            this.Controls.Add(cboOperarios);
            this.Controls.Add(btnAgregarOperario);
            this.Controls.Add(btnQuitarOperario);
            this.Controls.Add(lblObra);
            this.Controls.Add(cboObras);
            this.Controls.Add(btnAsignar);
            this.Controls.Add(btnDesasignar);
            this.Controls.Add(btnIniciar);
            this.Controls.Add(btnFinalizar);
            this.Controls.Add(lblEstado);
            this.Controls.Add(lstEventos);

            this.Activated += (s, e) => RefrescarCombos();
            RefrescarCombos();
        }

        private BEClsCuadrilla CuadrillaSeleccionada()
        {
            if (cboCuadrillas.SelectedIndex < 0)
                throw new Exception("Debe seleccionar una cuadrilla.");

            return ContextoAplicacion.Cuadrillas[cboCuadrillas.SelectedIndex];
        }

        private BEClsOperario OperarioSeleccionado()
        {
            if (cboOperarios.SelectedIndex < 0)
                throw new Exception("Debe seleccionar un operario.");

            return ContextoAplicacion.Operarios[cboOperarios.SelectedIndex];
        }

        private BEClsObra ObraSeleccionada()
        {
            if (cboObras.SelectedIndex < 0)
                throw new Exception("Debe seleccionar una obra.");

            return ContextoAplicacion.Obras[cboObras.SelectedIndex];
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsCuadrilla cuadrilla = new BEClsCuadrilla(
                    int.Parse(txtCodigo.Text),
                    txtNombre.Text,
                    null);

                ContextoAplicacion.Cuadrillas.Add(cuadrilla);
                RefrescarCombos();
                lstEventos.Items.Add("Cuadrilla creada: " + cuadrilla.Nombre);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregarOperario_Click(object sender, EventArgs e)
        {
            try
            {
                bllCuadrilla.AgregarOperario(CuadrillaSeleccionada(), OperarioSeleccionado());
                lstEventos.Items.Add("Operario agregado a la cuadrilla.");
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuitarOperario_Click(object sender, EventArgs e)
        {
            try
            {
                bllCuadrilla.QuitarOperario(CuadrillaSeleccionada(), OperarioSeleccionado());
                lstEventos.Items.Add("Operario quitado de la cuadrilla.");
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                bllCuadrilla.AsignarObra(CuadrillaSeleccionada(), ObraSeleccionada());
                lstEventos.Items.Add("Obra asignada a la cuadrilla.");
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDesasignar_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsCuadrilla cuadrilla = CuadrillaSeleccionada();
                bllCuadrilla.DesasignarObra(cuadrilla, cuadrilla.ObraAsignada);
                lstEventos.Items.Add("Obra desasignada de la cuadrilla.");
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            try
            {
                bllCuadrilla.IniciarObra(CuadrillaSeleccionada());
                lstEventos.Items.Add("Obra iniciada.");
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            try
            {
                bllCuadrilla.InformarFinalizacionObra(CuadrillaSeleccionada());
                lstEventos.Items.Add("Obra finalizada por la cuadrilla.");
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarCombos()
        {
            int cuadrillaSeleccionada = cboCuadrillas == null ? -1 : cboCuadrillas.SelectedIndex;

            cboCuadrillas.Items.Clear();
            foreach (BEClsCuadrilla cuadrilla in ContextoAplicacion.Cuadrillas)
                cboCuadrillas.Items.Add(cuadrilla.Codigo + " - " + cuadrilla.Nombre);

            cboOperarios.Items.Clear();
            foreach (BEClsOperario operario in ContextoAplicacion.Operarios)
                cboOperarios.Items.Add(operario.Legajo + " - " + operario.Nombre + " " + operario.Apellido);

            cboObras.Items.Clear();
            foreach (BEClsObra obra in ContextoAplicacion.Obras)
                cboObras.Items.Add(obra.Codigo + " - " + obra.Nombre);

            if (cuadrillaSeleccionada >= 0 && cuadrillaSeleccionada < cboCuadrillas.Items.Count)
                cboCuadrillas.SelectedIndex = cuadrillaSeleccionada;

            ActualizarEstado();
        }

        private void ActualizarEstado()
        {
            if (cboCuadrillas.SelectedIndex < 0)
            {
                lblEstado.Text = "Seleccione una cuadrilla.";
                return;
            }

            BEClsCuadrilla cuadrilla = ContextoAplicacion.Cuadrillas[cboCuadrillas.SelectedIndex];
            string obra = cuadrilla.ObraAsignada == null
                ? "Sin obra asignada"
                : cuadrilla.ObraAsignada.Nombre + " (" + cuadrilla.ObraAsignada.Estado + ")";

            lblEstado.Text = "Operarios: " + cuadrilla.Operarios.Count + " | Obra: " + obra;
        }
    }
}
