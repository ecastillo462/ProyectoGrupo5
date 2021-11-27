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
    public partial class UsuariosView : Form
    {
        public UsuariosView()
        {
            InitializeComponent();
            UsuarioController controller = new UsuarioController(this);
        }
    }
}
