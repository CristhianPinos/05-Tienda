
namespace _05_Tienda.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using _05_Tienda.Controllers;
    using _05_Tienda.Models;
    public partial class frm_Proveedores : Form
    {
        private ProveedoresController proveedoresController = new ProveedoresController();
        private int idProveedor = 0;
        public frm_Proveedores()
        {
            InitializeComponent();
        }
        private void frm_Proveedores_Load(object sender, EventArgs e)
        {
            cargaLista();
        }
        private void cargaLista()
        {
            try
            {
                List<ProveedoresModel> proveedores = proveedoresController.todos();
                lst_Proveedores.Items.Clear();
                foreach (var proveedor in proveedores)
                {
                    lst_Proveedores.Items.Add(FormatearProveedor(proveedor));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los proveedores: " + ex.Message);
            }
        }
        private string FormatearProveedor(ProveedoresModel proveedor)
        {
            return $"{proveedor.IdProveedor} - {proveedor.NombreEmpresa} - {proveedor.Direccion} - {proveedor.Telefono}";
        }
        private void lst_Proveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_Proveedores.SelectedItem != null)
            {
                var itemSeleccionado = lst_Proveedores.SelectedItem.ToString();
                var proveedor = proveedoresController.todos().Find(p => FormatearProveedor(p) == itemSeleccionado);
                if (proveedor != null)
                {
                    idProveedor = proveedor.IdProveedor;
                    txt_Nombre.Text = proveedor.NombreEmpresa;
                    txt_Direccion.Text = proveedor.Direccion;
                    txt_Telefono.Text = proveedor.Telefono;

                    txt_Nombre.Enabled = true;
                    txt_Direccion.Enabled = true;
                    txt_Telefono.Enabled = true;

                    btn_Eliminar.Enabled = true;
                    btn_Editar.Enabled = true;
                }
            }
            else
            {
                LimpiarCampos();
                DeshabilitarCampos();
            }
        }
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_Nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_Direccion.Text) ||
                string.IsNullOrWhiteSpace(txt_Telefono.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }

            ProveedoresModel proveedor = new ProveedoresModel
            {
                IdProveedor = idProveedor,
                NombreEmpresa = txt_Nombre.Text,
                Direccion = txt_Direccion.Text,
                Telefono = txt_Telefono.Text
            };

            try
            {
                string respuesta;
                if (idProveedor == 0)
                {
                    respuesta = proveedoresController.insertar(proveedor);
                }
                else
                {
                    respuesta = proveedoresController.actualizar(proveedor);
                }

                if (respuesta == "ok")
                {
                    MessageBox.Show("Proveedor guardado con exito");
                    btn_Cancelar_Click(null, null);
                    cargaLista();
                }
                else
                {
                    MessageBox.Show("Error: " + respuesta);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_Editar_Click(object sender, EventArgs e)
        {
            if (idProveedor == 0)
            {
                MessageBox.Show("Seleccione un proveedor para editar");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Nombre.Text) ||
                string.IsNullOrWhiteSpace(txt_Direccion.Text) ||
                string.IsNullOrWhiteSpace(txt_Telefono.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }

            ProveedoresModel proveedor = new ProveedoresModel
            {
                IdProveedor = idProveedor,
                NombreEmpresa = txt_Nombre.Text,
                Direccion = txt_Direccion.Text,
                Telefono = txt_Telefono.Text
            };
            string respuesta = proveedoresController.actualizar(proveedor);

            if (respuesta == "ok")
            {
                MessageBox.Show("Proveedor actualizado con exito");
                btn_Cancelar_Click(null, null);
                cargaLista();
            }
            else
            {
                MessageBox.Show("Error al actualizar el proveedor: " + respuesta);
            }
        }
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (idProveedor == 0)
            {
                MessageBox.Show("Seleccione un proveedor para eliminar");
                return;
            }

            DialogResult result = MessageBox.Show("¿Desea eliminar el proveedor?", "Confirmacion", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ProveedoresModel proveedor = new ProveedoresModel { IdProveedor = idProveedor };
                string respuesta = proveedoresController.eliminar(proveedor);

                if (respuesta == "ok")
                {
                    MessageBox.Show("Proveedor eliminado con exito");
                    btn_Cancelar_Click(null, null);
                    cargaLista();
                }
                else
                {
                    MessageBox.Show("Error: " + respuesta);
                }
            }
        }
        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
            DeshabilitarCampos();
            idProveedor = 0;
        }
        private void btn_Salir_Click(object sender, EventArgs e)
        {
            idProveedor = 0;
            this.Close();
        }
        private void LimpiarCampos()
        {
            txt_Nombre.Text = "";
            txt_Direccion.Text = "";
            txt_Telefono.Text = "";
        }
        private void DeshabilitarCampos()
        {
            txt_Nombre.Enabled = false;
            txt_Direccion.Enabled = false;
            txt_Telefono.Enabled = false;

            btn_Eliminar.Enabled = false;
            btn_Editar.Enabled = false;
        }
        private void lst_Proveedores_DoubleClick(object sender, EventArgs e)
        {
            idProveedor = Convert.ToInt16(lst_Proveedores.SelectedValue);
            txt_Nombre.Text = lst_Proveedores.GetItemText(lst_Proveedores.SelectedItem);
            txt_Direccion.Text = lst_Proveedores.GetItemText(lst_Proveedores.SelectedItem);
            txt_Telefono.Text = lst_Proveedores.GetItemText(lst_Proveedores.SelectedItem);
        }
    }
}
