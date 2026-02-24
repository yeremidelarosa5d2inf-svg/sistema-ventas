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
    public class ClienteDAL
    {
        public List<Cliente> Listarc()
        {
            List<Cliente> listac = new List<Cliente>();

            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cn.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listac.Add(new Cliente
                    {
                        id_cliente = (int)dr["id_cliente"],
                        nombre = dr["nombre"].ToString(),
                        direccion = (string)dr["direccion"],
                        telefono = dr["telefono"].ToString(),
                        correo = (string)dr["correo"]
                    });
                }
            }
            return listac;
        }

        public void Insertarc(Cliente cliente)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_ListarClientes", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                cmd.Parameters.AddWithValue("@direccion", cliente.direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.telefono);
                cmd.Parameters.AddWithValue("@correo", cliente.correo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }





        public void insertarc(Cliente cliente)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertarCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                cmd.Parameters.AddWithValue("@direccion", cliente.direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.telefono);
                cmd.Parameters.AddWithValue("@correo", cliente.correo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Actualizarc(Cliente cliente)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_actualizar_cliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_cliente", cliente.id_cliente);
                cmd.Parameters.AddWithValue("@nombre", cliente.nombre);
                cmd.Parameters.AddWithValue("@direccion", cliente.direccion);
                cmd.Parameters.AddWithValue("@telefono", cliente.telefono);
                cmd.Parameters.AddWithValue("@correo", cliente.correo);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Eliminarc(Cliente cliente)
        {
            using (SqlConnection cn = Conexion.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_eliminar_cliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_cliente", cliente.id_cliente);

                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
