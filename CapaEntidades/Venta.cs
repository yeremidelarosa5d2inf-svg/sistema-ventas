using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Venta
    {
        public int Id_Venta { get; set; }
        public DateTime Fecha_Venta { get; set; }
        public int Id_Cliente { get; set; }
        public decimal Total_General { get; set; }
        public string NombreCliente { get; set; }
        public bool Estado_Venta { get; set; }
    }
}