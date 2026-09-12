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

        private List<BEClsCuadrilla> cuadrillas = new List<BEClsCuadrilla>();

        private TextBox txtCUIT;
        private TextBox txtRazonSocial;
        private ComboBox cboContratistas;
        private ComboBox cboCuadrillas;
        private ComboBox cboObras;
        private ListBox lstEventos;

        public FrmContratistas()
        {
            this.Text = "Gestión de Contratistas";
            this.Size = new Size(800, 560);

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
            btnCrear.Size = new Size(160, 60);
            btnCrear.Click += btnCrear_Click;

            Label lblContratista = new Label();
            lblContratista.Text = "Contratista:";
            lblContratista.Location = new Point(30, 200);

            cboContratistas = new ComboBox();
            cboContratistas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboContratistas.Location = new Point(150, 197);
            cboContratistas.Width = 260;

            Label lblCuadrilla = new Label();
            lblCuadrilla.Text = "Cuadrilla:";
            lblCuadrilla.Location = new Point(30, 240);

            cboCuadrillas = new ComboBox();
            cboCuadrillas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCuadrillas.Location = new Point(150, 237);
            cboCuadrillas.Width = 260;

            Button btnAgregar = new Button();
            btnAgregar.Text = "Agregar Cuadrilla";
            btnAgregar.Location = new Point(440, 235);
            btnAgregar.Size = new Size(145, 30);
            btnAgregar.Click += btnAgregar_Click;

            Button btnQuitar = new Button();
            btnQuitar.Text = "Quitar Cuadrilla";
            btnQuitar.Location = new Point(600, 235);
            btnQuitar.Size = new Size(145, 30);
            btnQuitar.Click += btnQuitar_Click;

            Label lblObra = new Label();
            lblObra.Text = "Obra:";
            lblObra.Location = new Point(30, 280);

            cboObras = new ComboBox();
            cboObras.DropDownStyle = ComboBoxStyle.DropDownList;
            cboObras.Location = new Point(150, 277);
            cboObras.Width = 260;

            Button btnAsignar = new Button();
            btnAsignar.Text = "Asignar Obra a Cuadrilla";
            btnAsignar.Location = new Point(440, 275);
            btnAsignar.Size = new Size(200, 30);
            btnAsignar.Click += btnAsignar_Click;

            Button btnRecibir = new Button();
            btnRecibir.Text = "Recibir Finalización";
            btnRecibir.Location = new Point(150, 330);
            btnRecibir.Size = new Size(170, 35);
            btnRecibir.Click += btnRecibir_Click;

            Button btnInformar = new Button();
            btnInformar.Text = "Informar al Supervisor";
            btnInformar.Location = new Point(340, 330);
            btnInformar.Size = new Size(180, 35);
            btnInformar.Click += btnInformar_Click;

            lstEventos = new ListBox();
            lstEventos.Location = new Point(30, 400);
            lstEventos.Size = new Size(715, 100);

            this.Controls.Add(titulo);
            this.Controls.Add(lblCUIT);
            this.Controls.Add(txtCUIT);
            this.Controls.Add(lblRazon);
            this.Controls.Add(txtRazonSocial);
            this.Controls.Add(btnCrear);
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
                throw new Exception("Debe seleccionar una contratista.");

            return ContextoAplicacion.Contratistas[cboContratistas.SelectedIndex];
        }

        private BEClsCuadrilla CuadrillaSeleccionada()
        {
            if (cboCuadrillas.SelectedIndex < 0)
                throw new Exception("Debe seleccionar una cuadrilla.");

            return cuadrillas[cboCuadrillas.SelectedIndex];
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
                BEClsContratista contratista = new BEClsContratista(txtCUIT.Text, txtRazonSocial.Text);
                ContextoAplicacion.Contratistas.Add(contratista);
                RefrescarCombos();
                lstEventos.Items.Add("Contratista creada: " + contratista.RazonSocial);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                bllContratista.AgregarCuadrilla(ContratistaSeleccionada(), CuadrillaSeleccionada());
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
                lstEventos.Items.Add("La contratista informó la obra al Supervisor.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarCombos()
        {
            cboContratistas.Items.Clear();
            foreach (BEClsContratista contratista in ContextoAplicacion.Contratistas)
                cboContratistas.Items.Add(contratista.CUIT + " - " + contratista.RazonSocial);

            cuadrillas = bllCuadrilla.ListarTodo();

            cboCuadrillas.Items.Clear();
            foreach (BEClsCuadrilla cuadrilla in cuadrillas)
                cboCuadrillas.Items.Add(cuadrilla.Codigo + " - " + cuadrilla.Nombre);

            cboObras.Items.Clear();
            foreach (BEClsObra obra in ContextoAplicacion.Obras)
                cboObras.Items.Add(obra.Codigo + " - " + obra.Nombre);
        }
    }
}
