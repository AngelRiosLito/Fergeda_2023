using Fergeda_2023.Clases;
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
    public partial class chequeAsignado : Form
    {
        private Consultas X;
        private estiloVentana est;
        private formatoTablas Tb;
        int pagina = 1;
        public chequeAsignado()
        {
            X = new Consultas();
            est = new estiloVentana();
            Tb = new formatoTablas();
            InitializeComponent();
            est.estilo(this);
            Tb.colores(dataGridView1,0);
            Tb.elminarRows(dataGridView1);

        }

        #region Ventana   
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
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #region TABPAGE
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            pagina = 1;
            Tb.elimElemConConsulta(dataGridView1);
            textBox1.Text = maXChe()+"";
            textBox2.Text = hasta()+"";
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            pagina = 2;
        }

        #endregion

        #region HOJA 1
        private int maXChe() => Convert.ToInt32(X.elem3("SELECT max(fk_cheque)+1 FROM  fergeda.f_movimiento  where fk_bancoMostrado='1';"));/***/
        private int hasta() => Convert.ToInt32(X.elem3("SELECT max(idf_cheque) FROM fergeda.f_cheque;"));/*convertirla a fergeda*/
        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) && !string.IsNullOrEmpty(textBox2.Text))
            {
              


                X.tablaDatos(dataGridView1, "SELECT a.idf_cheque 'Cheque' ,a.fecha_Gen'Fecha de Cheque',if(b.razon_social='FINIQUITO',concat(b.razon_social,' / ',a.Notas),b.razon_social) "+
                                     "'Cliente', a.concepto'Concepto', a.importe'Importe'  FROM fergeda.f_cheque a inner join fergeda.f_proveedor b on a.fk_proveedor = b.idf_proveedor "+
                                    " inner join fergeda.r_empleado c on a.fk_solicitante = c.idr_empleado where  idf_cheque between '" + textBox1.Text + "' and '" + textBox2.Text + "' order by a.idf_cheque asc; ");

            }
        }

        
        private void GuardarCheque()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (X.elem3("SELECT  count(*) from fergeda.f_movimiento where fk_cheque='" + dataGridView1[0, i].Value.ToString() + "';").Equals("0"))
                    {
                        int maxF = Convert.ToInt32(X.elem3("SELECT max(flag)+1 FROM fergeda.f_movimiento where fk_bancoMostrado='1';"));
                        double val = double.Parse(X.elem3("SELECT saldo FROM fergeda.f_movimiento where idf_movimiento=(select max(idf_movimiento)FROM fergeda.f_movimiento where fk_bancoMostrado='1');"));

                        X.insertar("INSERT INTO fergeda.f_movimiento (fk_cheque, cliente, descripcion, cargo, saldo, fecha, fk_bancoMostrado,flag) VALUES" +
                            " ('" + dataGridView1[0, i].Value.ToString() + "', '" + dataGridView1[2, i].Value.ToString() + "', '" + dataGridView1[3, i].Value.ToString()
                            + "', '" + double.Parse(dataGridView1[4, i].Value.ToString()) + "', '" + (val - double.Parse(dataGridView1[4, i].Value.ToString()))
                            + "', '" + DateTime.Parse(dataGridView1[1, i].Value.ToString()).ToString("yyyy/MM/dd") + "', '1','" + maxF + "');");
                    }
                }
            }
        }

        #endregion

        #region HOJA 2
        private void GuardarCobranza()
        {

        }
        #endregion

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            switch (pagina)
            {
                case 1:
                    Console.WriteLine(" entro al  1");
                    GuardarCheque();
                    break;
                case 2:
                    Console.WriteLine(" entro al  2");
                    GuardarCobranza();
                    break;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                switch (pagina)
                {
                    case 1:
                        Tb.alinear(e,4,1);
                        Tb.FilaNegritas(e, 4);
                        Tb.FormatoCondicionalDatos(e);
                        break;
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
