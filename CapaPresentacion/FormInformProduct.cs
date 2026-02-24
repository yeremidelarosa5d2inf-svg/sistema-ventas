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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CapaPresentacion
{
    public partial class FormInformProduct : Form
    {
        private ProductoBL productoBL = new ProductoBL();
        public FormInformProduct()
        {
            InitializeComponent();

        }

        private void FormInformProduct_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();

            DataTable dt = new DataTable();
            //dt.Columns.Add("Id_Productos", typeof(int));
            dt.Columns.Add("Nombre_Producto", typeof(string));
            dt.Columns.Add("Precio_Producto", typeof(decimal));
            dt.Columns.Add("Stock", typeof(int));
            dt.Columns.Add("Estado", typeof(int));

            foreach (var p in productoBL.Listar())
            {
                dt.Rows.Add( p.Nombre_Producto, p.Precio_Producto, p.Stock, p.Estado);
            }

            var rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);

            reportViewer1.RefreshReport();
            /*
            comboBox1.DataSource = bl.Listar();
            comboBox1.DisplayMember = "Nombre_Categoria";
            comboBox1.ValueMember = "Id_Categoria";
            */
            this.reportViewer1.RefreshReport();

            
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void dgvProductos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            
        }
    }
}
