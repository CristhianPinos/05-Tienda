using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _05_Tienda.Views;

namespace _05_Tienda
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void paisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Pais frm_Pais = new frm_Pais();
            frm_Pais.ShowDialog();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Clientes frm_Clientes = new frm_Clientes();
            frm_Clientes.ShowDialog();
        }

        private void proveedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Proveedores frm_Proveedores = new frm_Proveedores();
            frm_Proveedores.ShowDialog();
        }

        private void ciudadesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_Ciudad frm_Ciudad = new frm_Ciudad();
            frm_Ciudad.ShowDialog();
        }
    }
}
