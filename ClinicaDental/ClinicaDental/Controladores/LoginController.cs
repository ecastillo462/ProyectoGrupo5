using ClinicaDental.Modelos.DAO;
using ClinicaDental.Modelos.Entidades;
using ClinicaDental.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaDental.Controladores
{
    public class LoginController
    {
        LoginView vista;

        public LoginController (LoginView view)
        {
            vista = view;

            vista.AceptarButton.Click += new EventHandler(ValidarUsuario);
            vista.CancelarButton.Click += new EventHandler(Cancelar);
            vista.AgregarButton.Click += new EventHandler(Agregar);
        }

        private void Agregar(object sender, EventArgs e)
        {
            UsuariosView mostrar = new UsuariosView();
            mostrar.Show();
        }

        private void Cancelar(object sender, EventArgs e)
        {
            LimpiarControles();
        }
        private void LimpiarControles()
        {
            vista.EmailTextBox.Clear();
            vista.ContraseñaTextBox.Clear();
        }
        private void ValidarUsuario(object sender, EventArgs e)
        {
            bool esValido = false;
            UsuarioDAO userDAO = new UsuarioDAO();

            Usuario user = new Usuario();

            user.Email = vista.EmailTextBox.Text;
            user.Clave = EncriptarClave(vista.ContraseñaTextBox.Text);

            esValido = userDAO.ValidarUsuario(user);

            if (esValido)
            {
               // MessageBox.Show("Usuario Correcto");
                MenuView menu = new MenuView();
                vista.Hide();

                menu.ShowDialog();
                vista.Close(); 
            }
            else
            {
                MessageBox.Show("Usuario Incorrecto");
            }
        }
         public static string EncriptarClave(string str)
         {
              SHA256 sha256 = SHA256Managed.Create();
              ASCIIEncoding encoding = new ASCIIEncoding();
              byte[] streams = null;
              StringBuilder sb = new StringBuilder();
              streams = sha256.ComputeHash(encoding.GetBytes(str));
              for (int i = 0; i < streams.Length; i++) sb.AppendFormat("{0:x2}", streams[i]);
              return sb.ToString();
         }

        
    }
}
