using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using CapaEntidades;
using System.Configuration;
using System.Data.SqlClient;
using CapaNegocio;

namespace CapaPresentacion
{
    public partial class FormFacturacion : Form
    {
        public FormFacturacion()
        {
            InitializeComponent();
            
            reportViewer1.Visible = false;
        }
        private void Listar()
        {
            VentaBL cn = new VentaBL();
            dataGridView1.DataSource = cn.Listar();
        }
        private void FormFacturacion_Load(object sender, EventArgs e)
        {
            Listar();
            try
            {
                CargarDetalleVentaEnGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.reportViewer1.RefreshReport();
        }

        private DataTable ObtenerDetalleVentaDesdeBD()
        {
            string connStr = ConfigurationManager.ConnectionStrings["cn"]?.ConnectionString;
            if (string.IsNullOrWhiteSpace(connStr))
                throw new InvalidOperationException("No se encontró la cadena de conexión 'cn' en App.config.");

            var dt = new DataTable();
            // Selecciona los campos que quieres mostrar y usar en el informe
            string sql = @"
                SELECT 
                    dv.id_detalle AS Id_Detalle,
                    dv.id_venta   AS Id_Venta,
                    dv.id_productos AS Id_Productos,
                    p.Nombre_Producto AS NombreProducto,
                    dv.cant AS Cant,
                    dv.precio AS PrecioUnitario,
                    (dv.cant * dv.precio) AS Subtotal
                FROM Detalle_venta dv
                LEFT JOIN Productos p ON dv.id_productos = p.Id_Productos
                ORDER BY dv.id_detalle;
            ";

            using (var conn = new SqlConnection(connStr))
            using (var da = new SqlDataAdapter(sql, conn))
            {
                da.Fill(dt);
            }

            return dt;
        }

        private void CargarDetalleVentaEnGrid()
        {
            var dt = ObtenerDetalleVentaDesdeBD();
            dataGridView1.DataSource = dt;

            if (dataGridView1.Columns.Contains("Id_Detalle")) dataGridView1.Columns["Id_Detalle"].Visible = false;
            if (dataGridView1.Columns.Contains("Id_Venta")) dataGridView1.Columns["Id_Venta"].Visible = false;
            if (dataGridView1.Columns.Contains("Id_Productos")) dataGridView1.Columns["Id_Productos"].Visible = false;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void btnVerFactura_Click(object sender, EventArgs e)
        {
            try
            {
                // Construir la lista de detalles a partir del grid (si hay filas seleccionadas, usar sólo esas)
                var detalles = new List<DetalleVenta>();

                if (dataGridView1.DataSource is DataTable dt)
                {
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                        {
                            var det = new DetalleVenta
                            {
                                Id_Detalle = row.Cells["Id_Detalle"]?.Value != null ? Convert.ToInt32(row.Cells["Id_Detalle"].Value) : 0,
                                Id_Venta = row.Cells["Id_Venta"]?.Value != null ? Convert.ToInt32(row.Cells["Id_Venta"].Value) : 0,
                                Id_Productos = row.Cells["Id_Productos"]?.Value != null ? Convert.ToInt32(row.Cells["Id_Productos"].Value) : 0,
                                NombreProducto = row.Cells["NombreProducto"]?.Value?.ToString() ?? string.Empty,
                                Cant = row.Cells["Cant"]?.Value != null ? Convert.ToInt32(row.Cells["Cant"].Value) : 0,
                                PrecioUnitario = row.Cells["PrecioUnitario"]?.Value != null ? Convert.ToDecimal(row.Cells["PrecioUnitario"].Value) : 0m
                            };
                            detalles.Add(det);
                        }
                    }
                    else
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            var det = new DetalleVenta
                            {
                                Id_Detalle = r.Table.Columns.Contains("Id_Detalle") && r["Id_Detalle"] != DBNull.Value ? Convert.ToInt32(r["Id_Detalle"]) : 0,
                                Id_Venta = r.Table.Columns.Contains("Id_Venta") && r["Id_Venta"] != DBNull.Value ? Convert.ToInt32(r["Id_Venta"]) : 0,
                                Id_Productos = r.Table.Columns.Contains("Id_Productos") && r["Id_Productos"] != DBNull.Value ? Convert.ToInt32(r["Id_Productos"]) : 0,
                                NombreProducto = r.Table.Columns.Contains("NombreProducto") ? r["NombreProducto"].ToString() : string.Empty,
                                Cant = r.Table.Columns.Contains("Cant") && r["Cant"] != DBNull.Value ? Convert.ToInt32(r["Cant"]) : 0,
                                PrecioUnitario = r.Table.Columns.Contains("PrecioUnitario") && r["PrecioUnitario"] != DBNull.Value ? Convert.ToDecimal(r["PrecioUnitario"]) : 0m
                            };
                            detalles.Add(det);
                        }
                    }
                }
                else
                {
                    var dt2 = ObtenerDetalleVentaDesdeBD();
                    foreach (DataRow r in dt2.Rows)
                    {
                        var det = new DetalleVenta
                        {
                            Id_Detalle = r.Table.Columns.Contains("Id_Detalle") && r["Id_Detalle"] != DBNull.Value ? Convert.ToInt32(r["Id_Detalle"]) : 0,
                            Id_Venta = r.Table.Columns.Contains("Id_Venta") && r["Id_Venta"] != DBNull.Value ? Convert.ToInt32(r["Id_Venta"]) : 0,
                            Id_Productos = r.Table.Columns.Contains("Id_Productos") && r["Id_Productos"] != DBNull.Value ? Convert.ToInt32(r["Id_Productos"]) : 0,
                            NombreProducto = r.Table.Columns.Contains("NombreProducto") ? r["NombreProducto"].ToString() : string.Empty,
                            Cant = r.Table.Columns.Contains("Cant") && r["Cant"] != DBNull.Value ? Convert.ToInt32(r["Cant"]) : 0,
                            PrecioUnitario = r.Table.Columns.Contains("PrecioUnitario") && r["PrecioUnitario"] != DBNull.Value ? Convert.ToDecimal(r["PrecioUnitario"]) : 0m
                        };
                        detalles.Add(det);
                    }
                }

                if (detalles.Count == 0)
                {
                    MessageBox.Show("No hay detalle de venta para mostrar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Abrir el formulario separado que contiene el ReportViewer
                var informe = new FormInformVenta();
                informe.Detalles = detalles;
                // Si no se tiene nombre de cliente, usar un valor por defecto para evitar el error de parámetro faltante
                informe.Cliente = "Consumidor Final";

                informe.ShowDialog(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error generando la factura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
