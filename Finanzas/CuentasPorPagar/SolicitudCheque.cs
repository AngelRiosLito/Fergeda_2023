using Fergeda_2023.Clases;
using Fergeda_2023.Finanzas.CuentasPorPagar;
using Fergeda_2023.General;
using Fergeda_2023.General.PDF;
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
    public partial class SolicitudCheque : Form
    {
        private formatoTablas Tb;
        private estiloVentana est;
        private MensageC msg;
        private Rutas rt;
        private Info info;
        private Consultas X;

        int empleado;
        public SolicitudCheque(int empleado)
        {
            this.empleado = empleado;
            InitializeComponent();
            Tb = new formatoTablas();
            est = new estiloVentana();
            msg = new MensageC();
            rt = new Rutas();
            info = new Info();
            X = new Consultas();
            Tb.colores(dataGridView1,2);
            est.estilo(this);
            Cargar();
            MensajesPeques();
        }

        private void MensajesPeques()
        {
            est.MensagePeque(bunifuImageButton6, "Cancelar Solicitud de Cheque");
        }
        private void Cargar()
        {
            txt_Solicitante.Text = X.elem3("SELECT concat_ws(' ',nombre,ap_paterno,ap_materno) FROM fergeda.r_empleado where idr_empleado='"+empleado+"';");
            txt_Departamento.Text =  X.elem3("SELECT departamento FROM fergeda.r_idempleados where idr_empleado='"+empleado+"';");
            X.comb("SELECT razon_social FROM fergeda.f_proveedor where estatus='Alta' order by razon_social;", com_Proveedor);
            txt_Folio.Text= X.elem3("SELECT max(idf_solicitudcheque)+1 FROM fergeda.f_solicitudcheque;");
        }

        private void btn_CalcularTotales_Click(object sender, EventArgs e)=> validar();        

        private void validar()
        {
            
            if (dataGridView1.Rows.Count > 0)
            {
                if (validarCampos(false))
                {
                    double sumatoria = 0;
                    for (int x = 0; x <= dataGridView1.Rows.Count - 1; x++)
                    {
                        if (dataGridView1[2, x].Value == null || dataGridView1[2, x].Value.ToString().Equals("")) { Console.WriteLine("Campo vacio 2"); }
                        else
                        {
                            if (dataGridView1[2, x].Value.ToString() != ".")
                            {
                                sumatoria += double.Parse(dataGridView1[2, x].Value.ToString());
                            }
                        }
                    }
                    txt_Subtotal.Text = "" + sumatoria.ToString("N2");
                    txt_iva.Text = (op_Iva_Si.Checked ? (sumatoria * .16).ToString("N2") : "0.0");
                    txt_Retencion.Text = (chec_honorarios.Checked ? ((sumatoria * .16) * 0.08).ToString("N2") : (chec_retencion.Checked ? (sumatoria * double.Parse(comboBox1.Text)).ToString("N2") : "0.0"));
                    txt_Importe.Text = txt_Total.Text = (sumatoria + double.Parse(txt_iva.Text) - double.Parse(txt_Retencion.Text)).ToString("N2");
                    dataGridView1.Rows.Add(".", ".", ".");
                }               
            }           
        }
        private void btn_Generar_Click(object sender, EventArgs e)
        {
            validar();
            string pro = X.elem3("SELECT idf_proveedor FROM fergeda.f_proveedor where razon_social='"+com_Proveedor.Text+"';");
            X.insertar("insert into fergeda.f_solicitudcheque (fk_empleado,fecha)values('" + empleado + "',now());");
            string id = X.elem3("SELECT max(idf_solicitudcheque) FROM fergeda.f_solicitudcheque where fk_empleado='"+empleado+"';");           
            Insertar(id, pro);
            limpiar();
        }

        private void Insertar(string id,string pro)
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1[2, i].Value.ToString().Equals("."))                
                    break;
                else
                {/*queda pendiente la factura*/
                    
                    X.insertar("insert into fergeda.f_solicitudlistado(fk_solicitud, orden_impresion, numero_Oc, concepto, importe, subtotal, " +
                        "iva, total, retencion, contiene_iva, contiene_retencion, estatus, motivo_cancelacion, fk_proveedor, importe_cheque, " +
                        "cancelada_por, fecha_pago, fecha,factura)values('" + id+"','N/A','"+txt_OC.Text+"','"+ dataGridView1[1, i].Value.ToString() + "'," +
                        "'"+ dataGridView1[2, i].Value.ToString() + "','"+double.Parse(txt_Subtotal.Text)+"','"+ double.Parse(txt_iva.Text) + "','"+ 
                        double.Parse(txt_Total.Text) + "','"+ double.Parse(txt_Retencion.Text) + "','"+(op_Iva_Si.Checked?"Si":"No") +"','"+ (chec_retencion.Checked ? "Si" : "No") 
                        + "','Alta','N/A','"+pro+"','" + double.Parse(txt_Total.Text) + "','N/A','"+ date_Pago.Value.ToString("yyyy/MM/dd") + "','"+ date_Fecha.Value.ToString("yyyy/MM/dd") + "'" +
                        ",'"+ dataGridView1[0, i].Value.ToString() + "'); ");
                }                
            }            
            new SolicitudChequePDF().generarCheque(id);

        }


        public bool validarCampos(bool s)
        {
            if (string.IsNullOrEmpty(txt_OI.Text) || string.IsNullOrEmpty(txt_OC.Text) || string.IsNullOrEmpty(txt_Folio.Text) || string.IsNullOrEmpty(txt_Departamento.Text) || string.IsNullOrEmpty(txt_Solicitante.Text) || string.IsNullOrEmpty(com_Proveedor.Text))
            {
                msg.cargarEl("no-molestar", "Error", "No se pueden dejar espacios\nen blaco"); msg.ShowDialog();
                s = false;
            }
            else  s = true; 
            return s;
        }

        private void chec_retencion_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = true;
            chec_honorarios.Checked = false;
        }

        private void op_Honorarios_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Visible = false;
            chec_retencion.Checked = false;
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void limpiar()
        {
            dataGridView1.Rows.Clear();
            rich_NC_ND.Text = "N/A";
            txt_OI.Text = txt_OC.Text= "";
            txt_Subtotal.Text = txt_iva.Text = txt_Retencion.Text = txt_Total.Text = txt_Importe.Text="0";
            op_Iva_Si.Checked = true;
            chec_retencion.Checked = chec_honorarios.Checked = false;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            CancelarSolicitud n = new CancelarSolicitud(empleado);
            n.ShowDialog();
        }
    }
}
