using ClinicaDental.Controladores;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaDental.Vistas
{
    public partial class ServiciosView : Form
    {
        public ServiciosView()
        {
            InitializeComponent();
            ServiciosController controlador = new ServiciosController(this);
        }
    }
}
