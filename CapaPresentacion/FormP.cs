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
using CapaNegocio;
using CapaPresentacion;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using System.Configuration;

namespace CapaPresentacion
{
   
    public partial class FrmProductos : Form
    {
        private CategoriaBL bl = new CategoriaBL();
        private ProductoBL productoBL = new ProductoBL();
        private int idSeleccionado = 0;
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombreProducto.Text))
            {
                MessageBox.Show("El nombre del Producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombreProducto.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                MessageBox.Show("El precio del Producto es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("El stock del Producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.Focus();
                return false;
            }
            else if (!int.TryParse(textBox1.Text, out _))
            {
                MessageBox.Show("El stock debe ser un número entero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                textBox1.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(comboBox1.Text))
            {
                MessageBox.Show("La categoria del Producto es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }

            return true;
        }

        public FrmProductos()
        {
            InitializeComponent();
            Listarp();
        }

        private void Listarp()
        {
            dgvProductos.DataSource = productoBL.Listar();

            dgvProductos.Columns["Id_Categoria"].Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Debe seleccionar una categoría.");
                return;
            }

            if (ValidarCampos())
            {
                Productos producto = new Productos
                {
                    Id_Productos = idSeleccionado, // si estás editando, si no puedes omitirlo
                    Nombre_Producto = txtNombreProducto.Text,

                    // Validación segura para precio
                    Precio_Producto = decimal.TryParse(txtPrecio.Text, out decimal precio) ? precio : 0,

                    // Validación segura para stock
                    Stock = int.TryParse(textBox1.Text, out int stock) ? stock : 0,

                    Estado = 1,

                    // Guardar el Id de la categoría desde el ComboBox
                    Id_Categoria = comboBox1.SelectedValue != null ? Convert.ToInt32(comboBox1.SelectedValue) : 0
                };

                // Guardar producto
                ProductoBL productoBL = new ProductoBL();
                productoBL.Agregar(producto);

                // Refrescar DataGrid
                dgvProductos.DataSource = productoBL.Listar();

                // Mensaje de confirmación
                MessageBox.Show("Producto guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpiar campos
                Limpiar();

            }

            /*Productos p = new Productos
            {
                Id_Productos = idSeleccionado,
                Nombre_Producto = txtNombreProducto.Text,
                Precio_Producto = decimal.TryParse(txtPrecio.Text, out decimal precio) ? precio : 0,
                Stock = int.TryParse(textBox1.Text, out int stock) ? stock : 0,
                Estado = 1,
                // Guardar el Id de la categoría (ValueMember) en la entidad
                Id_Categoria = comboBox1.SelectedValue != null ? Convert.ToInt32(comboBox1.SelectedValue) : 0,
            };

            ProductoBL productoBL = new ProductoBL();
            productoBL.Agregar(p);
            Limpiar();
            Listarp();

            MessageBox.Show("Se ha agregado un Nuevo Producto");
            */
        }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvProductos.CurrentRow?.DataBoundItem is Productos prod)
            {
                idSeleccionado = prod.Id_Productos;

                // Campos del groupbox de edición
                txtNombreProducto.Text = prod.Nombre_Producto ?? string.Empty;
                txtPrecio.Text = prod.Precio_Producto.ToString();

                // Asignar la categoría al combobox usando Id_Categoria (ValueMember).
                // Usar try/catch para evitar excepción si el Id no existe en la lista del combo.
                try
                {
                    comboBox1.SelectedValue = prod.Id_Categoria;
                }
                catch
                {
                    // Si el valor no está presente en el DataSource del combo, dejarlo sin seleccionar.
                    comboBox1.SelectedIndex = -1;
                }

                // El form tenía dos controles usados para stock: "textBox1" y "txtStock".
                // Rellenamos ambos si existen para mantenerlos sincronizados.
                textBox1.Text = prod.Stock.ToString();
                var txtStockCtrl = this.Controls.Find("txtStock", true).FirstOrDefault() as TextBox;
                if (txtStockCtrl != null) txtStockCtrl.Text = prod.Id_Productos.ToString();

                FillEliminarControls(prod);
            }
        }

        private void FillEliminarControls(Productos prod)
        {
            if (prod == null) return;

            
            Action<string, string> SetText = (name, value) =>
            {
                var ctrl = this.Controls.Find(name, true).FirstOrDefault();
                if (ctrl is TextBox tb) tb.Text = value;
                else if (ctrl is Label lbl) lbl.Text = value;
            };

            SetText("txtIdEliminar", prod.Id_Productos.ToString());
            SetText("lblIdEliminar", prod.Id_Productos.ToString());

            SetText("txtNombreEliminar", prod.Nombre_Producto ?? string.Empty);
            SetText("lblNombreEliminar", prod.Nombre_Producto ?? string.Empty);

            SetText("txtPrecioEliminar", prod.Precio_Producto.ToString());
            SetText("lblPrecioEliminar", prod.Precio_Producto.ToString());

            SetText("txtStockEliminar", prod.Stock.ToString());
            SetText("lblStockEliminar", prod.Stock.ToString());

            var cbEliminar = this.Controls.Find("comboBoxEliminar", true).FirstOrDefault() as ComboBox;
            if (cbEliminar != null)
            {
                try { cbEliminar.SelectedValue = prod.Id_Categoria; } catch { }
            }
            else
            {
                SetText("lblCategoriaEliminar", prod.Id_Categoria.ToString());
                SetText("txtCategoriaEliminar", prod.Id_Categoria.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                // MENSAJE DE ALERTA ANTES DE ELIMINAR
                DialogResult result = MessageBox.Show("¿Está seguro de que desea desactivar este Producto?",
                    "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    int Id;
                    if (!int.TryParse(Convert.ToString(dgvProductos.CurrentRow.Cells["Id_Productos"].Value), out Id) || Id <= 0)
                    {
                        MessageBox.Show("Id de producto inválido.");
                        return;
                    }

                    var producto = new Productos { Id_Productos = Id };
                    productoBL.Eliminar(producto);

                    MessageBox.Show("Producto eliminado");
                    Listarp();
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
            txtNombreProducto.Clear();
            txtStock.Clear();
            txtPrecio.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Clear();

            Action<string> ClearIfExists = (name) =>
            {
                var ctrl = this.Controls.Find(name, true).FirstOrDefault();
                if (ctrl is TextBox tb) tb.Clear();
                else if (ctrl is Label lbl) lbl.Text = string.Empty;
            };

            ClearIfExists("txtIdEliminar");
            ClearIfExists("lblIdEliminar");
            ClearIfExists("txtNombreEliminar");
            ClearIfExists("lblNombreEliminar");
            ClearIfExists("txtPrecioEliminar");
            ClearIfExists("lblPrecioEliminar");
            ClearIfExists("txtStockEliminar");
            ClearIfExists("lblStockEliminar");
            ClearIfExists("lblCategoriaEliminar");
            ClearIfExists("txtCategoriaEliminar");
        }

        private void FrmProductos_Load(object sender, EventArgs e)
        {
            dgvProductos.Columns["Id_Productos"].Visible = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
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

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void fillByToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

            FormInformProduct formInformProduct = new FormInformProduct();
            formInformProduct.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void dataSet1BindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
            
        }

        private void reportViewer2_Load(object sender, EventArgs e)
        {
           
        }
    }
}