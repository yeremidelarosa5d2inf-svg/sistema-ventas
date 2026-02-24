using Capa.Negocio;
using CapaEntidades;
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
   
    public partial class FrmCategorias : Form
    {
        private CategoriaBL bl = new CategoriaBL();
        private int idSeleccionado = 0;
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre1.Text))
            {
                MessageBox.Show("El nombre de la Categoria es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("La descripcion de la Categoria es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }
            return true;
        }

        public FrmCategorias()
        {
            InitializeComponent();
            Listar();
        }

        private void Listar()
        {
          dgvCategoria.DataSource = bl.Listar();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ValidarCampos()) {
                Categorias c = new Categorias
                {
                    Id_Categoria = idSeleccionado,
                    Nombre_Categoria = txtNombre1.Text,
                    Descripcion = textBox1.Text,
                };

                bl.Agregar(c);
                Limpiar();
                Listar();

                MessageBox.Show("Se ha agregado una Nueva Categoria");
            }
            
        }

        private void dgvCategorias_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            idSeleccionado = Convert.ToInt32(
              dgvCategoria.CurrentRow.Cells["Id_Categoria"].Value);

            txtNombre1.Text =
             dgvCategoria.CurrentRow.Cells["Nombre_Categoria"].Value.ToString();
            txtNombre2.Text =
             dgvCategoria.CurrentRow.Cells["Nombre_Categoria"].Value.ToString();
            textBox1.Text =
             dgvCategoria.CurrentRow.Cells["Descripcion"].Value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvCategoria.SelectedRows.Count > 0)
            {
                
                DialogResult result = MessageBox.Show("¿Está seguro de que desea desactivar esta categoría?",
                    "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    int Id = Convert.ToInt32(dgvCategoria.CurrentRow.Cells["Id_Categoria"].Value);

                    var categoria = new Categorias { Id_Categoria = idSeleccionado};
                    bl.Eliminar(categoria);

                    MessageBox.Show("Categoria eliminada");
                    Listar();
                    Limpiar();
                }
            }
            else
            {
                MessageBox.Show("Seleccione un registro de la tabla.");
            }
        }

        private void Limpiar()
        {
            idSeleccionado = 0;
            txtNombre1.Clear();
            txtNombre2.Clear();
            textBox1.Clear();
        }

        private void FrmCategorias_Load(object sender, EventArgs e)
        {
            dgvCategoria.Columns["Id_Categoria"].Visible = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvCategoria_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtNombre2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}