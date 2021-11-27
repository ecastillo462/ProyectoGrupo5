using ClinicaDental.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicaDental.Modelos.DAO
{
    public class UsuarioDAO: Conexion
    {
        SqlCommand comando = new SqlCommand();

        public bool ValidarUsuario(Usuario user)
        {
            bool valido = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT 1 FROM USUARIOS WHERE EMAIL = @Email AND CLAVE = @Clave;");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 100).Value = user.Clave;
                valido = Convert.ToBoolean(comando.ExecuteScalar());
                MiConexion.Close(); 
            }
            catch (Exception)
            {


            }
            return valido;
        }

        public string GetUsuarioPorEmail(string email)
        {
            string nombre = ""; 
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT NOMBRE FROM USUARIOS ");
                sql.Append(" WHERE EMAIL = @Email; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.AddWithValue("@Email", email); 
                
                nombre = comando.ExecuteScalar().ToString();

                comando.Parameters.Clear(); 
                MiConexion.Close();

            }
            catch (Exception ex)
            {
                MiConexion.Close();
            }
            return nombre;
        }

        public int getIdUsuarioPorNombre(string nombre)
        {
            //DataTable clientesDT = new DataTable();
            int IdUsuario = 0;

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT ID FROM USUARIOS WHERE NOMBRE = @Nombre ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@Nombre", nombre);

                IdUsuario = Convert.ToInt32(comando.ExecuteScalar());

                MiConexion.Close();
                comando.Parameters.Clear(); 
            }
            catch (Exception e)
            {

            }

            return IdUsuario;
        }
    }
}
