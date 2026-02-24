using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa.Negocio;
using CapaEntidades;
using CapaPresentacion;

namespace CapaPresentacion
{
    public partial class FInicio : Form
    {
        public FInicio()
        {
            InitializeComponent();
        }

        private void FInicio_Load(object sender, EventArgs e)
        {
            ProgressBar progressBar = new ProgressBar
            {
                Style = ProgressBarStyle.Marquee,
                Dock = DockStyle.Fill,
                MarqueeAnimationSpeed = 30
            };
        }

        private void FormCategoria_Click(object sender, EventArgs e)
        {
            FrmCategorias frmc = new FrmCategorias();
            frmc.ShowDialog();
        }

        private void FormProductos_Click(object sender, EventArgs e)
        {
            FrmProductos frmp = new FrmProductos();
            frmp.ShowDialog();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
   
        }

        private void FormClientes_Click(object sender, EventArgs e)
        {
            FormC formC = new FormC();
            formC.ShowDialog();
        }

        private void FormVentas_Click(object sender, EventArgs e)
        {
            FormV formV = new FormV();
            formV.ShowDialog();
        }

        private void FormDetalleVentas_Click(object sender, EventArgs e)
        {
            FormFacturacion formF = new FormFacturacion();
            formF.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
