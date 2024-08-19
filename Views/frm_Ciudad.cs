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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _05_Tienda.Views
{
    public partial class frm_Ciudad : Form
    {
        private CiudadesController ciudadController = new CiudadesController();
        private int idCiudad = 0;
        public frm_Ciudad()
        {
            InitializeComponent();
        }
        private void frm_Ciudad_Load(object sender, EventArgs e)
        {
            cargaLista();
        }
        private void cargaLista()
        {
            try
            {
                List<CiudadModel> ciudades = ciudadController.todos();
                lst_Ciudades.Items.Clear();
                foreach (var ciudad in ciudades)
                {
                    lst_Ciudades.Items.Add(FormatearCiudad(ciudad));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las ciudades: " + ex.Message);
            }
        }
        private string FormatearCiudad(CiudadModel ciudad)
        {
            return $"{ciudad.IdCiudad} - {ciudad.Detalle} - {ciudad.IdPais}";
        }
        private void lst_Ciudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_Ciudades.SelectedItem != null)
            {
                var itemSeleccionado = lst_Ciudades.SelectedItem.ToString();
                var ciudad = ciudadController.todos().Find(c => FormatearCiudad(c) == itemSeleccionado);
                if (ciudad != null)
                {
                    idCiudad = ciudad.IdCiudad;
                    txt_Detalle.Text = ciudad.Detalle;
                    txt_IdPais.Text = ciudad.IdPais.ToString();

                    txt_Detalle.Enabled = true;
                    txt_IdPais.Enabled = true;

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
            if (string.IsNullOrWhiteSpace(txt_Detalle.Text) || string.IsNullOrWhiteSpace(txt_IdPais.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }

            CiudadModel ciudad = new CiudadModel
            {
                IdCiudad = idCiudad,
                Detalle = txt_Detalle.Text,
                IdPais = int.Parse(txt_IdPais.Text)
            };

            try
            {
                string respuesta;
                if (idCiudad == 0)
                {
                    respuesta = ciudadController.insertar(ciudad);
                }
                else
                {
                    respuesta = ciudadController.actualizar(ciudad);
                }

                if (respuesta == "ok")
                {
                    MessageBox.Show("Ciudad guardada con exito");
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
            if (idCiudad == 0)
            {
                MessageBox.Show("Seleccione una ciudad para editar");
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_Detalle.Text) || string.IsNullOrWhiteSpace(txt_IdPais.Text))
            {
                MessageBox.Show("Por favor, rellene todos los campos");
                return;
            }

            CiudadModel ciudad = new CiudadModel
            {
                IdCiudad = idCiudad,
                Detalle = txt_Detalle.Text,
                IdPais = int.Parse(txt_IdPais.Text)
            };
            string respuesta = ciudadController.actualizar(ciudad);

            if (respuesta == "ok")
            {
                MessageBox.Show("Ciudad actualizada con exito");
                btn_Cancelar_Click(null, null);
                cargaLista();
            }
            else
            {
                MessageBox.Show("Error al actualizar la ciudad: " + respuesta);
            }
        }
        private void btn_Eliminar_Click(object sender, EventArgs e)
        {
            if (idCiudad == 0)
            {
                MessageBox.Show("Seleccione una ciudad para eliminar");
                return;
            }

            DialogResult result = MessageBox.Show("¿Desea eliminar la ciudad?", "Confirmacion", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                CiudadModel ciudad = new CiudadModel { IdCiudad = idCiudad };
                string respuesta = ciudadController.eliminar(ciudad);

                if (respuesta == "ok")
                {
                    MessageBox.Show("Ciudad eliminada con exito");
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
            idCiudad = 0;
        }
        private void btn_Salir_Click(object sender, EventArgs e)
        {
            idCiudad = 0;
            this.Close();
        }
        private void LimpiarCampos()
        {
            txt_Detalle.Text = "";
            txt_IdPais.Text = "";
        }
        private void DeshabilitarCampos()
        {
            txt_Detalle.Enabled = false;
            txt_IdPais.Enabled = false;

            btn_Eliminar.Enabled = false;
            btn_Editar.Enabled = false;
        }
        private void lst_Ciudades_DoubleClick(object sender, EventArgs e)
        {
            if (lst_Ciudades.SelectedItem != null)
            {
                var ciudadSeleccionada = (CiudadModel)lst_Ciudades.SelectedItem;
                idCiudad = ciudadSeleccionada.IdCiudad;
                txt_Detalle.Text = ciudadSeleccionada.Detalle;
                txt_IdPais.Text = ciudadSeleccionada.IdPais.ToString();
            }
        }
    }
}
