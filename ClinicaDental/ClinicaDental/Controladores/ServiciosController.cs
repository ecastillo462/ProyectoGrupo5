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
    public  class ServiciosController
    {
        ServiciosView vista;
        ServiciosDAO serviciosDAO = new ServiciosDAO();
        Servicios servicios = new Servicios();
        string operacion = string.Empty;
        

        public ServiciosController(ServiciosView view)
        {
            vista = view;
            vista.NuevoButton.Click += new EventHandler(Nuevo);
            vista.GuardarButton.Click += new EventHandler(Guardar);
            vista.Load += new EventHandler(Load);
            vista.ModificarButton.Click += new EventHandler(Modificar);
            vista.EliminarButton.Click += new EventHandler(Eliminar);
            vista.CancelarButton.Click += new EventHandler(Cancelar);
        }

        private void Load(object sender, EventArgs e)
        {
            ListarServicios();

        }

        private void ListarServicios()
        {
            vista.ServiciosDataGridView.DataSource = serviciosDAO.GetServicios();
        }

        private void Nuevo(object serder, EventArgs e)
        {
            HabilitarControles();
            operacion = "Nuevo";
        }

        private void Guardar(object serder, EventArgs e)
        {
            if (vista.NombreTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.NombreTextBox, "Ingrese un Servicio");
                vista.NombreTextBox.Focus();
                return;
            }
            if (vista.DescripcionTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.DescripcionTextBox, "Ingrese una Descripcion");
                vista.DescripcionTextBox.Focus();
                return;
            }
            if (vista.CostoTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.CostoTextBox, "Ingrese un Costo");
                vista.CostoTextBox.Focus();
                return;
            }

            servicios.Nombre = vista.NombreTextBox.Text;
            servicios.Descripcion = vista.DescripcionTextBox.Text;
            servicios.Costo = Convert.ToDecimal(vista.CostoTextBox.Text);
           
            if (operacion == "Nuevo")
            {
                bool inserto = serviciosDAO.InsertarNuevoServicio(servicios);

                if (inserto)
                {
                    DesabilitarControles();
                    LimpiarControles();

                    MessageBox.Show("Servicio ingresado exitosamente ", "Atención", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    ListarServicios();
                }
                else
                {
                    MessageBox.Show("No se pudo ingresar el Servicio", "Atención", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    ListarServicios();
                }
            }
            else if (operacion == "Modificar")
            {
                servicios.Id = Convert.ToInt32(vista.IdTextBox.Text);
                bool modifico = serviciosDAO.ModificarServicios(servicios);
                if (modifico)
                {
                    DesabilitarControles();
                    LimpiarControles();

                    MessageBox.Show("Servicio modificado exitosamente", "Atención", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    ListarServicios();

                }
                else
                {
                    MessageBox.Show("No se pudo modificar el Servicio", "Atención", MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    ListarServicios();
                }
            }

        }

        private void Eliminar(object serder, EventArgs e)
        {
            if (vista.ServiciosDataGridView.SelectedRows.Count > 0)
            {
                bool elimino = serviciosDAO.EliminarServicios(Convert.ToInt32(vista.ServiciosDataGridView.CurrentRow.Cells[0].Value.ToString()));

                if (elimino)
                {
                    DesabilitarControles();
                    LimpiarControles();

                    MessageBox.Show("Servicio eliminado exitosamente", "Atención", MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    ListarServicios();
                }
            }
        }

        private void Modificar(object serder, EventArgs e)
        {
            operacion = "Modificar";

            if (vista.ServiciosDataGridView.SelectedRows.Count > 0)
            {
                vista.IdTextBox.Text = vista.ServiciosDataGridView.CurrentRow.Cells["ID"].Value.ToString();
                vista.NombreTextBox.Text = vista.ServiciosDataGridView.CurrentRow.Cells["NOMBRE"].Value.ToString();
                vista.DescripcionTextBox.Text = vista.ServiciosDataGridView.CurrentRow.Cells["DESCRIPCION"].Value.ToString();
                vista.CostoTextBox.Text = vista.ServiciosDataGridView.CurrentRow.Cells["COSTO"].Value.ToString();
                HabilitarControles();
            }
        }

        private void Cancelar(object serder, EventArgs e)
        {
            LimpiarControles();
            DesabilitarControles();
        }
        private void HabilitarControles()
        {
            vista.IdTextBox.Enabled = true;
            vista.NombreTextBox.Enabled = true;
            vista.DescripcionTextBox.Enabled = true;
            vista.CostoTextBox.Enabled = true;

            vista.GuardarButton.Enabled = true;
            vista.CancelarButton.Enabled = true;
            vista.ModificarButton.Enabled = false;
            vista.NuevoButton.Enabled = false;
            vista.EliminarButton.Enabled = false;
        }
        private void DesabilitarControles()
        {
            vista.IdTextBox.Enabled = false;
            vista.NombreTextBox.Enabled = false;
            vista.DescripcionTextBox.Enabled = false;
            vista.CostoTextBox.Enabled = false;

            vista.GuardarButton.Enabled = false;
            vista.CancelarButton.Enabled = false;
            vista.ModificarButton.Enabled = true;
            vista.NuevoButton.Enabled = true;
            vista.EliminarButton.Enabled = true;
        }
        private void LimpiarControles()
        {
            vista.IdTextBox.Clear();
            vista.NombreTextBox.Clear();
            vista.DescripcionTextBox.Clear();
            vista.CostoTextBox.Clear();
        }



    }
}
