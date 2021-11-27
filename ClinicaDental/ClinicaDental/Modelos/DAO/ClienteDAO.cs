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
                sql.Append(" VALUES (@Nombre, @Edad, @Genero, @Telefono, @Email); ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 80).Value = client.Nombre;
                comando.Parameters.Add("@Edad", SqlDbType.Int).Value = client.Edad;
                comando.Parameters.Add("@Genero", SqlDbType.NVarChar, 20).Value = client.Genero;
                comando.Parameters.Add("@Telefono", SqlDbType.NVarChar, 16).Value = client.Telefono;
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = client.Nombre;

                comando.ExecuteNonQuery();
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
                sql.Append(" SET NOMBRE = @Nombre, EDAD = @Edad, GENERO = @Genero, TELEFONO = @Telefono, EMAIL = @Email  ");
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
                comando.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = client.Nombre;

                comando.ExecuteNonQuery();
                modifico = true;
                MiConexion.Close();

            }
            catch (Exception ex)
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
                modifico = true;
                MiConexion.Close();

            }
            catch (Exception ex)
            {
                return modifico;
            }
            return modifico;
        }
    }
}
