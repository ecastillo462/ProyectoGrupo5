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
    public class ConsultaController
    {
        ConsultasView VistaConsulta;
        ClienteDAO clienteDAO = new ClienteDAO();
        Consulta consulta = new Consulta();
        ConsultaDAO consultaDAO = new ConsultaDAO(); 
        UsuarioDAO usuarioDAO = new UsuarioDAO();
        ServiciosDAO serviciosDAO = new ServiciosDAO(); 
        string opcion = ""; 

        public ConsultaController (ConsultasView view )
        {
            VistaConsulta = view;
            VistaConsulta.Load += VistaConsulta_Load;
            VistaConsulta.NuevoButton.Click += new EventHandler(Nuevo);
            VistaConsulta.GuardarButton.Click += new EventHandler(Guardar);
            VistaConsulta.EliminarButton.Click += new EventHandler(Eliminar);
            VistaConsulta.CancelarButton.Click += new EventHandler(Cancelar);
            VistaConsulta.ModificarButton.Click += new EventHandler(Modificar);

            VistaConsulta.UsuarioTextBox.Text = "Prueba"; 
        }

        private void Modificar(object sender, EventArgs e)
        {
            if (VistaConsulta.ConsultasDataGridView.SelectedRows.Count > 0)
            {
                opcion = "Modificar";
                HabilitarControles();
                ListarClientesYServicios(); 

                VistaConsulta.IdTextBox.Text = VistaConsulta.ConsultasDataGridView.CurrentRow.Cells["ID_CONSULTA"].Value.ToString();
                VistaConsulta.UsuarioTextBox.Text  = VistaConsulta.ConsultasDataGridView.CurrentRow.Cells["USUARIO"].Value.ToString();
                VistaConsulta.ClientesComboBox.Text = VistaConsulta.ConsultasDataGridView.CurrentRow.Cells["CLIENTE"].Value.ToString();
                VistaConsulta.ServiciosComboBox.Text = VistaConsulta.ConsultasDataGridView.CurrentRow.Cells["SERVICIO"].Value.ToString();
                VistaConsulta.FechaDateTimePicker.Value = Convert.ToDateTime(VistaConsulta.ConsultasDataGridView.CurrentRow.Cells["FECHA"].Value);
                VistaConsulta.DescripcionTextBox.Text = VistaConsulta.ConsultasDataGridView.CurrentRow.Cells["DESCRIPCION"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }

        }

        private void Guardar(object sender, EventArgs e)
        {
            
            if (VistaConsulta.ClientesComboBox.SelectedItem == null)
            {
                VistaConsulta.errorProvider1.SetError(VistaConsulta.ClientesComboBox,"Debe seleccionar un cliente");
                VistaConsulta.ClientesComboBox.Focus(); 
                return;
            }

            if (VistaConsulta.ServiciosComboBox.SelectedItem == null)
            {
                VistaConsulta.errorProvider1.SetError(VistaConsulta.ServiciosComboBox, "Debe seleccionar un servicio");
                VistaConsulta.ServiciosComboBox.Focus();
                return;
            }

            if (VistaConsulta.DescripcionTextBox.Text == "")
            {
                VistaConsulta.errorProvider1.SetError(VistaConsulta.DescripcionTextBox, "Debe colocar una descripcion");
                VistaConsulta.DescripcionTextBox.Focus();
                return;
            }

            
            try
            {
                consulta.IdUsuario = usuarioDAO.getIdUsuarioPorNombre(VistaConsulta.UsuarioTextBox.Text);
                consulta.IdCliente = clienteDAO.getIdClientePorNombre(VistaConsulta.ClientesComboBox.Text);
                consulta.IdTipoServicio = serviciosDAO.getIdTipoServicioPorNombre(VistaConsulta.ServiciosComboBox.Text);
                consulta.Fecha = VistaConsulta.FechaDateTimePicker.Value; 
                consulta.Descripcion = VistaConsulta.DescripcionTextBox.Text;

                
                if (opcion == "Nuevo")
                {
                    bool inserto = consultaDAO.InsertarConsulta(consulta);

                    if (inserto)
                    {
                        LimpiarControles();
                        DeshabilitarControles();
                        ListarConsultas();
                        MessageBox.Show("Consulta agregada con éxito");
                    }
                    else
                    {
                        MessageBox.Show("No se pudo agregar la consulta");
                    }
                }
                else if (opcion == "Modificar")
                {
                    bool modifico = false;
                    consulta.Id = Convert.ToInt32(VistaConsulta.IdTextBox.Text);
                    modifico = consultaDAO.ActualizarConsulta(consulta);
                    if (modifico)
                    {
                        DeshabilitarControles();
                        MessageBox.Show("Consulta modificada exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ListarConsultas();
                        LimpiarControles();
                    }
                    else
                    {
                        MessageBox.Show("Ocurrió un error al intentar modificar la consulta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception)
            {
                
            }
            DeshabilitarControles();
            LimpiarControles();
        }

        private void Cancelar(object sender, EventArgs e)
        {
            DeshabilitarControles();
            LimpiarControles();
        }

        private void Eliminar(object sender, EventArgs e)
        {
            if (VistaConsulta.ConsultasDataGridView.SelectedRows.Count > 0)
            {
                bool elimino = consultaDAO.EliminarConsulta(Convert.ToInt32(VistaConsulta.ConsultasDataGridView.CurrentRow.Cells["ID_CONSULTA"].Value));
                if (elimino)
                {
                    MessageBox.Show("Consulta eliminada exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarConsultas();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al intentar eliminar la consulta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            } else
            {
                MessageBox.Show("Debes seleccionar un registro"); 
            }


            DeshabilitarControles();
            LimpiarControles(); 
        }


        private void VistaConsulta_Load(object sender, EventArgs e)
        {
            LoginView loginView = new LoginView();
            VistaConsulta.UsuarioTextBox.Text = usuarioDAO.GetUsuarioPorEmail(loginView.emailUsuario.ToString()); 
            DeshabilitarControles();
            ListarConsultas(); 
        }

        private void Nuevo(object sender, EventArgs e)
        {
            opcion = "Nuevo";

            LoginView loginView = new LoginView();
            VistaConsulta.UsuarioTextBox.Text = usuarioDAO.GetUsuarioPorEmail(loginView.emailUsuario.ToString());
            ListarClientesYServicios();
            HabilitarControles(); 
        }

        private void ListarClientesYServicios()
        {
            VistaConsulta.ClientesComboBox.DataSource = clienteDAO.GetClientes();
            VistaConsulta.ClientesComboBox.DisplayMember = "CLIENTES";
            VistaConsulta.ClientesComboBox.ValueMember = "NOMBRE";

            VistaConsulta.ServiciosComboBox.DataSource = serviciosDAO.GetServicios();
            VistaConsulta.ServiciosComboBox.DisplayMember = "SERVICIOS";
            VistaConsulta.ServiciosComboBox.ValueMember = "NOMBRE";
        }

        private void ListarConsultas()
        {
            VistaConsulta.ConsultasDataGridView.DataSource = consultaDAO.GetConsultas(); 
        }

        private void HabilitarControles()
        {
            VistaConsulta.ClientesComboBox.Enabled = true;
            VistaConsulta.ServiciosComboBox.Enabled = true;
            VistaConsulta.DescripcionTextBox.Enabled = true;

            VistaConsulta.CancelarButton.Enabled = true;
            VistaConsulta.GuardarButton.Enabled = true;
            VistaConsulta.NuevoButton.Enabled = false;
            VistaConsulta.ModificarButton.Enabled = false;
            VistaConsulta.EliminarButton.Enabled = false;

        }
        private void DeshabilitarControles()
        {
            VistaConsulta.UsuarioTextBox.Enabled = false;
            VistaConsulta.ClientesComboBox.Enabled = false;
            VistaConsulta.ServiciosComboBox.Enabled = false;
            VistaConsulta.DescripcionTextBox.Enabled = false;

            VistaConsulta.CancelarButton.Enabled = false;
            VistaConsulta.GuardarButton.Enabled = false;
            VistaConsulta.NuevoButton.Enabled = true;
            VistaConsulta.ModificarButton.Enabled = true;
            VistaConsulta.EliminarButton.Enabled = true;

        }
        private void LimpiarControles()
        {
            VistaConsulta.UsuarioTextBox.Clear();
            VistaConsulta.DescripcionTextBox.Clear();
            VistaConsulta.ClientesComboBox.Text = "";
            VistaConsulta.ServiciosComboBox.Text = ""; 
            VistaConsulta.IdTextBox.Text = string.Empty;
        }
    }
}
