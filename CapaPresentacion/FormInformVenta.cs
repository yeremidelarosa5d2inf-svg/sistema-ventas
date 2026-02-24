using CapaEntidades;
using CapaNegocio;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class FormInformVenta : Form
    {
        private BindingList<DetalleVenta> listaDetalles = new BindingList<DetalleVenta>();
        private BindingSource ventasBinding = new BindingSource();

        // Nueva propiedad pública para recibir el nombre del cliente desde quien crea el formulario
        public string Cliente { get; set; } = string.Empty;

        // Añadir propiedad pública para acceder a la lista de detalles
        public List<DetalleVenta> Detalles
        {
            get => listaDetalles.ToList();
            set
            {
                listaDetalles = new BindingList<DetalleVenta>(value ?? new List<DetalleVenta>());
                ventasBinding.DataSource = listaDetalles;
            }
        }

        public FormInformVenta()
        {
            InitializeComponent();
        }

        private void FormInformVenta_Load(object sender, EventArgs e)
        {
            // Mostrar control
            reportViewer1.Visible = true;

            // Calcular total
            decimal total = listaDetalles.Sum(d => d.Subtotal);

            // Preparar DataTable con los nombres de campos que usa el .rdlc (debe coincidir exactamente)
            DataTable dtVenta = new DataTable();
            dtVenta.Columns.Add("id_detalle", typeof(int));
            dtVenta.Columns.Add("id_venta", typeof(int));
            dtVenta.Columns.Add("id_productos", typeof(int));
            dtVenta.Columns.Add("NombreProducto", typeof(string)); // coincide con RDLC
            dtVenta.Columns.Add("Cantidad", typeof(int));          // coincide con RDLC
            dtVenta.Columns.Add("PrecioUnitario", typeof(decimal));// coincide con RDLC
            dtVenta.Columns.Add("Subtotal", typeof(decimal));      // coincide con RDLC

            foreach (var det in listaDetalles)
            {
                decimal subtotal = det.Subtotal;
                dtVenta.Rows.Add(0, 0, det.Id_Productos, det.NombreProducto, det.Cant, det.PrecioUnitario, subtotal);
            }

            // Preparar DataTable para DataSet1 (productos) porque el .rdlc también declara DataSet1
            DataTable dtProductos = new DataTable();
            dtProductos.Columns.Add("Id_Productos", typeof(int));
            dtProductos.Columns.Add("Nombre_Producto", typeof(string));
            dtProductos.Columns.Add("Precio_Producto", typeof(decimal));
            dtProductos.Columns.Add("Stock", typeof(int));
            dtProductos.Columns.Add("Estado", typeof(bool));
            dtProductos.Columns.Add("id_categoria", typeof(int));

            try
            {
                ProductoBL productoBL = new ProductoBL();
                var productos = productoBL.Listar();
                if (productos != null)
                {
                    foreach (var p in productos)
                    {
                        // Ajustar nombres de propiedades según tu entidad Productos
                        dtProductos.Rows.Add(p.Id_Productos, p.Nombre_Producto, p.Precio_Producto, p.Stock, p.Estado, p.Id_Categoria);
                    }
                }
            }
            catch
            {
                // Si falla leer productos, seguimos con una tabla vacía para evitar el error de "No hay instancia"
            }

            // Configurar ReportViewer
            reportViewer1.LocalReport.DataSources.Clear();

            // Añadir ambos DataSources. El nombre debe coincidir con los <DataSet Name="..."> del .rdlc
            ReportDataSource rdsVenta = new ReportDataSource("DataSetVenta", dtVenta);
            reportViewer1.LocalReport.DataSources.Add(rdsVenta);

            ReportDataSource rdsProductos = new ReportDataSource("DataSet1", dtProductos);
            reportViewer1.LocalReport.DataSources.Add(rdsProductos);

            // Pasar parámetros (ejemplo: cliente y total)
            string clienteTexto = string.IsNullOrEmpty(this.Cliente) ? string.Empty : this.Cliente;
            ReportParameter[] parametros = new ReportParameter[]
            {
                new ReportParameter("Cliente", clienteTexto),
                new ReportParameter("TotalGeneral", total.ToString("C"))
            };
            reportViewer1.LocalReport.SetParameters(parametros);

            // Forzar refresco
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }
    }
}
