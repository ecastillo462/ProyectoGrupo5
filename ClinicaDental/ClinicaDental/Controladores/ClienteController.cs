using ClinicaDental.Modelos.DAO;
using ClinicaDental.Modelos.Entidades;
using ClinicaDental.Vistas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClinicaDental.Controladores
{
    public class ClienteController
    {
        ClientesView clienteVista;
        ClienteDAO clientDAO = new ClienteDAO();
        Cliente client = new Cliente();
        string opcion = string.Empty;

        public ClienteController(ClientesView view)
        {
            clienteVista = view;
            clienteVista.NuevoButton.Click += new EventHandler(Nuevo);
            clienteVista.IngresarButton.Click += new EventHandler(InsertarCliente);
            clienteVista.ModificarButton.Click += new EventHandler(ModificarCliente);
            clienteVista.Load += new EventHandler(Load);
            clienteVista.EliminarButton.Click += new EventHandler(EliminarCliente);
            clienteVista.CancelarButton.Click += new EventHandler(Cancelar);
        }


        private void InsertarCliente(object sender, EventArgs e)
        {
            if (clienteVista.NombreTextBox.Text == "")
            {
                clienteVista.errorProvider1.SetError(clienteVista.NombreTextBox, "Ingrese un nombre");
                clienteVista.NombreTextBox.Focus();
                return;
            }
            if (clienteVista.EdadTextBox.Text == "")
            {
                clienteVista.errorProvider1.SetError(clienteVista.EdadTextBox, "Ingrese una edad");
                clienteVista.EdadTextBox.Focus();
                return;
            }
            if (clienteVista.GeneroComboBox.Text == "")
            {
                clienteVista.errorProvider1.SetError(clienteVista.GeneroComboBox, "Seleccione un género");
                clienteVista.GeneroComboBox.Focus();
                return;
            }
            if (clienteVista.TelefonoTextBox.Text == "")
            {
                clienteVista.errorProvider1.SetError(clienteVista.TelefonoTextBox, "Ingrese un teléfono");
                clienteVista.TelefonoTextBox.Focus();
                return;
            }
            if (clienteVista.EmailTextBox.Text == "")
            {
                clienteVista.errorProvider1.SetError(clienteVista.EmailTextBox, "Ingrese un dirección de email");
                clienteVista.EmailTextBox.Focus();
                return;
            }

            try
            {
                client.Nombre = clienteVista.NombreTextBox.Text;
                client.Edad = Convert.ToInt32(clienteVista.EdadTextBox.Text);
                client.Genero = clienteVista.GeneroComboBox.Text;
                client.Telefono = clienteVista.TelefonoTextBox.Text;
                client.Email = clienteVista.EmailTextBox.Text;

                if (opcion == "Nuevo")
                {
                    bool inserto = clientDAO.InsertarCliente(client);

                    if (inserto)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        ListarClientes(); 
                        MessageBox.Show("Paciente ingresado con éxito");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo ingresar el paciente");
                    }
                }
                else if (opcion == "Modificar")
                {
                    client.Id = Convert.ToInt32(clienteVista.IdTextBox.Text);
                    bool modifico = clientDAO.ActualizarCliente(client);
                    if (modifico)
                    {
                        DeshabilitarControles();
                        MessageBox.Show("Paciente modificado exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarClientes();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("El (La) paciente no se pudo modificar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
            }


        }
        private void ModificarCliente(object sender, EventArgs e)
        {
            if (clienteVista.ClientesDataGridView.SelectedRows.Count > 0)
            {
                opcion = "Modificar";
                HabilitarControles();

                clienteVista.IdTextBox.Text = clienteVista.ClientesDataGridView.CurrentRow.Cells["ID"].Value.ToString();
                clienteVista.NombreTextBox.Text = clienteVista.ClientesDataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                clienteVista.GeneroComboBox.Text = clienteVista.ClientesDataGridView.CurrentRow.Cells["GENERO"].Value.ToString();
                clienteVista.TelefonoTextBox.Text = clienteVista.ClientesDataGridView.CurrentRow.Cells["TELEFONO"].Value.ToString();
                clienteVista.EmailTextBox.Text = clienteVista.ClientesDataGridView.CurrentRow.Cells["EMAIL"].Value.ToString();
                clienteVista.EdadTextBox.Text = clienteVista.ClientesDataGridView.CurrentRow.Cells["EDAD"].Value.ToString();

            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }



        }
        private void EliminarCliente(object sender, EventArgs e)
        {
            if (clienteVista.ClientesDataGridView.SelectedRows.Count > 0)
            {
                bool elimino = clientDAO.EliminarCliente(Convert.ToInt32(clienteVista.ClientesDataGridView.CurrentRow.Cells["ID"].Value));
                if (elimino)
                {
                    MessageBox.Show("Paciente eliminado exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarClientes();
                    LimpiarControles(); 
                }
                else
                {
                    MessageBox.Show("El (La) paciente no se pudo eliminar", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void Nuevo(object sender, EventArgs e)
        {
            LimpiarControles();
            HabilitarControles();
            opcion = "Nuevo";
        }
        private void Load(object sender, EventArgs e)
        {
            DeshabilitarControles();
            ListarClientes();
        }
        private void Cancelar(object sender, EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
            client = null;
        }
        private void ListarClientes()
        {
            clienteVista.ClientesDataGridView.DataSource = clientDAO.GetClientes();
        }

        private void LimpiarControles()
        {
            clienteVista.NombreTextBox.Clear();
            clienteVista.EdadTextBox.Clear();
            clienteVista.GeneroComboBox.Text = string.Empty;
            clienteVista.TelefonoTextBox.Clear();
            clienteVista.EmailTextBox.Clear();
        }
        private void HabilitarControles()
        {
            clienteVista.NombreTextBox.Enabled = true;
            clienteVista.EdadTextBox.Enabled = true;
            clienteVista.GeneroComboBox.Enabled = true;
            clienteVista.TelefonoTextBox.Enabled = true;
            clienteVista.EmailTextBox.Enabled = true;

            clienteVista.IngresarButton.Enabled = true;
            clienteVista.CancelarButton.Enabled = true;
            clienteVista.ModificarButton.Enabled = false;
            clienteVista.NuevoButton.Enabled = false;
        }
        private void DeshabilitarControles()
        {
            clienteVista.NombreTextBox.Enabled = false;
            clienteVista.EdadTextBox.Enabled = false;
            clienteVista.GeneroComboBox.Enabled = false;
            clienteVista.TelefonoTextBox.Enabled = false;
            clienteVista.EmailTextBox.Enabled = false;

            clienteVista.IngresarButton.Enabled = false;
            clienteVista.CancelarButton.Enabled = false;
            clienteVista.ModificarButton.Enabled = true;
            clienteVista.NuevoButton.Enabled = true;
        }
        private void LimpiarControles(object sender, EventArgs e)
        {
            clienteVista.NombreTextBox.Clear();
            clienteVista.EdadTextBox.Clear();
            clienteVista.GeneroComboBox.Text = string.Empty;
            clienteVista.TelefonoTextBox.Clear();
            clienteVista.EmailTextBox.Clear();
            clienteVista.IdTextBox.Text = string.Empty;
        }
    }
}
