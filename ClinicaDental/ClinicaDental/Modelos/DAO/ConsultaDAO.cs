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
    class ConsultaDAO:Conexion
    {
        SqlCommand comando = new SqlCommand();
        public bool InsertarConsulta(Consulta consulta)
        {
            bool inserto = false;

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO CONSULTA ");
                sql.Append(" VALUES (@IdUsuario, @IdCliente, @IdTipoServicio, @Fecha, @Descripcion); ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = consulta.IdUsuario;
                comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = consulta.IdCliente;
                comando.Parameters.Add("@IdTipoServicio", SqlDbType.NVarChar, 20).Value = consulta.IdTipoServicio;
                comando.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = consulta.Fecha;
                comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 200).Value = consulta.Descripcion;

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
        public bool ActualizarConsulta(Consulta consulta)
        {
            bool modifico = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE CONSULTA ");
                sql.Append(" SET ID_USUARIO = @Id_Usuario, ID_CLIENTE = @Id_Cliente, ID_SERVICIO = @Id_Servicio, FECHA = @Fecha, DESCRIPCION = @Descripcion ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = consulta.Id;
                comando.Parameters.Add("@Id_Usuario", SqlDbType.Int).Value = consulta.IdUsuario;
                comando.Parameters.Add("@Id_Cliente", SqlDbType.Int).Value = consulta.IdCliente;
                comando.Parameters.Add("@Id_Servicio", SqlDbType.Int).Value = consulta.IdTipoServicio;
                comando.Parameters.Add("@Fecha", SqlDbType.DateTime).Value = consulta.Fecha;
                comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, 200).Value = consulta.Descripcion;

                comando.ExecuteNonQuery();

                comando.Parameters.Clear();

                MiConexion.Close();

                modifico = true;

            }
            catch (Exception e)
            {
                MiConexion.Close(); 
                return modifico;
            }
            return modifico;
        }
        public bool EliminarConsulta(int id)
        {
            bool elimino = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM CONSULTA ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                comando.Parameters.Add("@Id", SqlDbType.Int).Value = id;

                comando.ExecuteNonQuery();

                comando.Parameters.Clear();

                elimino = true;
                MiConexion.Close();

            }
            catch (Exception e)
            {
                return elimino;
            }
            return elimino;
        }
        public DataTable GetConsultas()
        {
            DataTable consultasDT = new DataTable();

            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT C.ID as Id_Consulta,  U.NOMBRE As Usuario,  CL.NOMBRE as Cliente, S.NOMBRE as Servicio, C.FECHA as Fecha, C.DESCRIPCION as Descripcion ");
                sql.Append(" FROM CONSULTA C, USUARIOS U, SERVICIOS S, CLIENTES CL ");
                sql.Append(" WHERE C.ID_USUARIO = U.ID AND C.ID_CLIENTE = CL.ID AND C.ID_SERVICIO = S.ID; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();

                SqlDataReader dr = comando.ExecuteReader();
                consultasDT.Load(dr);

                MiConexion.Close();
            }
            catch (Exception)
            {

            }

            return consultasDT;
        }
        public decimal GetSubTotal(int Id)
        {
            decimal SubTotal = 0;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT S.COSTO FROM CONSULTA C ");
                sql.Append(" JOIN SERVICIOS S ON S.ID = C.ID_SERVICIO ");
                sql.Append(" WHERE C.ID = @Id");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = Id;
                SubTotal = Convert.ToDecimal(comando.ExecuteScalar());

                comando.Parameters.Clear();
                MiConexion.Close();
            }
            catch (Exception ex)
            {
                MiConexion.Close();
            }
            return SubTotal;
        }
    }
}
