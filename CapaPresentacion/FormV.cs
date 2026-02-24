using CapaNegocio;
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

namespace CapaPresentacion
{
    public partial class FormV : Form
    {
        // Usar BindingList para que el DataGridView observe cambios automáticamente
        private BindingList<DetalleVenta> listaDetalles = new BindingList<DetalleVenta>();
        private BindingSource ventasBinding = new BindingSource();

        public FormV()
        {
            InitializeComponent();

            // configurar binding del DataGridView
            ventasBinding.DataSource = listaDetalles;
            dvgVenta.AutoGenerateColumns = true;
            dvgVenta.DataSource = ventasBinding;
            dvgVenta.DataBindingComplete += DvgVenta_DataBindingComplete;
        }

        private void FormV_Load(object sender, EventArgs e)
        {
            ClienteBL clienteBL = new ClienteBL();
            ProductoBL productoBL = new ProductoBL();

            // ComboBox de clientes
            cbxCliente.DataSource = clienteBL.ListarCl();
            cbxCliente.DisplayMember = "nombre";
            cbxCliente.ValueMember = "id_cliente";

            // ComboBox de productos
            cbxProducto.DataSource = productoBL.Listar();
            cbxProducto.DisplayMember = "Nombre_Producto";
            cbxProducto.ValueMember = "Id_Productos";

            txtTotal.ReadOnly = true;
            txtTotal.TabStop = false; // opcional: evita que reciba foco al tabular
        }

        private bool ValidarCantidad()
        {
            if (!int.TryParse(txtCantidad.Text, out int cantidad))
            {
                MessageBox.Show("La cantidad debe ser un número entero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Obtener el producto seleccionado
            ProductoBL productoBL = new ProductoBL();
            int idProducto = Convert.ToInt32(cbxProducto.SelectedValue);
            Productos producto = productoBL.ObtenerPorId(idProducto);

            if (cantidad <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cantidad > producto.Stock)
            {
                MessageBox.Show($"La cantidad no puede ser mayor al stock disponible ({producto.Stock}).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cbxCliente.SelectedIndex != -1)
            {
                cbxCliente.Enabled = false; // Bloquear cliente una vez que se empieza la venta
            }
            if (ValidarCantidad())
            {
                int idProducto = Convert.ToInt32(cbxProducto.SelectedValue);
                ProductoBL productoBL = new ProductoBL();
                Productos producto = productoBL.ObtenerPorId(idProducto);

                DetalleVenta detalle = new DetalleVenta
                {
                    Id_Productos = producto.Id_Productos,
                    NombreProducto = producto.Nombre_Producto,
                    Cant = Convert.ToInt32(txtCantidad.Text),
                    PrecioUnitario = producto.Precio_Producto
                };

                // Añadir al BindingList actualiza automáticamente el DataGridView
                listaDetalles.Add(detalle);
                UpdateTotalTextbox();
                btnFacturar.Enabled = listaDetalles.Any();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            ReiniciarFormulario();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void btnFacturar_Click(object sender, EventArgs e)
        {
            // Calcular total
            decimal total = listaDetalles.Sum(d => d.Subtotal);
            UpdateTotalTextbox();

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


            // Crear FormInformVenta, pasar los datos y mostrarlo
            var formInformVenta = new FormInformVenta
            {
                Cliente = cbxCliente.Text,
                Detalles = listaDetalles.ToList() // pasar copia de la lista de detalles
                                                  // Opcional: podría añadirse una propiedad DtVenta/DtProductos si prefiere pasar DataTables
            };

            formInformVenta.ShowDialog();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {
        }

        private void reportViewer1_Load_1(object sender, EventArgs e)
        {
        }

        private void ReiniciarFormulario()
        {
            // limpiar datos en memoria (BindingList notifica automáticamente)
            listaDetalles.Clear();
            ventasBinding.ResetBindings(false);

            // restablecer ComboBoxes y TextBoxes
            cbxCliente.SelectedIndex = -1;
            cbxCliente.Enabled = true;
            cbxProducto.SelectedIndex = -1;
            txtCantidad.Clear();
            txtVenta.Clear();

            // desactivar botones que dependen de datos
            btnFacturar.Enabled = false;

            // limpiar ReportViewer: quitar orígenes y parámetros, ocultar y refrescar
            

            // recargar comboboxes para quedar como al iniciar
            try
            {
                ClienteBL clienteBL = new ClienteBL();
                ProductoBL productoBL = new ProductoBL();

                cbxCliente.DataSource = clienteBL.ListarCl();
                cbxCliente.DisplayMember = "nombre";
                cbxCliente.ValueMember = "id_cliente";

                cbxProducto.DataSource = productoBL.Listar();
                cbxProducto.DisplayMember = "Nombre_Producto";
                cbxProducto.ValueMember = "Id_Productos";
            }
            catch
            {
                // si falla la recarga, no interrumpimos el reinicio
            }

            // asegurar que las columnas de Id estén ocultas
            HideIdColumns();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                // Confirmación previa a la eliminación
                if (dvgVenta.SelectedRows != null && dvgVenta.SelectedRows.Count > 0)
                {
                    var item = dvgVenta.SelectedRows[0].DataBoundItem as DetalleVenta;
                    string nombre = item?.NombreProducto ?? "el elemento seleccionado";
                    var confirmar = MessageBox.Show($"¿Eliminar \"{nombre}\"? Esta acción no se puede deshacer.", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (confirmar != DialogResult.Yes) return;
                }
                else if (!string.IsNullOrWhiteSpace(txtVenta.Text))
                {
                    var confirmar = MessageBox.Show($"¿Eliminar según '{txtVenta.Text}'? Esta acción no se puede deshacer.", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (confirmar != DialogResult.Yes) return;
                }
                else
                {
                    MessageBox.Show("Seleccione una fila o ingrese el Id/posición para eliminar.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                bool removed = false;

                // Si hay una fila seleccionada en el DataGridView, eliminar el elemento asociado
                if (dvgVenta.SelectedRows != null && dvgVenta.SelectedRows.Count > 0)
                {
                    var item = dvgVenta.SelectedRows[0].DataBoundItem as DetalleVenta;
                    if (item != null)
                    {
                        listaDetalles.Remove(item);
                        removed = true;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(txtVenta.Text))
                {
                    // Si no hay selección, intentar eliminar según valor en txtVenta:
                    if (int.TryParse(txtVenta.Text.Trim(), out int valor))
                    {
                        var byId = listaDetalles.FirstOrDefault(d => d.Id_Detalle == valor);
                        if (byId != null)
                        {
                            listaDetalles.Remove(byId);
                            removed = true;
                        }
                        else if (valor >= 1 && valor <= listaDetalles.Count)
                        {
                            // interpretar como posición 1-based
                            listaDetalles.RemoveAt(valor - 1);
                            removed = true;
                        }
                    }
                }

                if (removed)
                {
                    ventasBinding.ResetBindings(false);
                    btnFacturar.Enabled = listaDetalles.Any();
                    txtVenta.Clear();
                    UpdateTotalTextbox(); // <-- actualizar total después de eliminar
                }
                else
                {
                    MessageBox.Show("No se encontró una fila seleccionada ni un identificador válido en el campo. Seleccione una fila o ingrese el Id/posición en el campo.", "Eliminar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Oculta columnas de Id que aparecen como 0
        private void HideIdColumns()
        {
            try
            {
                if (dvgVenta.Columns == null) return;

                if (dvgVenta.Columns.Contains("Id_Detalle"))
                    dvgVenta.Columns["Id_Detalle"].Visible = false;

                if (dvgVenta.Columns.Contains("Id_Venta"))
                    dvgVenta.Columns["Id_Venta"].Visible = false;

                if (dvgVenta.Columns.Contains("Id_Productos"))
                {
                    // opcional: ocultar Id_Productos si no quieres verlo
                    dvgVenta.Columns["Id_Productos"].Visible = false;
                }
            }
            catch
            {
                // no interrumpir por fallo al ocultar columnas
            }
        }

        private void DvgVenta_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            HideIdColumns();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        // Reemplaza el manejador actual txtTotal_TextChanged por uno vacío para evitar recursión
        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            // Actualización del total controlada explícitamente por UpdateTotalTextbox()
        }

        // Añadir este método a la clase FormV
        private void UpdateTotalTextbox()
        {
            txtTotal.Text = listaDetalles.Sum(d => d.Subtotal).ToString("C");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtVenta_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}