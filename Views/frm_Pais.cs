
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

    public partial class frm_Pais : Form
    {
        PaisesController paisesController = new PaisesController();
        public int codigoPais = 0;
        public frm_Pais()
        {
            InitializeComponent();
        }

        private void frm_Pais_Load(object sender, EventArgs e)
        {
            cargaLista();
        }
        public void cargaLista()
        {
            lst_Paises.Items.Clear();
            lst_Paises.DataSource = paisesController.todos();
            lst_Paises.DisplayMember = "Detalle"; //muestra el texto en la lista        
            lst_Paises.ValueMember = "IdPais";
        }

        private void btn_Salir_Click(object sender, EventArgs e)
        {
            //txt_Detalle.Text = "";
            //lst_Paises.SelectedIndex = -1;
            codigoPais = 0;
            this.Close();
        }
        private void btn_Cancelar_Click(object sender, EventArgs e)
        {
            txt_Detalle.Text = "";
            codigoPais = 0;
        }
        private void btn_Guardar_Click(object sender, EventArgs e)
        {
            string respuesta = "";
            PaisModel pais = new PaisModel
            {
                IdPais = codigoPais,
                Detalle = txt_Detalle.Text
            };
            try
            {
                if (codigoPais == 0)
                {
                    respuesta = paisesController.insertar(pais);
                }
                else
                {
                    respuesta = paisesController.actualziar(pais);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Se guardo con exito");
            codigoPais = 0;
            txt_Detalle.Enabled = false;
            txt_Detalle.Text = "";
        }

        private void lst_Paises_SelectedIndexChanged(object sender, EventArgs e)
        {
            //codigoPais = Convert.ToInt32(lst_Paises.SelectedValue.ToString());
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            PaisModel paisModel = new PaisModel { IdPais = codigoPais };
            DialogResult result = MessageBox.Show("Desea Eliminar al pais", "Formulario de Paises", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                if (paisesController.eliminar(paisModel) == "ok")
                {
                    MessageBox.Show("Se elimino con exito");
                }
                else
                {
                    MessageBox.Show("Ocurrio un error al eliminar");
                }
            }
            else
            {
                MessageBox.Show("El usuario cancelo la eliminacion");

            }
        }

        private void btn_editar_Click(object sender, EventArgs e)
        {
            txt_Detalle.Enabled = true;
            codigoPais = Convert.ToInt16(lst_Paises.SelectedValue);
        }

        private void lst_Paises_DoubleClick(object sender, EventArgs e)
        {
            codigoPais = Convert.ToInt16(lst_Paises.SelectedValue);
            txt_Detalle.Text = lst_Paises.GetItemText(lst_Paises.SelectedItem);
        }
    }
}
