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
    public  class ServiciosDAO:Conexion
    {
        SqlCommand comando = new SqlCommand();
        public bool InsertarNuevoServicio(Servicios servicios)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO SERVICIOS ");
                sql.Append(" VALUES (@Nombre,@Descripcion,@Costo); ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 80).Value = servicios.Nombre;
                comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 100).Value = servicios.Descripcion;
                comando.Parameters.Add("@Costo", SqlDbType.Decimal).Value = servicios.Costo;
                comando.ExecuteNonQuery();
                MiConexion.Close();
                return true;
            }
            catch (Exception)
            {
                //throw;
                return false;
            }
        }

        public bool ModificarServicios(Servicios servicios)
        {
            bool modifico = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE SERVICIOS ");
                sql.Append(" SET NOMBRE = @Nombre, DESCRIPCION = @Descripcion, COSTO = @Costo");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = servicios.Id;
                comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 50).Value = servicios.Nombre;
                comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 100).Value = servicios.Descripcion;
                comando.Parameters.Add("@Costo", SqlDbType.Decimal).Value = servicios.Costo;
                comando.ExecuteNonQuery();
                modifico = true;
                MiConexion.Close();

            }
            catch (Exception )
            {
                return modifico;
            }
            return modifico;
        }

        public bool EliminarServicios(int id)
        {
            bool elimino = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM SERVICIOS ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.Parameters.Clear();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                comando.ExecuteNonQuery();
                elimino = true;
                MiConexion.Close();

            }
            catch (Exception )
            {
                return elimino;
            }
            return elimino;
        }

        public DataTable GetServicios()
        {
            DataTable dt = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT * FROM SERVICIOS ");

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





    }
}
