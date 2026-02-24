using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Datos
{

    public class CategoriaDAL
    {
        public List<Categorias> Listar()
        {
            List<Categorias> lista = new List<Categorias>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarCategorias", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Categorias
                    {
                        Id_Categoria = (int)dr["Id_Categoria"],
                        Nombre_Categoria = dr["Nombre_Categoria"].ToString(),
                        Descripcion = (string)dr["Descripcion"]
                    });
                }
            }
            return lista;
        }

        public void Insertar(Categorias categoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Nombre_Categoria", categoria.Nombre_Categoria);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                cn.Open();
                cmd.ExecuteNonQuery(); 
            }
        }

        public void Actualizar(Categorias categoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand( "sp_ActualizarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Categoria", categoria.Id_Categoria);
                cmd.Parameters.AddWithValue("@Nombre_Categoria", categoria.Nombre_Categoria);
                cmd.Parameters.AddWithValue("@Descripcion", categoria.Descripcion);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminar(Categorias categoria)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_EliminarCategoria", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id_Categoria", categoria.Id_Categoria);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}