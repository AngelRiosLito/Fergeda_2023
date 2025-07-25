using Fergeda_2023.Clases;
using Fergeda_2023.Compras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Mantenimiento_Edificio
{
    public partial class MenuMantoEdificio : Form
    {
        int empleado;
        int apli = 7;
        private estiloVentana est;
        public MenuMantoEdificio(int empleado)
        {
            this.empleado = empleado;
            est = new estiloVentana();
            InitializeComponent();
            est.estilo(this);
        }

        private void bunifuImageButton16_Click(object sender, EventArgs e)
        {
            new RequisicionCompra(empleado).Show();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            new Info().cerrarConex("" + empleado, apli); Application.Exit();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            new Info().cerrarConex("" + empleado, apli);
            Form1 n = new Form1();
            n.Show();
            Close();
        }
    }
}
