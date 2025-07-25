using Fergeda_2023.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.General
{
    public partial class MensageC : Form
    {
        private estiloVentana est;
        public MensageC()
        {
            est = new estiloVentana();
            InitializeComponent();
            est.estilo(this);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
        public void cargarEl(string img, string aviso, string mensage)
        {
            lbl_Aviso.Text = aviso;
            lbl_cuerpo.Text = mensage;
            pictureBox1.Image = Image.FromFile(@"" + img + ".png");
        }
    }
}
