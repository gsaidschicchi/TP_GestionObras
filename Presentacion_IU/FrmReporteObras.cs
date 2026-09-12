using BE_GestionObras;
using BLL_GestionObras;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmReporteObras : Form
    {
        private readonly BLLClsObra bllObra = new BLLClsObra();
        private DataGridView dgvObras;
        private Label lblResumen;

        public FrmReporteObras()
        {
            this.Text = "Reporte de Obras";
            this.Size = new Size(900, 550);

            Label titulo = new Label();
            titulo.Text = "Reporte General de Obras";
            titulo.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            titulo.AutoSize = true;
            titulo.Location = new Point(30, 20);

            lblResumen = new Label();
            lblResumen.Location = new Point(30, 65);
            lblResumen.AutoSize = true;

            dgvObras = new DataGridView();
            dgvObras.Location = new Point(30, 105);
            dgvObras.Size = new Size(820, 370);
            dgvObras.ReadOnly = true;
            dgvObras.AutoGenerateColumns = true;
            dgvObras.AllowUserToAddRows = false;

            this.Controls.Add(titulo);
            this.Controls.Add(lblResumen);
            this.Controls.Add(dgvObras);

            CargarReporte();
        }

        private void CargarReporte()
        {
            List<BEClsObra> obras = bllObra.ListarTodo();

            int pendientes = 0;
            int enEjecucion = 0;
            int finalizadas = 0;
            int aprobadas = 0;
            int rechazadas = 0;

            foreach (BEClsObra obra in obras)
            {
                if (obra.Estado == EstadoObra.PENDIENTE)
                {
                    pendientes++;
                }

                if (obra.Estado == EstadoObra.EN_EJECUCION)
                {
                    enEjecucion++;
                }

                if (obra.Estado == EstadoObra.FINALIZADA)
                {
                    finalizadas++;
                }

                if (obra.EstadoSupervision == EstadoSupervision.APROBADO)
                {
                    aprobadas++;
                }

                if (obra.EstadoSupervision == EstadoSupervision.RECHAZADO)
                {
                    rechazadas++;
                }
            }

            lblResumen.Text =
                "Total: " + obras.Count +
                " | Pendientes: " + pendientes +
                " | En ejecución: " + enEjecucion +
                " | Finalizadas: " + finalizadas +
                " | Aprobadas: " + aprobadas +
                " | Rechazadas: " + rechazadas;

            dgvObras.DataSource = null;
            dgvObras.DataSource = obras;
        }
    }
}
