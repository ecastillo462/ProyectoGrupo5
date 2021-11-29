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
    public class FacturaDAO: Conexion
    {
        SqlCommand comando = new SqlCommand();
        public bool CrearFactura(Factura factura)
        {
            bool inserto = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" INSERT INTO FACTURA ");
                sql.Append(" VALUES (@Id_Consulta, @SubTotal, @Descuento, @Isv, @Total); ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Id_Consulta", SqlDbType.Int).Value = factura.IdConsulta;
                comando.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = factura.SubTotal;
                comando.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = factura.Descuento;
                comando.Parameters.Add("@Isv", SqlDbType.Decimal).Value = factura.ISV;
                comando.Parameters.Add("@Total", SqlDbType.Decimal).Value = factura.Total;
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();

                MiConexion.Close();
                inserto = true;
            }
            catch (Exception ex)
            {
                return inserto;
            }

            return inserto;
        }
        public bool ActualizarFactura(Factura factura)
        {
            bool modifico = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" UPDATE FACTURA ");
                sql.Append(" SET ID_CONSULTA = @Id_Consulta, SUBTOTAL = @SubTotal, DESCUENTO = @Descuento, ISV = @Isv, TOTAL = @Total ");
                sql.Append(" WHERE ID = @Id; ");

                comando.Connection = MiConexion;
                MiConexion.Open();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = sql.ToString();
                comando.Parameters.Add("@Id", SqlDbType.Int).Value = factura.Id;
                comando.Parameters.Add("@Id_Consulta", SqlDbType.Int).Value = factura.IdConsulta;
                comando.Parameters.Add("@SubTotal", SqlDbType.Decimal).Value = factura.SubTotal;
                comando.Parameters.Add("@Descuento", SqlDbType.Decimal).Value = factura.Descuento;
                comando.Parameters.Add("@Isv", SqlDbType.Decimal).Value = factura.ISV;
                comando.Parameters.Add("@Total", SqlDbType.Decimal).Value = factura.Total;
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                MiConexion.Close();
                modifico = true;
            }
            catch (Exception)
            {
                return modifico;
                
            }
            return modifico;
        }
        public bool EliminarFactura(int id)
        {
            bool elimino = false;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" DELETE FROM FACTURA ");
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
        public DataTable GetFacturas()
        {
            DataTable consultasDT = new DataTable();
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT F.ID,C.ID ID_CONSULTA,U.NOMBRE USUARIO,CL.NOMBRE CLIENTE,S.NOMBRE SERVICIO, C.FECHA, ");
                sql.Append(" F.SUBTOTAL, F.DESCUENTO, F.ISV, F.TOTAL FROM FACTURA F");
                sql.Append(" JOIN CONSULTA C ON C.ID = F.ID_CONSULTA ");
                sql.Append(" JOIN USUARIOS U ON U.ID = C.ID_USUARIO ");
                sql.Append(" JOIN CLIENTES CL ON CL.ID = C.ID_CLIENTE ");
                sql.Append(" JOIN SERVICIOS S ON S.ID = C.ID_SERVICIO; ");

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
    }
}
