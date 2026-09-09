using BE_GestionObras;
using BLL_GestionObras;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmSupervision : Form
    {
        private readonly BLLClsSupervisor bllSupervisor = new BLLClsSupervisor();
        private ComboBox cboObras;
        private Label lblEstado;
        private ListBox lstEventos;

        public FrmSupervision()
        {
            this.Text = "Supervisión de Obras";
            this.Size = new Size(650, 420);

            Label titulo = new Label();
            titulo.Text = "Supervisión de Obras";
            titulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);

            Label lblObra = new Label();
            lblObra.Text = "Obra:";
            lblObra.Location = new Point(30, 100);

            cboObras = new ComboBox();
            cboObras.DropDownStyle = ComboBoxStyle.DropDownList;
            cboObras.Location = new Point(150, 97);
            cboObras.Width = 300;
            cboObras.SelectedIndexChanged += (s, e) => ActualizarEstado();

            lblEstado = new Label();
            lblEstado.Location = new Point(30, 155);
            lblEstado.AutoSize = true;
            lblEstado.Text = "Seleccione una obra.";

            Button btnAprobar = new Button();
            btnAprobar.Text = "Aprobar";
            btnAprobar.Location = new Point(100, 210);
            btnAprobar.Size = new Size(150, 40);
            btnAprobar.Click += btnAprobar_Click;

            Button btnRechazar = new Button();
            btnRechazar.Text = "Rechazar";
            btnRechazar.Location = new Point(280, 210);
            btnRechazar.Size = new Size(150, 40);
            btnRechazar.Click += btnRechazar_Click;

            lstEventos = new ListBox();
            lstEventos.Location = new Point(30, 285);
            lstEventos.Size = new Size(570, 80);

            this.Controls.Add(titulo);
            this.Controls.Add(lblObra);
            this.Controls.Add(cboObras);
            this.Controls.Add(lblEstado);
            this.Controls.Add(btnAprobar);
            this.Controls.Add(btnRechazar);
            this.Controls.Add(lstEventos);

            this.Activated += (s, e) => RefrescarObras();
            RefrescarObras();
        }

        private BEClsObra ObraSeleccionada()
        {
            if (cboObras.SelectedIndex < 0)
                throw new Exception("Debe seleccionar una obra.");

            return ContextoAplicacion.Obras[cboObras.SelectedIndex];
        }

        private void btnAprobar_Click(object sender, EventArgs e)
        {
            try
            {
                bllSupervisor.AprobarObra(ObraSeleccionada());
                lstEventos.Items.Add("Obra aprobada por el Supervisor.");
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRechazar_Click(object sender, EventArgs e)
        {
            try
            {
                bllSupervisor.RechazarObra(ObraSeleccionada());
                lstEventos.Items.Add("Obra rechazada por el Supervisor.");
                ActualizarEstado();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefrescarObras()
        {
            int seleccion = cboObras == null ? -1 : cboObras.SelectedIndex;

            cboObras.Items.Clear();
            foreach (BEClsObra obra in ContextoAplicacion.Obras)
                cboObras.Items.Add(obra.Codigo + " - " + obra.Nombre);

            if (seleccion >= 0 && seleccion < cboObras.Items.Count)
                cboObras.SelectedIndex = seleccion;

            ActualizarEstado();
        }

        private void ActualizarEstado()
        {
            if (cboObras.SelectedIndex < 0)
            {
                lblEstado.Text = "Seleccione una obra.";
                return;
            }

            BEClsObra obra = ContextoAplicacion.Obras[cboObras.SelectedIndex];
            lblEstado.Text = "Estado Obra: " + obra.Estado +
                             " | Informada: " + (obra.InformadaAlSupervisor ? "SI" : "NO") +
                             " | Supervisión: " + obra.EstadoSupervision;
        }
    }
}
