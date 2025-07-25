using Fergeda_2023.Clases;
using Fergeda_2023.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Finanzas
{    
    public partial class CuentasCobradas : Form
    {

        private Consultas X;
        private Archivos arc;
        private estiloVentana est;
        private formatoTablas Tb;
        private FormatoFechas fech;
        private Rutas rt;
        private MensageC msg;
    
        int empleado, apli, pagina=1;
        int id_cliente, diasVen=0;
        double totalCobranza = 0;


        public CuentasCobradas(int empleado,int apli)
        {
            X = new Consultas();
            arc = new Archivos();
            est = new estiloVentana();
            Tb = new formatoTablas();
            rt = new Rutas();
            msg = new MensageC();
            fech = new FormatoFechas();
            this.empleado = empleado;
            this.apli = apli;
            InitializeComponent();
            est.estilo(this);
            Tb.colores(dataGridView1,1);
            Tb.quitarCelda(dataGridView1);
        }

        private string pag()
        {
            string p = " and pagado<>'Si' and pagado<>'Baja' ";
            if (rb_NoPagado.Checked) { p = " and pagado<>'Si' and pagado<>'Baja'"; }
            else if (rb_Pagado.Checked) { p = " and pagado<>'No' and pagado<>'V' and pagado<>'Baja'"; }
            else if (rb_Baja.Checked) { p = " and pagado='Baja'"; }
            else if (rb_Todo.Checked) { p = ""; }
            return p;
        }

        private void cargarCuentasPorCobrar(string comple)
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);
            string pagado = pag();
            if (ch_Proveedor.Checked)
            {
                comple = " Cliente='" + com_Cliente.Text + "'";
                //pagado = "";
            }


            X.tablaDatos(dataGridView1, "SELECT Cliente,Factura,fecha_vencimiento 'Fecha de Vencimiento',round(Vencido,2) 'Vencido'," +
                "round(Total,2) 'Importe Factura',fecha_revision 'Fecha Revision',Fecha 'Fecha Factura', Crédito, Cobranza, notas_credito " +
                "'Notas de Credito', Cancelada, Saldo, Vendedor, Moneda, Costo_Dolares 'Costo Dolares', Intercambios, Anticipo, " +
                "Observaciones, Pagado, Numero_NC 'Numero de NC'  FROM fergeda.f_cuentaxcobrar where " + comple + " " + pagado + "; ");

            Tb.colorStatus(dataGridView1, "Pagado", "Si", 132, 255, 67);
            Tb.colorStatus(dataGridView1, "Pagado", "V", 252, 239, 135);
            Tb.colorStatus(dataGridView1, "Pagado", "Baja", 255, 73, 89);

        }


        #region TABPAGE
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            C_Cobrar();
            pagina = 1;
        }
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            combos();
            CuentasCobradas1();
            pagina = 2;
            
        }

        #endregion

        #region GENERALES
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            
           
                    switch (pagina)
                    {
                        case 1:
                            cargarCuentasPorCobrar(fecha());
                            break;
                        case 2:
                           CuentasCobrar();
                            break;
                    }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                switch (pagina)
                {
                    case 1:
                        Tb.alinear(e, 1, 0);
                        Tb.FilaNegritas(e, 1);
                        Tb.alinear(e, 3, 1);
                        Tb.FilaNegritas(e, 4);
                        Tb.alinear(e, 4, 1);
                        Tb.alinear(e, 6, 0);
                        Tb.alinear(e, 6, 1);
                        Tb.alinear(e, 8, 1);
                        Tb.alinear(e, 10 , 1);
                        Tb.alinear(e, 11, 1);
                        Tb.FilaNegritas(e, 11);
                        break;

                }
            }
        }
        private string fecha()
        {
            string complemento = "";
            string revision = "";

            if (rb_vencimientocc.Checked)
                revision = "fecha_Vencimiento";
            else if (rb_revisioncc.Checked)
                revision = "fecha_revision";

            if (rb_añocc.Checked)
                complemento = "year(" + revision + ")='" + dateTimePicker2.Value.Year + "'";
            else if (rb_mescc.Checked)
                complemento = "year(" + revision + ")='" + dateTimePicker2.Value.Year + "' and month(" + revision + ")='" + dateTimePicker2.Value.Month + "'";
            else if (rb_diacc.Checked)
                complemento = "" + revision + "='" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "'";

            if (ch_Proveedor.Checked)
            {
                if (!string.IsNullOrEmpty(com_Cliente.Text))
                {
                    complemento += " and Cliente='" + com_Cliente.Text + "'";
                }
            }

            return complemento;
        }

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            switch (pagina)
            {
                case 1:
                    if (!string.IsNullOrEmpty(txt_Factura.Text) && !string.IsNullOrEmpty(com_tipoCambio.Text) && !string.IsNullOrEmpty(txt_Dolares.Text) && lbl_ClienteC.Text != ".")
                    {

                        //MessageBox.Show(id_cliente+"---- "+ txt_Factura.Text+"-----"+ diasVen+"-----\n"+ fech.diaPago(dateTimePicker2, diasVen)+"----"+ com_tipoCambio.Text+"-----"+ empleado);
                        X.insertar("call fergeda.f_InsertarCuentaXCobrar('" + id_cliente + "', '" + Convert.ToInt32(txt_Factura.Text) + "', '" + fech.diaPago(dateTimePicker2, diasVen).ToString("yyyy/MM/dd") + "','0', '" + com_tipoCambio.Text + "',' " + empleado + "','"+DateTime.Now.ToString("yyyy/MM/dd")+"') ;");
                        MessageBox.Show(lbl_Importe + "---- " + txt_Factura.Text);
                        X.insertar("UPDATE fergeda.f_cuentascobrar SET saldo='" + float.Parse(lbl_Importe.Text) + "' WHERE fk_factura='" + Convert.ToInt32(txt_Factura.Text) + "';");
                        Console.WriteLine("PASO*****");
                        if (chec_fecha.Checked)
                            X.insertar("update fergeda.f_cuentascobrar set Revision='S' where fk_factura='" + txt_Factura.Text + "';");
                        cargarCuentasPorCobrar(fecha());
                        LimpiarCuentasXCobrar();
                    }
                    else { msg.cargarEl("no-molestar", "Error ", "No deje campos vacios"); msg.ShowDialog(); }

                    break;
                case 2:
                    if (!string.IsNullOrEmpty(txt_FacturaCobrar.Text))
                    {
                        if (!string.IsNullOrEmpty(txt_NoNC.Text) && !string.IsNullOrEmpty(txt_ImporteNC.Text) && !string.IsNullOrEmpty(txt_cobranza.Text) && !string.IsNullOrEmpty(txt_NP.Text) && !string.IsNullOrEmpty(com_BancoPago.Text))
                        {
                            string banco = X.elem3("SELECT idfin_Banks FROM litopolis_v5.fin_ban where cuenta='" + com_BancoPago.Text + "';");
                            X.insertar("INSERT INTO litopolis_v5.fin_cuentascobradas (id_Factura,  id_Cliente, Cobranza, fecha, numero_Pago, Banco,notaCredito) VALUES " +
                            "('" + Convert.ToInt32(txt_FacturaCobrar.Text) + "', '" + id_cliente + "', '" + float.Parse(txt_cobranza.Text) + "', '" +
                            DateTime.Now.ToString("yyyy/MM/dd") + "', '" + Convert.ToInt32(txt_NP.Text) + "', '" + banco + "','" + float.Parse(txt_ImporteNC.Text) + "');");
                            X.insertar("insert into litopolis_v5.fin_salcob (fk_factura,monto,flag,fecha,fk_ban,lt) values('" + Convert.ToInt32(txt_FacturaCobrar.Text)
                                + "','" + float.Parse(txt_cobranza.Text) + "','0',now()," + banco + ",'" + txt_NP.Text + "');");


                            actualizarSaldos(txt_FacturaCobrar.Text);
                            CuentasCobrar();
                            LimpiarCobro();
                        }
                        else { msg.cargarEl("no-molestar", "Error ", "No deje campos vacios"); msg.ShowDialog(); }
                    }
                    else
                    {
                        msg.cargarEl("no-molestar", "Error ", "Ingresa una factura para poder\ncontinuar"); msg.ShowDialog();
                    }
                    break;

            } 
        }

        private void Z(object sender, EventArgs e)
        {

        }

       

      


        #endregion

        #region HOJA 1
        private void C_Cobrar()
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);
             

            string comple = "", fechas= "";

            if (rb_vencimientocc.Checked)            
                fechas = "fecha_vencimiento";            
            else if (rb_revisioncc.Checked)            
                fechas = "fecha_revision";
              

            if (rb_diacc.Checked)            
                comple = "" + fechas + "='" + dateTimePicker2.Value.ToString("yyyy/MM/dd")+"';";            
            else if (rb_mescc.Checked)            
                comple = "year("+fechas+")='"+dateTimePicker2.Value.Year+ "' and month("+fechas+")='" + dateTimePicker2.Value.Month + "'";            
            else if (rb_añocc.Checked)            
                comple = "year(" + fechas + ")='" + dateTimePicker2.Value.Year + "'" ;
            

            //if (ch_Proveedor.Checked)
            //{
            //    if (!string.IsNullOrEmpty(com_Cliente.Text))
            //    {
            //        complemento += " and Cliente='" + com_Cliente.Text + "'";
            //    }
            //}

            //string p = " and Pagado<>'Si' and Pagado<>'Baja' ";
            //if (rb_NoPagado.Checked) { p = " and Pagado<>'Si' and Pagado<>'Baja'"; }
            //else if (rb_Pagado.Checked) { p = " and Pagado<>'No' and Pagado<>'V' and Pagado<>'Baja'"; }
            //else if (rb_Baja.Checked) { p = " and Pagado='Baja'"; }
            //else if (rb_Todo.Checked) { p = ""; }

            X.tablaDatos(dataGridView1, " select Cliente, Factura, fecha_vencimiento'Fecha de Vencimiento', FORMAT(Vencido, 2) 'Vencido', FORMAT(Total, 2) 'Importe Factura', "+
                         "fecha_revision 'Fecha de Revisión', Fecha 'Fecha Factura', Crédito, FORMAT(Cobranza, 2) 'Cobranza', FORMAT(notas_credito, 2) 'Nota de Crédito', "+
                         "FORMAT(Cancelada, 2)'Cancelado', FORMAT(Saldo, 2)'Saldo', Vendedor, Moneda, Costo_Dolares'Tipo de Cambio', Intercambios, Anticipo, Observaciones, "+
                        "Pagado, Numero_NC, 'Revisión' from fergeda.f_cuentaxcobrar where "+comple+"; ");

           // X.tablaDatos(dataGridView1, "select Cliente,Factura,fecha_Vencimiento 'Fecha de Vencimiento',FORMAT(Vencido,2) 'Vencido',FORMAT(total,2) 'Importe Factura',fecha_Revision 'Fecha de Revisión',fecha 'Fecha Factura',Crédito,FORMAT(cobranza,2) " +
           //"'Cobranza', FORMAT(notaCredito,2) 'Nota de Crédito', FORMAT(Cancelada,2) 'Cancelado',FORMAT(Saldo,2)'Saldo', Vendedor, Moneda, costo_Dolares 'Tipo de Cambio', Intercambios, Anticipos, Observaciónes, Pagado,Numero_NC,Revisión from" +
           //" litopolis_v5.fin_cuentaXCobrar where " + complemento + " " + p + "; ");

            //Tb.colorStatus(dataGridView1, "Pagado", "Si", 153, 232, 159);
            //Tb.colorStatus(dataGridView1, "Pagado", "V", 242, 207, 194);
            //Tb.colorStatus(dataGridView1, "Pagado", "Baja", 255, 105, 97);
            //Tb.colorStatus(dataGridView1, "Revisión", "S", 227, 199, 95);
        }
        //pendiente cambiooooooooooooooooooooooooooooo
        private void com_tipoCambio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(com_tipoCambio.Text))
            {
                if (!com_tipoCambio.Text.Equals("MXN"))
                {
                    txt_Dolares.Visible = true;
                }
                else
                {
                    txt_Dolares.Visible = false;
                }
                //Console.WriteLine("entro");
                //switch (com_tipoCambio.Text)
                //{
                //    case "USD":case "EUR":
                //        txt_Dolares.Visible = true;
                //        break;
                //    default:
                //        txt_Dolares.Visible = false;
                //        break;
                //}

            }
        }
            private void txt_Factura__KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                id_cliente = diasVen = 0;
                if (!string.IsNullOrEmpty(txt_Factura.Text))
                {
                    if (Convert.ToInt32(X.elem3("call fergeda.consultaCuentasPorCobrar(" + Convert.ToInt32(txt_Factura.Text) + ",1);")) > 0)
                    {
                        if (X.elem3("SELECT count(*) FROM fergeda.f_cuentascobradas where fk_factura='" + txt_Factura.Text + "';").Equals("0"))
                        {
                            id_cliente = Convert.ToInt32(X.elem3("SELECT fk_cliente FROM fergeda.f_cuentascobrar where fk_factura='" + txt_Factura.Text + "' ;"));
                            lbl_ClienteC.Text = X.elem3("SELECT razon_social FROM fergeda.f_liscliente where idf_cliente='" + id_cliente + "';");
                            lbl_Importe.Text = "" + float.Parse(X.elem3("call fergeda.consultaCuentasPorCobrar2(" + Convert.ToInt32(txt_Factura.Text) + ",1);")).ToString("N2");
                            diasVen = Convert.ToInt32(X.elem3("call fergeda.consultaCuentasPorCobrar2(" + id_cliente + ",2);"));

                        }
                        else
                        {
                            msg.cargarEl("no-molestar", "Error ", "No se puede ingresar la factura\nSe encuentra registrada"); msg.ShowDialog();
                        }
                    }
                    else { msg.cargarEl("information", "Error ", "No se encuentra la Factura registrada"); msg.ShowDialog(); }
                }
                else { msg.cargarEl("no-molestar", "Error ", "No deje campos vacios"); msg.ShowDialog(); }
            }
        }

        private void com_Cliente_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region HOJA 2
        private void combos()
        {
            com_BancoPago.Items.Clear();
            X.comb("SELECT Banco FROM fergeda.f_banks where flag=1 and status='Alta' order by Banco; ", com_BancoPago);
        }
        private void CuentasCobradas1()
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);


        }

        private void txt_FacturaCobrar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txt_FacturaCobrar.Text))
                {
                    if (Convert.ToInt32(X.elem3("call fergeda.consultaCuentasPorCobrar(" + Convert.ToInt32(txt_FacturaCobrar.Text) + ",1);")) > 0)
                    {
                        if (X.elem3("SELECT count(*) FROM fergeda.f_cuentascobradas where fk_factura='" + txt_FacturaCobrar.Text + "';").Equals("0"))
                        {
                            id_cliente = Convert.ToInt32(X.elem3("SELECT fk_cliente FROM fergeda.f_cuentascobrar where fk_factura='" + txt_FacturaCobrar.Text + "' ;"));
                            lbl_ClienteC1.Text = X.elem3("SELECT razon_social FROM fergeda.f_liscliente where idf_cliente='" + id_cliente + "';");
                            txt_cobranza.Text = float.Parse(X.elem3("SELECT total FROM fergeda.f_factura where idf_Factura='" + txt_FacturaCobrar.Text + "';")).ToString("N2");
                            totalCobranza = float.Parse(txt_cobranza.Text);
                        }
                        else
                        {
                            msg.cargarEl("no-molestar", "Error ", "No se puede ingresar la factura\nSe encuentra registrada"); msg.ShowDialog();
                            txt_cobranza.Text = "0.0"; lbl_ClienteC1.Text = ".";
                        }
                    }
                    else { msg.cargarEl("information", "Error ", "No se encuentra la Factura registrada"); msg.ShowDialog(); txt_cobranza.Text = "0.0"; lbl_ClienteC1.Text = "."; }
                }

                else { msg.cargarEl("no-molestar", "Error ", "No deje campos vacios"); msg.ShowDialog(); }
            }
        }

        #endregion
     
        private void LimpiarCuentasXCobrar()
        {
            txt_Factura.Text = ""; lbl_ClienteC.Text = ".";
            lbl_Importe.Text = txt_Dolares.Text = "0.0";
            txt_Dolares.Enabled = false;

        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            MenuFinanzas n = new MenuFinanzas(empleado);
            n.Show();
            Close();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            ImpoExportarExcel ex = new ImpoExportarExcel();
            using (var fd = new FolderBrowserDialog())
            {
                SaveFileDialog fichero = new SaveFileDialog();
                fichero.Filter = "Excel (*.xlsx)|*.xlsx";
                if (fichero.ShowDialog() == DialogResult.OK)
                {
                    new ImpoExportarExcel().exportarExcel(dataGridView1, fichero.FileName);
                }
            }
        }

        private void LimpiarCobro()
        {
            txt_FacturaCobrar.Text = "";
            txt_cobranza.Text = "0.0";
            txt_NP.Text = "0";
            lbl_ClienteC.Text = ".";
        }


        #region   CO0NSULTAS 
        private void actualizarSaldos(string fac)
        {
            /*Corregir el procedimiento*/
            float pago = float.Parse(X.elem3("SELECT sum(cobranza+importenc+importecancelado) FROM fergeda.f_cuentascobradas where fk_factura='" + fac + "';"));
            float total = float.Parse(X.elem3("SELECT total FROM fergeda.f_factura where idf_Factura='" + fac + "';"));
            float pago2 = float.Parse(X.elem3("SELECT cobranza FROM fergeda.f_cuentascobradas where fk_factura='" + fac + "';"));
            X.insertar("UPDATE  fergeda.f_cuentascobrar SET saldo='" + (total - pago) + "',saldo_vencido='" + totalCobranza + "', cobranza='" + pago2 + "' WHERE fk_factura='" + fac + "';");
            float saldo = float.Parse(X.elem3("SELECT saldo FROM fergeda.f_cuentascobrar WHERE fk_factura='" + fac + "';"));
            if (saldo.Equals(0))
            {
                X.insertar("update  fergeda.f_cuentascobrar set pagado='Si' where fk_factura='" + fac + "'");
            }
        }

        private void CuentasCobrar()
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);
            com_BancoPago.Items.Clear();
            string comple = "";
            if (rb_AñoCobradas.Checked)
            {
                comple = "year(Fecha)='" + dateTimePicker1.Value.ToString("yyyy") + "'";
            }
            else if (rb_MEsCobradas.Checked)
            {
                comple = "year(Fecha)='" + dateTimePicker1.Value.ToString("yyyy") + "' and month(Fecha)='" + dateTimePicker1.Value.ToString("MM") + "'";
            }
            else if (rb_diaCobradas.Checked)
            {
                comple = "Fecha='" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "'";
            }

            if (che_Cliente2.Checked)
            {
                comple = comple + " and Cliente='" + com_Cliente2.Text + "'";
            }

            //MessageBox.Show("entro");
            //MessageBox.Show()
            X.tablaDatos(dataGridView1, "SELECT * FROM fergeda.f_cuentacobranza where " + comple + ";");
            X.comb("SELECT Banco FROM fergeda.f_bank;", com_BancoPago);
            Tb.colorStatus(dataGridView1, "Complemento", "-", 255, 230, 82);
        }

        #endregion
       

    }
}
