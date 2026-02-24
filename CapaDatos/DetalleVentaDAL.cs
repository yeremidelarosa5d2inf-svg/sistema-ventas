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
    public class DetalleVentaDAL
    {
        public void Insertar(DetalleVenta detalle)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_insertar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_venta", detalle.Id_Venta);
                cmd.Parameters.AddWithValue("@id_productos", detalle.Id_Productos);
                cmd.Parameters.AddWithValue("@cant", detalle.Cant);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public List<DetalleVenta> ListarPorVenta(int idVenta)
        {
            List<DetalleVenta> lista = new List<DetalleVenta>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_listar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_venta", idVenta);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    lista.Add(new DetalleVenta
                    {
                        Id_Productos = 0,
                        Cant = Convert.ToInt32(dr["cant"]),
                        PrecioUnitario = Convert.ToDecimal(dr["precio"]),
                        
                    });
                }
            }

            return lista;
        }

        public void Eliminar(int idDetalle)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_eliminar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_detalle", idDetalle);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizar(DetalleVenta detalle)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_detalle_venta", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_detalle", detalle.Id_Detalle);
                cmd.Parameters.AddWithValue("@cant", detalle.Cant);
                cmd.Parameters.AddWithValue("@precio", detalle.PrecioUnitario);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public decimal CalcularTotal(int idVenta)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_calcular_total_factura", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_venta", idVenta);

                cn.Open();
                return Convert.ToDecimal(cmd.ExecuteScalar());
            }
        }
    }
}
