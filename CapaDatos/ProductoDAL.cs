using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;
using CapaDatos;
using Capa;

namespace CapaDatos
{
    public class ProductoDAL
    {
        public List<Productos> Listarp()
        {
            List<Productos> lista = new List<Productos>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarProductos", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    // Construir mapa de columnas
                    var columnMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        columnMap[dr.GetName(i)] = i;
                    }

                    Func<string[], int> Ord = (candidates) =>
                    {
                        foreach (var c in candidates)
                        {
                            if (columnMap.TryGetValue(c, out int idx))
                                return idx;
                        }
                        return -1;
                    };

                    int idIdx = Ord(new[] { "Id_Productos", "Id_Producto", "Id" });
                    int nombreIdx = Ord(new[] { "Nombre_Producto", "Nombre" });
                    int precioIdx = Ord(new[] { "Precio_Producto", "Precio" });
                    int stockIdx = Ord(new[] { "Stock" });
                    int estadoIdx = Ord(new[] { "Estado" });
                    int IdCategoriaIdx = Ord(new[] { "Id_Categoria", "CategoriaId" });
                    int nombreCategoriaIdx = Ord(new[] { "Nombre_Categoria", "Categoria" });

                    while (dr.Read())
                    {
                        var producto = new Productos
                        {
                            Id_Productos = (idIdx >= 0 && !dr.IsDBNull(idIdx)) ? Convert.ToInt32(dr.GetValue(idIdx)) : 0,
                            Nombre_Producto = (nombreIdx >= 0 && !dr.IsDBNull(nombreIdx)) ? Convert.ToString(dr.GetValue(nombreIdx)) : string.Empty,
                            Precio_Producto = (precioIdx >= 0 && !dr.IsDBNull(precioIdx)) ? Convert.ToDecimal(dr.GetValue(precioIdx)) : 0.0m,
                            Stock = (stockIdx >= 0 && !dr.IsDBNull(stockIdx)) ? Convert.ToInt32(dr.GetValue(stockIdx)) : 0,
                            Estado = (estadoIdx >= 0 && !dr.IsDBNull(estadoIdx)) ? Convert.ToInt32(dr.GetValue(estadoIdx)) : 0,
                            Id_Categoria = (IdCategoriaIdx >= 0 && !dr.IsDBNull(IdCategoriaIdx)) ? Convert.ToInt32(dr.GetValue(IdCategoriaIdx)) : 0,
                            Nombre_Categoria = (nombreCategoriaIdx >= 0 && !dr.IsDBNull(nombreCategoriaIdx)) ? Convert.ToString(dr.GetValue(nombreCategoriaIdx)) : string.Empty
                        };

                        lista.Add(producto);
                    }
                }
            }
            return lista;
        }


        public void Insertarp(Productos producto)
        {
            if (producto.Id_Categoria < 2005 || producto.Id_Categoria > 2042)
            {
                throw new Exception("Categoría inválida. Seleccione una categoría válida.");
            }

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_InsertarProducto", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Nombre_Producto", producto.Nombre_Producto);
                cmd.Parameters.AddWithValue("@Precio_Producto", producto.Precio_Producto);
                cmd.Parameters.AddWithValue("@Stock", producto.Stock);
                cmd.Parameters.AddWithValue("@Estado", producto.Estado);
                cmd.Parameters.AddWithValue("@ID_Categoria", producto.Id_Categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminarp(Productos producto)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarProducto", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@Id_Productos", SqlDbType.Int).Value = producto.Id_Productos;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizarp(Productos producto)
        {
            if (producto.Id_Categoria < 2005 || producto.Id_Categoria > 2042)
                throw new Exception("Categoría inválida. Seleccione una categoría válida.");

            using (SqlConnection cn = Conexion.ObtenerConexion())
            using (SqlCommand cmd = new SqlCommand("sp_ActualizarProducto", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id_Productos", SqlDbType.Int).Value = producto.Id_Productos;
                cmd.Parameters.Add("@Nombre_Producto", SqlDbType.VarChar, 200).Value = producto.Nombre_Producto ?? string.Empty;

                var precioParam = cmd.Parameters.Add("@Precio_Producto", SqlDbType.Decimal);
                precioParam.Precision = 18;
                precioParam.Scale = 2;
                precioParam.Value = producto.Precio_Producto;

                cmd.Parameters.Add("@Stock", SqlDbType.Int).Value = producto.Stock;
                cmd.Parameters.Add("@Estado", SqlDbType.Int).Value = producto.Estado;
                cmd.Parameters.Add("@Id_Categoria", SqlDbType.Int).Value = producto.Id_Categoria;

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Productos ObtenerPorId(int idProducto)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Productos WHERE Id_Productos = @id", cn);
                cmd.Parameters.AddWithValue("@id", idProducto);

                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    return new Productos
                    {
                        Id_Productos = Convert.ToInt32(dr["Id_Productos"]),
                        Nombre_Producto = dr["Nombre_Producto"].ToString(),
                        Precio_Producto = Convert.ToDecimal(dr["Precio_Producto"]),
                        Stock = Convert.ToInt32(dr["Stock"]),
                        Id_Categoria = Convert.ToInt32(dr["Id_Categoria"])
                    };
                }
            }

            return null; // si no encuentra nada
        }
    }
}