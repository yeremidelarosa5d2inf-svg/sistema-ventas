using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class DetalleVentaBL
    {
        private DetalleVentaDAL detalleDAL = new DetalleVentaDAL();

        public void Insertar(DetalleVenta detalle)
        {
            detalleDAL.Insertar(detalle);
        }

        public List<DetalleVenta> ListarPorVenta(int idVenta)
        {
            return detalleDAL.ListarPorVenta(idVenta);
        }

        public void Eliminar(int idDetalle)
        {
            detalleDAL.Eliminar(idDetalle);
        }

        public void Actualizar(DetalleVenta detalle)
        {
            detalleDAL.Actualizar(detalle);
        }

        public decimal CalcularTotal(int idVenta)
        {
            return detalleDAL.CalcularTotal(idVenta);
        }
    }
}
