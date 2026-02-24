using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class VentaBL
    {
            private VentaDAL ventaDAL = new VentaDAL();

            public void InsertarVen(Venta venta)
            {
                ventaDAL.InsertarV(venta);
            }

            public List<Venta> Listar()
            {
                return ventaDAL.Listar();
            }

            public void ActualizarTotalVen(int idVenta)
            {
                ventaDAL.ActualizarTotalV(idVenta);
            }

            public void AnularVen(int idVenta)
            {
                ventaDAL.AnularV(idVenta);
            }
    }
}
