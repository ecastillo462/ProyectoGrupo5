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
    public class ClienteDAO : Conexion
    {
        SqlCommand comando = new SqlCommand();

        public bool InsertarCliente(Cliente client)
        {
            bool inserto = false;

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO CLIENTES ");
                sql.Append(" VALUES (@Nombre, @Edad, @Genero, @Telefono, @Email, @Foto); ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 80).Value = client.Nombre;
                comando.Parameters.Add("@Edad", SqlDbType.Int).Value = client.Edad;
                comando.Parameters.Add("@Genero", SqlDbType.NVarChar, 20).Value = client.Genero;
                comando.Parameters.Add("@Telefono", SqlDbType.NVarChar, 16).Value = client.Telefono;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = client.Email;

                if (client.Imagen == null)
                {
                    comando.Parameters.Add("@Foto", SqlDbType.Image).Value = DBNull.Value;
                }
                else
                {
                    comando.Parameters.Add("@Foto", System.Data.SqlDbType.Image).Value = client.Imagen;
                }

                comando.ExecuteNonQuery();
                comando.Parameters.Clear(); 
                
                MiConexion.Close();
                inserto = true;
            }
            catch (Exception)
            {
                return inserto;
            }

            return inserto;
        }

        public DataTable GetClientes()
        {
            DataTable clientesDT = new DataTable();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM CLIENTES ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                SqlDataReader dr = comando.ExecuteReader();
                clientesDT.Load(dr);

                MiConexion.Close();
            }
            catch (Exception)
            {

            }

            return clientesDT;
        }

        public bool ActualizarCliente(Cliente client)
        {
            bool modifico = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE CLIENTES ");
                sql.Append(" SET NOMBRE = @Nombre, EDAD = @Edad, GENERO = @Genero, TELEFONO = @Telefono, EMAIL = @Email, IMAGEN = @Foto ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = client.Id;
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 80).Value = client.Nombre;
                comando.Parameters.Add("@Edad", SqlDbType.Int).Value = client.Edad;
                comando.Parameters.Add("@Genero", SqlDbType.NVarChar, 20).Value = client.Genero;
                comando.Parameters.Add("@Telefono", SqlDbType.NVarChar, 16).Value = client.Telefono;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = client.Email;

                if (client.Imagen == null)
                {
                    comando.Parameters.Add("@Foto", SqlDbType.Image).Value = DBNull.Value;
                }
                else
                {
                    comando.Parameters.Add("@Foto", System.Data.SqlDbType.Image).Value = client.Imagen;
                }

                comando.ExecuteNonQuery();

                comando.Parameters.Clear(); 
                
                MiConexion.Close();

                modifico = true;

            }
            catch (Exception e)
            {
                return modifico;
            }
            return modifico;
        }

        public bool EliminarCliente(int id)
        {
            bool modifico = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM CLIENTES ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                comando.ExecuteNonQuery();

                comando.Parameters.Clear(); 

                modifico = true;
                MiConexion.Close();

            }
            catch (Exception ex)
            {
                return modifico;
            }
            return modifico;
        }

        public int getIdClientePorNombre(string nombre)
        {
            int idCliente = 0; 

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT ID FROM CLIENTES WHERE NOMBRE = @Nombre ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.AddWithValue("@Nombre", nombre);

                idCliente = Convert.ToInt32(comando.ExecuteScalar()); 

                MiConexion.Close();

                comando.Parameters.Clear(); 
            }
            catch (Exception)
            {

            }

            return idCliente;
        }

    }
}
