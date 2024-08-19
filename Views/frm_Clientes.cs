
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
    public partial class frm_Clientes : Form
    {
        private ClientesController clientesController = new ClientesController();
        private int idCliente = 0;
        public frm_Clientes()
        {
            InitializeComponent();
        }

        private void frm_Clientes_Load(object sender, EventArgs e)
        {
            cargaLista();
        }
        private void cargaLista()
        {
            try
            {
                List<ClientesModel> clientes = clientesController.todos();
                lst_Clientes.Items.Clear();
                foreach (var cliente in clientes)
                {
                    lst_Clientes.Items.Add(FormatearCliente(cliente));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los clientes: " + ex.Message);
            }
        }
        private string FormatearCliente(ClientesModel cliente)
        {
            return $"{cliente.IdCliente} - {cliente.Nombres} {cliente.Apellidos} - {cliente.Correo} - {cliente.Direccion} - {cliente.Telefono}";
        }
        private void lst_Clientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_Clientes.SelectedItem != null)
            {
                var itemSeleccionado = lst_Clientes.SelectedItem.ToString();
                var cliente = clientesController.todos().Find(c => FormatearCliente(c) == itemSeleccionado);
                if (cliente != null)
                {
                    idCliente = cliente.IdCliente;
                    txt_Nombres.Text = cliente.Nombres;
                    txt_Apellidos.Text = cliente.Apellidos;
                    txt_Correo.Text = cliente.Correo;
                    txt_Direccion.Text = cliente.Direccion;
                    txt_Telefono.Text = cliente.Telefono;

                    txt_Nombres.Enabled = true;
                    txt_Apellidos.Enabled = true;
                    txt_Correo.Enabled = true;
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
            if (string.IsNullOrWhiteSpace(txt_Nombres.Text) ||
                string.IsNullOrWhiteSpace(txt_Apellidos.Text) ||
                string.IsNullOrWhiteSpace(txt_Correo.Text) ||
                string.IsNullOrWhiteSpace(txt_Direccion.Text) ||
                string.IsNullOrWhiteSpace(txt_Telefono.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }

            ClientesModel cliente = new ClientesModel
            {
                IdCliente = idCliente,
                Nombres = txt_Nombres.Text,
                Apellidos = txt_Apellidos.Text,
                Correo = txt_Correo.Text,
                Direccion = txt_Direccion.Text,
                Telefono = txt_Telefono.Text
            };

            try
            {
                string respuesta;
                if (idCliente == 0)
                {
                    respuesta = clientesController.insertar(cliente);
                }
                else
                {
                    respuesta = clientesController.actualizar(cliente);
                }

                if (respuesta == "ok")
                {
                    MessageBox.Show("Cliente guardado con exito");
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
            if (idCliente == 0)
            {
                MessageBox.Show("Seleccione un cliente para editar");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Nombres.Text) ||
                string.IsNullOrWhiteSpace(txt_Apellidos.Text) ||
                string.IsNullOrWhiteSpace(txt_Correo.Text) ||
                string.IsNullOrWhiteSpace(txt_Direccion.Text) ||
                string.IsNullOrWhiteSpace(txt_Telefono.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }

            ClientesModel cliente = new ClientesModel
            {
                IdCliente = idCliente,
                Nombres = txt_Nombres.Text,
                Apellidos = txt_Apellidos.Text,
                Correo = txt_Correo.Text,
                Direccion = txt_Direccion.Text,
                Telefono = txt_Telefono.Text
            };
            string respuesta = clientesController.actualizar(cliente);

            if (respuesta == "ok")
            {
                MessageBox.Show("Cliente actualizado con exito");
                btn_Cancelar_Click(null, null);
                cargaLista();
            }
            else
            {
                MessageBox.Show("Error al actualizar el cliente: " + respuesta);
            }
        }
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (idCliente == 0)
            {
                MessageBox.Show("Seleccione un cliente para eliminar");
                return;
            }

            DialogResult result = MessageBox.Show("¿Desea eliminar el cliente?", "Confirmacion", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                ClientesModel cliente = new ClientesModel { IdCliente = idCliente };
                string respuesta = clientesController.eliminar(cliente);

                if (respuesta == "ok")
                {
                    MessageBox.Show("Cliente eliminado con exito");
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
            idCliente = 0;
        }
        private void btn_Salir_Click(object sender, EventArgs e)
        {
            idCliente = 0;
            this.Close();
        }
        private void LimpiarCampos()
        {
            txt_Nombres.Text = "";
            txt_Apellidos.Text = "";
            txt_Correo.Text = "";
            txt_Direccion.Text = "";
            txt_Telefono.Text = "";
        }

        private void DeshabilitarCampos()
        {
            txt_Nombres.Enabled = false;
            txt_Apellidos.Enabled = false;
            txt_Correo.Enabled = false;
            txt_Direccion.Enabled = false;
            txt_Telefono.Enabled = false;

            btn_Eliminar.Enabled = false;
            btn_Editar.Enabled = false;
        }

        private void lst_Clientes_DoubleClick(object sender, EventArgs e)
        {
            idCliente = Convert.ToInt16(lst_Clientes.SelectedValue);
            txt_Nombres.Text = lst_Clientes.GetItemText(lst_Clientes.SelectedItem);
            txt_Apellidos.Text = lst_Clientes.GetItemText(lst_Clientes.SelectedItem);
            txt_Direccion.Text = lst_Clientes.GetItemText(lst_Clientes.SelectedItem);
            txt_Telefono.Text = lst_Clientes.GetItemText(lst_Clientes.SelectedItem);
            txt_Correo.Text = lst_Clientes.GetItemText(lst_Clientes.SelectedItem);
        }
    }
}

