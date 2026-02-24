using CapaEntidades;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CapaPresentacion
{
    public partial class FormC : Form
    {
        private ClienteBL bl = new ClienteBL();
        private int idSeleccionado = 0;
        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("La dirección es obligatoria.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDireccion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }
            else if (!long.TryParse(txtTelefono.Text, out _))
            {
                MessageBox.Show("El teléfono debe ser numérico.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("El correo es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }
            else if (!txtCorreo.Text.Contains("@"))
            {
                MessageBox.Show("El correo no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCorreo.Focus();
                return false;
            }

            return true;
        }

        public FormC()
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void FormC_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = bl.ListarCl();
            dataGridView1.Columns["id_cliente"].Visible = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (ValidarCampos())
            { 
              
              ClienteBL clienteBL = new ClienteBL(); Cliente cliente = new Cliente { 
                  nombre = txtNombre.Text, 
                  direccion = txtDireccion.Text, 
                  telefono = txtTelefono.Text, 
                  correo = txtCorreo.Text 
              }; 
                clienteBL.AgregarCl(cliente); 
                dataGridView1.DataSource = clienteBL.ListarCl();
                MessageBox.Show("Cliente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }

        private void Limpiar()
        {
            txtNombre.Text = "";
            txtDireccion.Text = "";
            txtTelefono.Text = "";
            txtCliente.Text = "";
            txtCorreo.Text = "";
            idSeleccionado = 0;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int idCliente = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id_cliente"].Value);

                var confirmacion = MessageBox.Show("¿Seguro que deseas eliminar este cliente?",
                                                   "Confirmar eliminación",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);

                if (confirmacion == DialogResult.Yes)
                {
                    ClienteBL clienteBL = new ClienteBL();

                    // Crear objeto Cliente con el ID seleccionado
                    Cliente cliente = new Cliente { id_cliente = idCliente };

                    // Pasar el objeto al método Eliminar
                    clienteBL.EliminarCl(cliente);

                    // Refrescar el DataGridView
                    dataGridView1.DataSource = clienteBL.ListarCl();
                }
            }
            else
            {
                MessageBox.Show("Selecciona un cliente primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                DataGridViewRow fila = dataGridView1.CurrentRow;
                txtNombre.Text = fila.Cells["nombre"].Value.ToString();
                txtDireccion.Text = fila.Cells["direccion"].Value.ToString();
                txtTelefono.Text = fila.Cells["telefono"].Value.ToString();
                txtCorreo.Text = fila.Cells["correo"].Value.ToString();
                txtCliente.Text = fila.Cells["id_cliente"].Value.ToString();
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}