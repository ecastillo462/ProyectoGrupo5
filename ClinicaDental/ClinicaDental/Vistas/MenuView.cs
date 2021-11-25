using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ClinicaDental.Vistas
{
    public partial class MenuView : Syncfusion.Windows.Forms.Office2010Form
    {
        public MenuView()
        {
            InitializeComponent();
        }

        ClientesView vistaClientes;
        ServiciosView vistaServicios;
        ConsultasView vistaConsulta;

        private void ClienteToolStripButton_Click(object sender, EventArgs e)
        {
            if (vistaClientes == null )
            {
                vistaClientes = new ClientesView();
                vistaClientes.MdiParent = this;
                vistaClientes.FormClosed += Vista_FormClosed;
                vistaClientes.Show();
            }
        }

        private void Vista_FormClosed(object sender, FormClosedEventArgs e)
        {
            vistaClientes = null;
            vistaServicios = null;
            vistaConsulta = null;
        }

        private void ServicioToolStripButton_Click(object sender, EventArgs e)
        {
            if (vistaServicios == null)
            {
                vistaServicios = new ServiciosView();
                vistaServicios.MdiParent = this;
                vistaServicios.FormClosed += Vista_FormClosed;
                vistaServicios.Show();
            }
        }

        private void ConsultaToolStripButton_Click(object sender, EventArgs e)
        {
            if (vistaConsulta == null)
            {
                vistaConsulta = new ConsultasView();
                vistaConsulta.MdiParent = this;
                vistaConsulta.FormClosed += Vista_FormClosed;
                vistaConsulta.Show();
            }
        }
    }
}
