using Fergeda_2023.Clases;
using Fergeda_2023.Compras;
using Fergeda_2023.Compras.PDF;
using Fergeda_2023.Finanzas;
using Fergeda_2023.General;
using Fergeda_2023.Mantenimiento_Edificio;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Fergeda_2023
{
    public partial class Form1 : Form
    {
        private estiloVentana est;
        private Consultas X;
        private Info info;
        private MensageC msg;
        public Form1()
        {
            est = new estiloVentana();
            X = new Consultas();
            info = new Info();
            msg = new MensageC();
            InitializeComponent();
            est.estilo(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new CuentasPagar(2647,2).ShowDialog();
            //new CuentasPagar(749, 4).Show();
            //new SolicitudChequePDF().generarCheque("30");
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            //new GenerarOCF().generarOC(3);
        }

        private void bunifuImageButton3_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        bool accesos = false;
        private void txt_usuario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if (txt_usuario.Text == "Usuario")
            {
                txt_usuario.Text = "";
                txt_usuario.ForeColor = Color.Black;
                accesos = true;
            }
        }

        private void txt_usuario_Leave(object sender, EventArgs e)
        {
            if (txt_usuario.Text == "")
            {
                txt_usuario.Text = "Usuario";
                txt_usuario.ForeColor = Color.DarkGray;
            }
        }

        private void txt_Contraseña_Leave(object sender, EventArgs e)
        {
            if (txt_Contraseña.Text == "")
            {
                txt_Contraseña.Text = "Contraseña";
                txt_Contraseña.ForeColor = Color.DarkGray;
                txt_Contraseña.UseSystemPasswordChar = false;
            }
        }

        private void txt_Contraseña_Enter(object sender, EventArgs e)
        {
            if (txt_Contraseña.Text == "Contraseña")
            {
                accesos = true;
                txt_Contraseña.Text = "";
                txt_Contraseña.ForeColor = Color.Black;
                txt_Contraseña.UseSystemPasswordChar = true;
            }
        }
        #region PERMISOS
        public string permisos()
        {
            string tip;
            tip = X.elem3("SELECT if(count(Permiso1)=0,'XuCbeXiOsCk=',Permiso1) FROM fergeda.s_pass where empleado='" + Convert.ToInt32(txt_usuario.Text) + "';");
            tip = new Transformacion().Mostrar(tip, new Transformacion().Formato);
            if (!tip.Equals("No"))            
                if (X.elem3("SELECT Estatus FROM fergeda.s_pass where empleado='" + Convert.ToInt32(txt_usuario.Text) + "'").Equals("Bloqueado"))                
                   tip = "No";
            return tip;
        }

        public bool contraseña()
        {
            bool ac = false;         
            if (new Transformacion().Mostrar(X.elem3("SELECT pass FROM fergeda.s_pass where empleado='" + Convert.ToInt32(txt_usuario.Text) + "';"), new Transformacion().Formato).Equals(txt_Contraseña.Text))            
                ac = true;            
            else  ac = false; 

            return ac;
        }

        #endregion
        int contador;
        private void txt_Contraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            int dat = 0;
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                int dep = info.PermisoApp(Convert.ToInt32(txt_usuario.Text));
              
                if (permisos() != "No" && contraseña() == true)
                {
                    info.accesos(txt_usuario.Text + "/Activo", dep);
                    dat++;
                    switch (dep)
                    {
                        case 4:
                            new MenuFinanzas(Convert.ToInt32(txt_usuario.Text)).Show();
                            Hide();
                            break;
                        case 6:
                            MenuCompras n = new MenuCompras(Convert.ToInt32(txt_usuario.Text));
                            n.Show();
                            Hide();
                            break;
                        case 7:
                            MenuMantoEdificio n1 = new MenuMantoEdificio(Convert.ToInt32(txt_usuario.Text));
                            n1.Show();
                            Hide();
                            break;
                    }                   
                }
                else
                {
                    contador++;
                    if (contador < 4)
                    {
                        msg.cargarEl("information", "Incorrecto", "Usuario o contraseña Incorrecta"); msg.Visible = true;
                        info.accesos(txt_usuario.Text + "/Denegado", dep);
                        txt_Contraseña.Text = "";
                    }
                    else
                    {
                        msg.cargarEl("lock(1)", "Acceso Denegado", "La cuenta se a Bloqueado Comuniquese\ncon el Administrador"); msg.ShowDialog();                       
                            X.insertar("UPDATE fergeda.s_pass SET Estatus='Bloqueado' WHERE empleado='" + Convert.ToInt32(txt_Contraseña.Text) + "';");/*Cambiar a bloqueado*/
                        info.accesos(txt_usuario.Text + "/Bloqueada", dep);
                    }
                }
            }
            
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            new RequisicionCompra(3453).Show();
            //new requisicioncompra().LlenadoPdf(11);
            //new MenuCompras(2940).ShowDialog();
            //new chequeAsignado().Show();
            //new CuentasCobradas(2647,4).Show();
        }
    }
}
