using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class ProductoBL
    {
        private ProductoDAL dalp = new ProductoDAL();
        public List<Productos> Listar()
        {
            return dalp.Listarp();
        }
        public void Agregar(Productos producto)
        {
            if (producto.Id_Productos == 0)
                dalp.Insertarp(producto);
            else
                dalp.Actualizarp(producto);
        }
        public void Eliminar(Productos Id_Producto)
        {
            dalp.Eliminarp(Id_Producto);
        }
        public Productos ObtenerPorId(int idProducto)
        {
            return dalp.ObtenerPorId(idProducto);
        }

    }
}
