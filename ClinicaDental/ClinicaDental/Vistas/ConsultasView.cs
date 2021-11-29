using ClinicaDental.Controladores;
using ClinicaDental.Modelos.DAO;
using ClinicaDental.Modelos.Entidades;
using System.Windows.Forms;

namespace ClinicaDental.Vistas
{
    public partial class ConsultasView : Form
    {
        public Usuario user = new Usuario(); 
        UsuarioDAO userDAO = new UsuarioDAO();
        public ConsultasView()
        {
            InitializeComponent();

            ConsultaController controlador = new ConsultaController(this);
        }
    }
}
