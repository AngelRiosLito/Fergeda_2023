using Fergeda_2023.Clases;
using Fergeda_2023.General;
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

namespace Fergeda_2023.Compras
{
    public partial class Proveedor : Form
    {
        int empleado;
        private Consultas X;
        private MensageC msg;
        private estiloVentana est;
        string dias = "", condicion = "", municipio = "";
        public Proveedor(int empleado)
        {
            InitializeComponent();
            this.empleado = empleado;
            X = new Consultas();
            msg = new MensageC();
            est = new estiloVentana();
            CargarMunicipios();
            est.estilo(this);
        }

        #region PROPIEDADES VENTANA
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
        #endregion

        #region Cargar Municipio
        private void CargarMunicipios()
        {
            com_municipio.Items.Clear();
            com_municipio.Text = "";
            X.comb("SELECT distinct(municipio) FROM fergeda.r_municipios order by municipio asc;", com_municipio);
        }
        #endregion

        #region Válidar espacios vacíos
        
        private bool ValidarTextos()
        {
            bool bandera = true;
            List<TextBox> listaCajasTexto = new List<TextBox> {txt_nombre,txt_rfc,txt_razonSocial,txt_contacto,txt_telefono,txt_correo,txt_cp,txt_exterior,txt_interior};
            for (int i = 0; i < listaCajasTexto.Count; i++)
            {
                if (string.IsNullOrEmpty(listaCajasTexto[i].Text))
                {
                    bandera = false;
                    msg.cargarEl("information", "Aviso", "Está dejando espacios en blanco"); msg.ShowDialog();
                    break;
                }
            }
            return bandera;
        }

        #endregion

        #region Bloquear escritura de combobox
        private void comboBox4_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = true;
        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = true;
        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e) => e.Handled = true;
        #endregion

        #region Configuración Num. Telefono
        private void permitirNumeros(object sender, KeyPressEventArgs e, TextBox txtbox)
        {
            txtbox = sender as TextBox;
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '+')
                e.Handled = true;
            else
            {
                if (e.KeyChar == '+' && txtbox.Text.Contains('+'))
                    e.Handled = true;
                else if (e.KeyChar == '(' && txtbox.Text.Contains('('))
                    e.Handled = true;
                else if (e.KeyChar == ')' && txtbox.Text.Contains(')'))
                    e.Handled = true;
            }
        }
        private void txt_telefono_KeyPress(object sender, KeyPressEventArgs e) => permitirNumeros(sender, e, txt_telefono);
        #endregion

        #region Restringir Código postal
        private void txt_cp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        #endregion

        #region Restringir RFC
        private void txt_rfc_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                e.Handled = true;
        }
        #endregion

        #region Inserción de datos
        private void AsignarCondicion()
        {
            municipio = X.elem3("SELECT idr_municipios FROM fergeda.r_municipios where municipio = '"+ com_municipio.Text + "';");
            dias = cm_CondicionPago.Text.Equals("Contado") ? "0" : cm_CondicionPago.Text.Split(' ')[0];
            condicion = cm_CondicionPago.Text.Equals("Contado") ? "CONTADO" : "Días";            
        }

        
        private void InsercionDatos()
        {
            if (ValidarTextos())
            {
                AsignarCondicion();
                X.insertar("insert into fergeda.f_proveedor (proveedor, razon_social, clasificacion, contacto, telefono, correo, numero_exterior, numero_interior, fk_municipio, cp, estatus, fk_empleado, dias, condiciones, rfc) " +
                    "values ('" + txt_nombre.Text + "','" + txt_razonSocial.Text + "','" + com_clasificacion.Text + "','" + txt_contacto.Text + "','" + txt_telefono.Text + "','" + txt_correo.Text + "'," +
                    "'" + txt_exterior.Text + "','" + txt_interior.Text + "','" + municipio + "','" + txt_cp.Text + "','Alta','" + empleado + "','" + dias + "','" + condicion + "','" + txt_rfc.Text + "')");
                LimpiarDatos();
                CargarMunicipios();
                msg.cargarEl("correcto", "Correcto","Registro de proveedor correcto"); msg.ShowDialog();
            }
        }
        private void bunifuImageButton2_Click(object sender, EventArgs e) => InsercionDatos();        
        #endregion

        #region Limpiar datos
        private void LimpiarDatos()
        {
            List<TextBox> listaCajasTexto = new List<TextBox> { txt_nombre, txt_rfc, txt_razonSocial, txt_contacto, txt_telefono, txt_correo, txt_cp, txt_exterior, txt_interior };
            for (int i = 0; i < listaCajasTexto.Count; i++)
                listaCajasTexto[i].Text = "";
            cm_CondicionPago.Text = "Contado";
            com_municipio.Text = "Abasalo";
        }        
        #endregion

        #region Mover Tab
        private void moverMarcador(object sender, KeyEventArgs e, TextBox txtbox, TextBox txtbox1)
        {
            if (e.KeyCode.Equals(Keys.Tab))
            {
                e.SuppressKeyPress = true;
                if (sender.Equals(txtbox))
                    txtbox1.Focus();
            }
        }
        private void txt_nombre_KeyDown(object sender, KeyEventArgs e) => moverMarcador(sender, e, txt_nombre, txt_razonSocial);
        private void txt_cp_KeyDown(object sender, KeyEventArgs e) => moverMarcador(sender, e, txt_cp, txt_exterior);        
        private void txt_exterior_KeyDown(object sender, KeyEventArgs e) => moverMarcador(sender, e, txt_exterior, txt_interior);
        private void txt_interior_KeyDown(object sender, KeyEventArgs e) => moverMarcador(sender, e, txt_interior, txt_nombre);
        #endregion
        private void bunifuImageButton1_Click(object sender, EventArgs e) => this.Close();
    }
}