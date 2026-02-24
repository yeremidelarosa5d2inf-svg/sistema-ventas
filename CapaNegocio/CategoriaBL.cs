using Capa.Datos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa.Negocio
{

    public class CategoriaBL
    {
        private CategoriaDAL dal = new CategoriaDAL();

        public List<Categorias> Listar()
        {
            return dal.Listar();
        }

        public void Agregar(Categorias categoria)
        {
            if (categoria.Id_Categoria == 0)
                dal.Insertar(categoria);
            else
                dal.Actualizar(categoria);
        }

        public void Eliminar(Categorias Id_Categoria)
        {
            dal.Eliminar(Id_Categoria);
        }
    }
}
