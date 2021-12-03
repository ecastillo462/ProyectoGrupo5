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
    public class FacturaController
    {
        FacturaView vista;
        FacturaDAO facturaDAO = new FacturaDAO();
        ConsultaDAO consultaDAO = new ConsultaDAO();
        Factura factura = new Factura();
        string operacion = string.Empty;
        ConsultasView vistaConsulta;
        public FacturaController(FacturaView view)
        {
            vista = view;
            vista.GuardarButton.Click += new EventHandler(Guardar);
            vista.Load += new EventHandler(Load);
            vista.ModificarButton.Click += new EventHandler(Modificar);
            vista.EliminarButton.Click += new EventHandler(Eliminar);
            vista.CancelarButton.Click += new EventHandler(Cancelar);
            vista.DescuentoTextBox.TextChanged += DescuentoTextBox_TextChanged;
        }

        private void Cancelar(object sender, EventArgs e)
        {
            LimpiarControles();
            DeshabilitarControles();
        }
        private void DescuentoTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToDecimal(vista.DescuentoTextBox.Text);
            }
            catch
            {
                vista.errorProvider1.SetError(vista.DescuentoTextBox, "Descuento incorrecto");
                vista.DescuentoTextBox.Focus();
                return;
            }

            if (vista.DescuentoTextBox.Text != "" && Convert.ToDecimal(vista.DescuentoTextBox.Text) <= Convert.ToDecimal(vista.SubTotalTextBox.Text))
            {
                vista.TotalTextBox.Text = ((Convert.ToDecimal(vista.SubTotalTextBox.Text) + Convert.ToDecimal(vista.IsvTextBox.Text)) - Convert.ToDecimal(vista.DescuentoTextBox.Text)).ToString();
            }
        }
        public FacturaController(ConsultasView view)
        {
            vistaConsulta = view;
        }
        private void Load(object sender, EventArgs e)
        {
            ListarFacturas(); 
            HabilitarControles();
            vista.IsvTextBox.Text = Convert.ToString(Convert.ToDecimal(vista.SubTotalTextBox.Text) * 0.15M);
            operacion = "Nuevo";
        }
        private void Guardar(object serder, EventArgs e)
        {
            if (vista.DescuentoTextBox.Text == "")
            {
                vista.errorProvider1.SetError(vista.DescuentoTextBox, "Ingrese el descuento");
                vista.DescuentoTextBox.Focus();
                return;
            }
            if (Convert.ToDecimal(vista.DescuentoTextBox.Text) > Convert.ToDecimal(vista.SubTotalTextBox.Text))
            {
                vista.errorProvider1.SetError(vista.DescuentoTextBox, "El descuento debe ser menor que el subtotal ");
                vista.DescuentoTextBox.Focus();
                return;
            }

            if (facturaDAO.ValidarFactura(Convert.ToInt32(vista.IdConsultaTextBox.Text)) && operacion != "Modificar")
            {
                MessageBox.Show("No puedes volver a guardar una factura ya existente, solamente la puedes modificar");
                return; 
            }

            factura.SubTotal = Convert.ToDecimal(vista.SubTotalTextBox.Text);
            factura.IdConsulta = Convert.ToInt32(vista.IdConsultaTextBox.Text);
            factura.Descuento = Convert.ToDecimal( vista.DescuentoTextBox.Text);
            factura.ISV = Convert.ToDecimal(vista.IsvTextBox.Text);
            factura.Total = Convert.ToDecimal(vista.TotalTextBox.Text);

            if (operacion == "Nuevo")
            {
                bool inserto = facturaDAO.CrearFactura(factura);

                if (inserto)
                {
                    vista.TotalTextBox.Text = factura.Total.ToString();
                    LimpiarControles();
                    DeshabilitarControles();
                    ListarFacturas();
                    MessageBox.Show("Factura agregada con éxito");
                }
                else
                {
                    MessageBox.Show("No se pudo agregar la factura");
                }
            }
            else if (operacion == "Modificar")
            {
                bool modifico = false;
                factura.Id = Convert.ToInt32(vista.IdTextBox.Text);
                modifico = facturaDAO.ActualizarFactura(factura);
                if (modifico)
                {
                    DeshabilitarControles();
                    MessageBox.Show("Factura modificada exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarFacturas();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al intentar modificar la factura", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        private void Modificar(object serder, EventArgs e)
        {
            operacion = "Modificar";
            if (vista.FacturasDataGridView.SelectedRows.Count > 0)
            {
                vista.IdTextBox.Text = vista.FacturasDataGridView.CurrentRow.Cells["ID"].Value.ToString();
                vista.IdConsultaTextBox.Text = vista.FacturasDataGridView.CurrentRow.Cells["ID_CONSULTA"].Value.ToString();
                vista.SubTotalTextBox.Text = vista.FacturasDataGridView.CurrentRow.Cells["SUBTOTAL"].Value.ToString();
                vista.IsvTextBox.Text = vista.FacturasDataGridView.CurrentRow.Cells["ISV"].Value.ToString();
                vista.DescuentoTextBox.Text = vista.FacturasDataGridView.CurrentRow.Cells["DESCUENTO"].Value.ToString();
                HabilitarControles();
            }
        }
        private void Eliminar(object sender, EventArgs e)
        {
            if (vista.FacturasDataGridView.SelectedRows.Count > 0)
            {
                bool elimino = facturaDAO.EliminarFactura(Convert.ToInt32(vista.FacturasDataGridView.CurrentRow.Cells["ID"].Value));
                if (elimino)
                {
                    MessageBox.Show("Factura eliminada exitosamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListarFacturas();
                    LimpiarControles();
                }
                else
                {
                    MessageBox.Show("Ocurrió un error al intentar eliminar la Factura", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debes seleccionar un registro");
            }
            DeshabilitarControles();
            LimpiarControles();
        }
        private void ListarFacturas()
        {
            vista.FacturasDataGridView.DataSource = facturaDAO.GetFacturas();
        }
        private void HabilitarControles()
        {
            vista.DescuentoTextBox.Enabled = true;

            vista.CancelarButton.Enabled = true;
            vista.GuardarButton.Enabled = true;
                        
            vista.ModificarButton.Enabled = false;
            vista.EliminarButton.Enabled = false;

        }
        private void DeshabilitarControles()
        {
            vista.DescuentoTextBox.Enabled = false;

            vista.CancelarButton.Enabled = false;
            vista.GuardarButton.Enabled = false;

            vista.ModificarButton.Enabled = true;
            vista.EliminarButton.Enabled = true;

        }
        private void LimpiarControles()
        {
            vista.IdTextBox.Clear();
            vista.IdConsultaTextBox.Clear();
            vista.SubTotalTextBox.Clear();
            vista.DescuentoTextBox.Clear();
            vista.IsvTextBox.Clear();
            vista.TotalTextBox.Clear();
            vista.errorProvider1.Clear(); 
        }
    }
}
