using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class VentaDAL
    {
        public void InsertarV(Venta venta)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_cliente", venta.Id_Cliente);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<Venta> Listar()
        {
            List<Venta> lista = new List<Venta>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_listar_ventas", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new Venta
                    {
                        Id_Venta = Convert.ToInt32(dr["id_venta"]),
                        Fecha_Venta = Convert.ToDateTime(dr["fecha_venta"]),
                        NombreCliente = dr["cliente"].ToString(),
                        Total_General = Convert.ToDecimal(dr["total_general"]),
                        Estado_Venta = Convert.ToBoolean(dr["estado_venta"])
                    });
                }
            }

            return lista;
        }

        public void ActualizarTotalV(int idVenta)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_total_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_venta", idVenta);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AnularV(int idVenta)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_anular_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_venta", idVenta);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}

