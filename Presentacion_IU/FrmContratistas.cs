using BE_GestionObras;
using BLL_GestionObras;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmContratistas : Form
    {
        private readonly BLLClsContratista bllContratista = new BLLClsContratista();
        private readonly BLLClsCuadrilla bllCuadrilla = new BLLClsCuadrilla();
        private readonly BLLClsObra bllObra = new BLLClsObra();

        private List<BEClsContratista> contratistas = new List<BEClsContratista>();
        private List<BEClsCuadrilla> cuadrillas = new List<BEClsCuadrilla>();
        private List<BEClsObra> obras = new List<BEClsObra>();

        private TextBox txtCUIT;
        private TextBox txtRazonSocial;
        private ComboBox cboContratistas;
        private ComboBox cboCuadrillas;
        private ComboBox cboObras;
        private ListBox lstEventos;

        public FrmContratistas()
        {
            this.Text = "Gestión de Contratistas";
            this.Size = new Size(820, 600);

            Label titulo = new Label();
            titulo.Text = "Gestión de Contratistas";
            titulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);

            Label lblCUIT = new Label();
            lblCUIT.Text = "CUIT:";
            lblCUIT.Location = new Point(30, 85);

            txtCUIT = new TextBox();
            txtCUIT.Location = new Point(150, 82);
            txtCUIT.Width = 220;

            Label lblRazon = new Label();
            lblRazon.Text = "Razón Social:";
            lblRazon.Location = new Point(30, 125);

            txtRazonSocial = new TextBox();
            txtRazonSocial.Location = new Point(150, 122);
            txtRazonSocial.Width = 220;

            Button btnCrear = new Button();
            btnCrear.Text = "Crear Contratista";
            btnCrear.Location = new Point(400, 82);
            btnCrear.Size = new Size(150, 30);
            btnCrear.Click += btnCrear_Click;

            Button btnModificar = new Button();
            btnModificar.Text = "Modificar";
            btnModificar.Location = new Point(560, 82);
            btnModificar.Size = new Size(100, 30);
            btnModificar.Click += btnModificar_Click;

            Button btnEliminar = new Button();
            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new Point(670, 82);
            btnEliminar.Size = new Size(100, 30);
            btnEliminar.Click += btnEliminar_Click;

            Label lblContratista = new Label();
            lblContratista.Text = "Contratista:";
            lblContratista.Location = new Point(30, 180);

            cboContratistas = new ComboBox();
            cboContratistas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboContratistas.Location = new Point(150, 177);
            cboContratistas.Width = 300;
            cboContratistas.SelectedIndexChanged += cboContratistas_SelectedIndexChanged;

            Label lblCuadrilla = new Label();
            lblCuadrilla.Text = "Cuadrilla:";
            lblCuadrilla.Location = new Point(30, 230);

            cboCuadrillas = new ComboBox();
            cboCuadrillas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCuadrillas.Location = new Point(150, 227);
            cboCuadrillas.Width = 260;

            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar Cuadrilla";
            btnAgregar.Location = new Point(440, 225);
            btnAgregar.Size = new Size(145, 30);
            btnAgregar.Click += btnAgregar_Click;

            Button btnQuitar = new Button();
            btnQuitar.Text = "Quitar Cuadrilla";
            btnQuitar.Location = new Point(600, 225);
            btnQuitar.Size = new Size(145, 30);
            btnQuitar.Click += btnQuitar_Click;

            Label lblObra = new Label();
            lblObra.Text = "Obra:";
            lblObra.Location = new Point(30, 275);

            cboObras = new ComboBox();
            cboObras.DropDownStyle = ComboBoxStyle.DropDownList;
            cboObras.Location = new Point(150, 272);
            cboObras.Width = 260;

            Button btnAsignar = new Button();
            btnAsignar.Text = "Asignar Obra a Cuadrilla";
            btnAsignar.Location = new Point(440, 270);
            btnAsignar.Size = new Size(200, 30);
            btnAsignar.Click += btnAsignar_Click;

            Button btnRecibir = new Button();
            btnRecibir.Text = "Recibir Finalización";
            btnRecibir.Location = new Point(150, 325);
            btnRecibir.Size = new Size(170, 35);
            btnRecibir.Click += btnRecibir_Click;

            Button btnInformar = new Button();
            btnInformar.Text = "Informar al Supervisor";
            btnInformar.Location = new Point(340, 325);
            btnInformar.Size = new Size(180, 35);
            btnInformar.Click += btnInformar_Click;

            lstEventos = new ListBox();
            lstEventos.Location = new Point(30, 400);
            lstEventos.Size = new Size(715, 120);

            this.Controls.Add(titulo);
            this.Controls.Add(lblCUIT);
            this.Controls.Add(txtCUIT);
            this.Controls.Add(lblRazon);
            this.Controls.Add(txtRazonSocial);
            this.Controls.Add(btnCrear);
            this.Controls.Add(btnModificar);
            this.Controls.Add(btnEliminar);
            this.Controls.Add(lblContratista);
            this.Controls.Add(cboContratistas);
            this.Controls.Add(lblCuadrilla);
            this.Controls.Add(cboCuadrillas);
            this.Controls.Add(btnAgregar);
            this.Controls.Add(btnQuitar);
            this.Controls.Add(lblObra);
            this.Controls.Add(cboObras);
            this.Controls.Add(btnAsignar);
            this.Controls.Add(btnRecibir);
            this.Controls.Add(btnInformar);
            this.Controls.Add(lstEventos);

            this.Activated += (s, e) => RefrescarCombos();
            RefrescarCombos();
        }

        private BEClsContratista ContratistaSeleccionada()
        {
            if (cboContratistas.SelectedIndex < 0)
            {
                throw new Exception("Debe seleccionar una contratista.");
            }

            return contratistas[cboContratistas.SelectedIndex];
        }

        private BEClsCuadrilla CuadrillaSeleccionada()
        {
            if (cboCuadrillas.SelectedIndex < 0)
            {
                throw new Exception("Debe seleccionar una cuadrilla.");
            }

            return cuadrillas[cboCuadrillas.SelectedIndex];
        }

        private BEClsObra ObraSeleccionada()
        {
            if (cboObras.SelectedIndex < 0)
            {
                throw new Exception("Debe seleccionar una obra.");
            }

            return obras[cboObras.SelectedIndex];
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                BEClsContratista contratista = new BEClsContratista(txtCUIT.Text, txtRazonSocial.Text);

                bool resultado = bllContratista.CrearContratista(contratista);

                if (resultado)
                {
                    RefrescarCombos();
                    lstEventos.Items.Add("Contratista creada: " + contratista.RazonSocial);
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
                BEClsContratista contratista = ContratistaSeleccionada();
                contratista.RazonSocial = txtRazonSocial.Text;

                if (bllContratista.ModificarContratista(contratista))
                {
                    RefrescarCombos();
                    lstEventos.Items.Add("Contratista modificada.");
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
                BEClsContratista contratista = ContratistaSeleccionada();

                if (bllContratista.EliminarContratista(contratista))
                {
                    RefrescarCombos();
                    lstEventos.Items.Add("Contratista eliminada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboContratistas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboContratistas.SelectedIndex < 0)
            {
                return;
            }

            BEClsContratista contratista = contratistas[cboContratistas.SelectedIndex];
            txtCUIT.Text = contratista.CUIT;
            txtRazonSocial.Text = contratista.RazonSocial;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                bllContratista.AgregarCuadrilla(ContratistaSeleccionada(), CuadrillaSeleccionada());
                RefrescarCombos();
                lstEventos.Items.Add("Cuadrilla agregada a la contratista.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                bllContratista.QuitarCuadrilla(ContratistaSeleccionada(), CuadrillaSeleccionada());
                RefrescarCombos();
                lstEventos.Items.Add("Cuadrilla quitada de la contratista.");
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
                bllContratista.AsignarObraACuadrilla(
                    ContratistaSeleccionada(),
                    CuadrillaSeleccionada(),
                    ObraSeleccionada());

                RefrescarCombos();
                lstEventos.Items.Add("Obra asignada a la cuadrilla por la contratista.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRecibir_Click(object sender, EventArgs e)
        {
            try
            {
                bllContratista.RecibirFinalizacionObra(ObraSeleccionada());
                lstEventos.Items.Add("La contratista recibió la finalización de la obra.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInformar_Click(object sender, EventArgs e)
        {
            try
            {
                bllContratista.InformarFinalizacionAlSupervisor(ObraSeleccionada());
                RefrescarCombos();
                lstEventos.Items.Add("La contratista informó la obra al Supervisor.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarCombos()
        {
            contratistas = bllContratista.ListarTodo();
            cuadrillas = bllCuadrilla.ListarTodo();
            obras = bllObra.ListarTodo();

            cboContratistas.Items.Clear();
            foreach (BEClsContratista contratista in contratistas)
            {
                cboContratistas.Items.Add(contratista.CUIT + " - " + contratista.RazonSocial);
            }

            cboCuadrillas.Items.Clear();
            foreach (BEClsCuadrilla cuadrilla in cuadrillas)
            {
                cboCuadrillas.Items.Add(cuadrilla.Codigo + " - " + cuadrilla.Nombre);
            }

            cboObras.Items.Clear();
            foreach (BEClsObra obra in obras)
            {
                cboObras.Items.Add(obra.Codigo + " - " + obra.Nombre);
            }
        }
    }
}
