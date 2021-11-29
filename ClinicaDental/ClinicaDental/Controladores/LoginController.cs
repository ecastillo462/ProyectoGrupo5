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
            vista.EmailTextBox.TextChanged += EmailTextBox_TextChanged;
            vista.AceptarButton.Click += new EventHandler(ValidarUsuario);
            vista.CancelarButton.Click += new EventHandler(Cancelar);
            vista.AgregarButton.Click += new EventHandler(Agregar);
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            ClaseCompartida.EmailUsuario = vista.EmailTextBox.Text;
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
                System.Security.Principal.GenericIdentity identidad = new System.Security.Principal.GenericIdentity(vista.EmailTextBox.Text);
                System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(identidad, null);
                System.Threading.Thread.CurrentPrincipal = principal;

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
            string cadena = str + "MiClavePersonal";
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(cadena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
