using Fergeda_2023.Clases;
using Fergeda_2023.Compras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Finanzas
{
    public partial class MenuFinanzas : Form
    {
        int apli = 4,empleado;
        private estiloVentana est;
        
        public MenuFinanzas(int empleado)
        {
            est = new estiloVentana();
            this.empleado = empleado;
            InitializeComponent();
            est.estilo(this);
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            new ConsultasCompras(empleado, apli).Show();
            Close();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            new Info().cerrarConex("" + empleado, apli);
            Form1 n = new Form1();
            n.Show();
            Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            new Info().cerrarConex("" + empleado, apli); Application.Exit();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void lbl_Aviso_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            CuentasPagar n = new CuentasPagar(empleado, apli);
            n.Show();
            Close();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            CuentasCobradas n = new CuentasCobradas(empleado,apli);
            n.Show();
            Close();
        }
    }
}
