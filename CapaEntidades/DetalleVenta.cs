using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class DetalleVenta
    {
            public int Id_Detalle { get; set; }
            public int Id_Venta { get; set; }
            public int Id_Productos { get; set; }
            public string NombreProducto { get; set; }
            public int Cant { get; set; }
            public decimal PrecioUnitario { get; set; }
            public decimal Subtotal
            {
                get { return Cant * PrecioUnitario; }
            }
    }
}