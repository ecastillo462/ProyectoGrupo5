using ClinicaDental.Modelos.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaDental
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UsuarioDAO usuarioDAO = new UsuarioDAO(); 

            if (usuarioDAO.ProbarConexion())
            {
                MessageBox.Show("Ta bueno");
            } else
            {
                MessageBox.Show("No ta bueno :("); 
            }
        }
    }
}
