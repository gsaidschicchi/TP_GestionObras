using System;
using System.Windows.Forms;

namespace Presentacion_IU
{
    public class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            this.Text = "Sistema de Gestión de Obras";
            this.WindowState = FormWindowState.Maximized;
            this.IsMdiContainer = true;

            // MENU PRINCIPAL - corrección indicada por el profesor.
            MenuStrip menuPrincipal = new MenuStrip();

            ToolStripMenuItem menuArchivo = new ToolStripMenuItem("Archivo");
            ToolStripMenuItem menuGestion = new ToolStripMenuItem("Gestión");
            ToolStripMenuItem menuSupervision = new ToolStripMenuItem("Supervisión");
            ToolStripMenuItem menuReportes = new ToolStripMenuItem("Reportes");

            ToolStripMenuItem itemCerrarSesion = new ToolStripMenuItem("Cerrar sesión");
            itemCerrarSesion.Click += (s, e) => { this.DialogResult = DialogResult.OK; this.Close(); };

            ToolStripMenuItem itemSalir = new ToolStripMenuItem("Salir");
            itemSalir.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            ToolStripMenuItem itemContratistas = new ToolStripMenuItem("Contratistas");
            ToolStripMenuItem itemCuadrillas = new ToolStripMenuItem("Cuadrillas");
            ToolStripMenuItem itemOperarios = new ToolStripMenuItem("Operarios");
            ToolStripMenuItem itemObras = new ToolStripMenuItem("Obras");
            ToolStripMenuItem itemSupervisores = new ToolStripMenuItem("Supervisores");
            ToolStripMenuItem itemSupervision = new ToolStripMenuItem("Supervisión de Obras");
            ToolStripMenuItem itemReporteObras = new ToolStripMenuItem("Reporte de Obras");

            itemContratistas.Click += (s, e) => AbrirFormulario(new FrmContratistas());
            itemCuadrillas.Click += (s, e) => AbrirFormulario(new FrmCuadrillas());
            itemOperarios.Click += (s, e) => AbrirFormulario(new FrmOperarios());
            itemObras.Click += (s, e) => AbrirFormulario(new FrmObras());
            itemSupervisores.Click += (s, e) => AbrirFormulario(new FrmSupervisores());
            itemSupervision.Click += (s, e) => AbrirFormulario(new FrmSupervision());
            itemReporteObras.Click += (s, e) => AbrirFormulario(new FrmReporteObras());

            menuArchivo.DropDownItems.Add(itemCerrarSesion);
            menuArchivo.DropDownItems.Add(itemSalir);

            menuGestion.DropDownItems.Add(itemContratistas);
            menuGestion.DropDownItems.Add(itemCuadrillas);
            menuGestion.DropDownItems.Add(itemOperarios);
            menuGestion.DropDownItems.Add(itemObras);
            menuGestion.DropDownItems.Add(itemSupervisores);

            menuSupervision.DropDownItems.Add(itemSupervision);
            menuReportes.DropDownItems.Add(itemReporteObras);

            menuPrincipal.Items.Add(menuArchivo);
            menuPrincipal.Items.Add(menuGestion);
            menuPrincipal.Items.Add(menuSupervision);
            menuPrincipal.Items.Add(menuReportes);

            this.MainMenuStrip = menuPrincipal;
            this.Controls.Add(menuPrincipal);
        }

        private void AbrirFormulario(Form formulario)
        {
            formulario.MdiParent = this;
            formulario.Show();
        }
    }
}
