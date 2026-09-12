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

            ToolStripMenuItem itemSalir = new ToolStripMenuItem("Salir");
            itemSalir.Click += (s, e) => Application.Exit();

            ToolStripMenuItem itemContratistas = new ToolStripMenuItem("Contratistas");
            ToolStripMenuItem itemCuadrillas = new ToolStripMenuItem("Cuadrillas");
            ToolStripMenuItem itemOperarios = new ToolStripMenuItem("Operarios");
            ToolStripMenuItem itemObras = new ToolStripMenuItem("Obras");
            ToolStripMenuItem itemSupervision = new ToolStripMenuItem("Supervisión de Obras");

            itemContratistas.Click += (s, e) => AbrirFormulario(new FrmContratistas());
            itemCuadrillas.Click += (s, e) => AbrirFormulario(new FrmCuadrillas());
            itemOperarios.Click += (s, e) => AbrirFormulario(new FrmOperarios());
            itemObras.Click += (s, e) => AbrirFormulario(new FrmObras());
            itemSupervision.Click += (s, e) => AbrirFormulario(new FrmSupervision());

            menuArchivo.DropDownItems.Add(itemSalir);

            menuGestion.DropDownItems.Add(itemContratistas);
            menuGestion.DropDownItems.Add(itemCuadrillas);
            menuGestion.DropDownItems.Add(itemOperarios);
            menuGestion.DropDownItems.Add(itemObras);

            menuSupervision.DropDownItems.Add(itemSupervision);

            menuPrincipal.Items.Add(menuArchivo);
            menuPrincipal.Items.Add(menuGestion);
            menuPrincipal.Items.Add(menuSupervision);

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
