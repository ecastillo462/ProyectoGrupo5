using ClinicaDental.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                comando.Parameters.Clear();
                MiConexion.Close();
                
            }
            catch (Exception)
            {


            }
            return valido;
        }
        public bool InsertarNuevoUsuario(Usuario user)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO USUARIOS ");
                sql.Append(" VALUES (@Nombre, @Email, @Clave, @Foto); ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 80).Value = user.Nombre;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 100).Value = EncriptarClave(user.Clave);

                if (user.Imagen == null)
                {
                    comando.Parameters.Add("@Foto", SqlDbType.Image).Value = DBNull.Value;
                }
                else
                {
                    comando.Parameters.Add("@Foto", System.Data.SqlDbType.Image).Value = user.Imagen;
                }

                comando.ExecuteNonQuery();
                MiConexion.Close();
                comando.Parameters.Clear();
                return true;

            }
            catch (Exception)
            {
                return false;
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

        public DataTable GetUsuarios()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT ID, NOMBRE, EMAIL, IMAGEN FROM USUARIOS ");
                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                SqlDataReader dr = comando.ExecuteReader();
                dt.Load(dr);
                MiConexion.Close();
            }
            catch (Exception)
            {
            }
            return dt;
        }

        public bool ActualizarUsuario(Usuario user)
        {
            bool modifico = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE USUARIOS ");
                sql.Append(" SET NOMBRE = @Nombre, EMAIL = @Email, CLAVE = @Clave, IMAGEN = @Foto ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = user.Id;
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 80).Value = user.Nombre;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = user.Email;
                comando.Parameters.Add("@Clave", SqlDbType.NVarChar, 100).Value = EncriptarClave(user.Clave);

                if (user.Imagen == null)
                {
                    comando.Parameters.Add("@Foto", SqlDbType.Image).Value = DBNull.Value;
                }
                else
                {
                    comando.Parameters.Add("@Foto", System.Data.SqlDbType.Image).Value = user.Imagen;
                }

                comando.ExecuteNonQuery();
                modifico = true;
                MiConexion.Close();
                comando.Parameters.Clear();

            }
            catch (Exception )
            {
                return modifico;
            }
            return modifico;
        }

        public bool EliminarUsuario(int id)
        {
            bool modifico = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM USUARIOS ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                comando.ExecuteNonQuery();
                MiConexion.Close();
                comando.Parameters.Clear(); 
                modifico = true;

            }
            catch (Exception ex)
            {
                MiConexion.Close();
                MessageBox.Show("Ocurrió un error al intentar eliminar el usuario"); 
                return modifico;
            }
            return modifico;
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
