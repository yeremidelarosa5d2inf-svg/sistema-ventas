using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

public class Productos
{
    public int Id_Productos { get; set; }
    public string Nombre_Producto { get; set; }
    public decimal Precio_Producto { get; set; }
    public int Stock { get; set; }
    public int Estado { get; set; }
    public int Id_Categoria { get; set; }
    public string Nombre_Categoria { get; set; }
}