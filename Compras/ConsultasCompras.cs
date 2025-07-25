using Fergeda_2023.Clases;
using Fergeda_2023.Finanzas;
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
    public partial class ConsultasCompras : Form
    {
        private formatoTablas Tb;
        private Consultas X;
        private estiloVentana est;
        private Archivos arc;
        private Rutas rt;
        bool flag = true;
        int empleado, apli;
         
        public ConsultasCompras(int empleado,int apli)
        {
            Tb = new formatoTablas();
            X = new Consultas();
            est = new estiloVentana();
            arc = new Archivos();
            rt = new Rutas();
            this.empleado = empleado;
            this.apli = apli;
            InitializeComponent();
            est.estilo(this);
            Tb.colores(dataGridView1, 1);
            Tb.quitarCelda(dataGridView1);
            inicializar();
        }
        
        #region PROPIEDADES
        
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            switch (apli)
            {
                case 4:
                    MenuFinanzas n1 = new MenuFinanzas(empleado);
                    n1.Show();
                    Close();
                    break;
                case 6:
                    MenuCompras n = new MenuCompras(empleado);
                    n.Show();
                    Close();
                    break;
            }
            
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

        private void lbl_Aviso_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void bunifuGradientPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        #endregion

        #region GENERALES
        private void inicializar()
        {
            label2.Text = label6.Text = label4.Text =  label26.Text = label28.Text = label30.Text = label22.Text = label21.Text = "N/A";
            label12.Text = label13.Text = label14.Text = "0.0";
            label24.Text = "0";
            label25.Text = ".";
            if (flag)
                textBox1.Text = "";
            flag = false;
            textBox2.Text = textBox3.Text = textBox4.Text = textBox5.Text = "N/A";

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                inicializar();
                buscar();
            }
        }

        private void CalculaTotales()
        {
            double Total1 = 0, Total2 = 0, Total3 = 0;
            for (int i = 0; i <= dataGridView1.Rows.Count - 1; i++)
            {
                Total1 += double.Parse(dataGridView1[5, i].Value.ToString());
                Total3 += double.Parse(dataGridView1[8, i].Value.ToString());
            }


            label12.Text = Total1.ToString("N2");
            label13.Text = "" + (Total3 - Total1).ToString("N2");
            label14.Text = "" + Total3.ToString("N2");
        }

        #endregion

        #region BUSQUEDAS
        private void buscar()
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string comple = "";
                if (radioButton1.Checked)
                    comple = "a.fk_ordencompra='" + textBox1.Text + "'";
                else if (radioButton3.Checked)
                    comple = "a.factura like '%" + textBox1.Text + "%'";
                else if (radioButton4.Checked)
                    comple = "a.fk_solicitudcheque like '%" + textBox1.Text + "%'";

                X.tablaDatos(dataGridView1, "SELECT  a.vobo 'Vo.Bo.',a.fk_ordencompra 'OC', a.partida 'Partida', a.cantidad 'Cantidad', " +
                    "a.unidad 'Unidad', format(a.precio_unitario, 2) 'C/U', format(a.sub_total, 2)'SubTotal', format(a.iva, 2) 'Iva', " +
                    "format(a.total, 2) 'Total', a.fecha_entrega 'Fecha Entrega',a.notas 'Notas', a.estatus 'Status', if (a.factura is null," +
                    "'N/A',a.factura) 'Factura' , a.fk_solicitudcheque 'Solicitud de Cheque', a.notas_internas 'Notas Internas',c.razon_social 'Proveedor', a.tipo_proyecto 'Proyecto'" +
                    ",  b.razon_social 'Cliente',  a.descripcion 'Descripción' FROM " +
                    "fergeda.c_listadocompras a inner join fergeda.f_cliente b  on a.fk_cliente = b.idf_cliente inner join " +
                    "fergeda.f_proveedor c on a.fk_proveedor = c.idf_proveedor inner join fergeda.r_idempleados d on a.fk_departamento = " +
                    "d.idr_depatamento  inner join fergeda.r_empleado e on a.fk_solicitaempleado = e.idr_empleado   " +
                    "where " + comple + "  group by   a.partida order by  a.idc_listadocompras desc; ");


                Tb.colorStatus(dataGridView1, "Status", "En Tramite de Pago", 250, 234, 157);
                Tb.colorStatus(dataGridView1, "Status", "Baja", 245, 120, 117);
                Tb.colorStatus(dataGridView1, "Status", "Pagado", 180, 245, 192);
                Tb.colorStatus(dataGridView1, "Status", "Recibida", 233, 217, 242);
                Tb.colorStatus(dataGridView1, "Status", "Esperando Factura", 242, 174, 114);
                Tb.colorStatus(dataGridView1, "Status", "Devolucion", 255, 192, 254);

                CalculaTotales();
                //cargarID();

            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            tablas();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                arc.abirArchivo(rt.OC(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                arc.abirArchivo(rt.OCCancelada(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                arc.abirArchivo(rt.OCFirmada(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                arc.abirArchivo(rt.SolicitudesCheque(dataGridView1[13, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                switch (dataGridView1.CurrentCell.ColumnIndex)
                {
                    case 0:                       
                            if (!string.IsNullOrEmpty(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                                X.insertar("Update fergeda.c_listadocompras set vobo= 'Si' where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                        break;
                    case 12:/*fac*/
                        if (!string.IsNullOrEmpty(dataGridView1[12, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                            if (X.elem3("SELECT factura FROM fergeda.c_listadocompras where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and partida='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';").Equals(""))
                                X.insertar("update  fergeda.c_listadocompras set factura='" + dataGridView1[12, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "',estatus='Recibida' where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and partida='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                            else
                                X.insertar("update  fergeda.c_listadocompras set factura='" + dataGridView1[12, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "',estatus='En Tramite de Pago' where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and partida='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                        break;
                    case 13:/*sol*/
                        if (!string.IsNullOrEmpty(dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                            if (X.elem3("SELECT Factura FROM fergeda.c_listadocompras where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and partida='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';").Equals(""))
                                X.insertar("update  fergeda.c_listadocompras set fk_solicitudcheque='" + dataGridView1[13, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "',estatus='Esperando Factura' where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and partida='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                            else
                                X.insertar("update  fergeda.c_listadocompras set fk_solicitudcheque='" + dataGridView1[13, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "',estatus='En Tramite de Pago' where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and partida='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                        break;
                    case 14:
                        if (!string.IsNullOrEmpty(dataGridView1[14, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                        {
                            string not = X.elem3("SELECT if(notas_internas='N/A','',notas_internas) FROM fergeda.c_listadocompras where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and partida='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                            string emp = X.elem3("SELECT concat_ws(' ',nombre,ap_paterno,ap_materno) FROM fergeda.r_empleado where idr_empleado='" + empleado + "';");
                            X.insertar("update fergeda.c_listadocompras set notas_internas='" + emp + ":" + dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value.ToString() + (not == "" ? "" : (" / " + not)) + "' where fk_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' and partida='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                        }
                        break;
                }
            }
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            new CancelarOC(empleado).Show();
        }

        private void tablas()
        {
            if (dataGridView1.Rows.Count > 0)
            {

                //label21.Text = DateTime.Parse(dataGridView1[13, dataGridView1.CurrentCell.RowIndex].Value.ToString()).ToString("dd/MM/yyyy");              
                textBox4.Text = dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                label6.Text = dataGridView1[11, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                label2.Text = dataGridView1[17, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                textBox2.Text = dataGridView1[15, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                //label4.Text = dataGridView1[17, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                label30.Text = dataGridView1[16, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                //label28.Text = dataGridView1[14, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                //label26.Text = dataGridView1[13, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                textBox3.Text = dataGridView1[18, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                label25.Text = dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                label24.Text = dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                textBox5.Text = dataGridView1[14, dataGridView1.CurrentCell.RowIndex].Value.ToString();

                label22.Text = X.elem3("SELECT concat_ws(' ',nombre,ap_paterno,ap_materno) FROM fergeda.r_empleado where idr_empleado=" +
                    "(SELECT fk_empleado FROM fergeda.c_ordencompra where idc_ordencompra='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "');");
                
            }
        }

        #endregion
    }
}
