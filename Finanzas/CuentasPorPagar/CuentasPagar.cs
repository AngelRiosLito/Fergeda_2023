using Fergeda_2023.Clases;
using Fergeda_2023.Finanzas.PDF;
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

namespace Fergeda_2023.Finanzas
{
    public partial class CuentasPagar : Form
    {
        private formatoTablas Tb;
        private FlujoBancos flu;
        private FormatoFechas fech;
        private Consultas X;
        private estiloVentana est;
        private Archivos arc;
        private Rutas rt;
        private MensageC msg;
        private RotaciondePagosFer rot;
        private Transformacion tr;
        int empleado, apli,pagina=1;
        string opG = "transferencia";

        public CuentasPagar(int empleado,int apli)
        {
            Tb = new formatoTablas();
            X = new Consultas();
            est = new estiloVentana();
            arc = new Archivos();
            rt = new Rutas();
            fech = new FormatoFechas();
            msg = new MensageC();
            tr = new Transformacion();
            rot = new RotaciondePagosFer();
            flu = new FlujoBancos();
            this.empleado = empleado;
            this.apli = apli;
            InitializeComponent();
            est.estilo(this);
            Tb.colores(dataGridView1, 1);
            Tb.quitarCelda(dataGridView1);

            tabControl1.TabPages.Remove(tabPage6);
            tabControl1.TabPages.Remove(tabPage7);
            tabControl1.TabPages.Remove(tabPage8);
        }

        #region GENERALES

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
            new Info().cerrarConex("" + empleado, apli); Application.Exit();
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            MenuFinanzas n = new MenuFinanzas(empleado);
            n.Show();
            Close();
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int Columna = dataGridView1.CurrentCell.ColumnIndex;
                switch (pagina)
                {
                    case 1:
                        switch (Columna)
                        {
                            case 10:
                                if (dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value.ToString().ToUpper().Equals("USD") || dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value.ToString().ToUpper().Equals("EUR"))
                                {

                                    X.insertar("UPDATE fergeda.f_cuentasporpagar SET costo='" + dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString().ToUpper() +
                                        "',descripcion='" + dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString() + " Costo Factura: $ " + dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString().ToUpper() + " USD' WHERE fk_ordencompra='" + Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "';");

                                    float importe = float.Parse(dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString()) * float.Parse(dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value.ToString());

                                    X.insertar("UPDATE fergeda.f_ban_cuentasporpagar SET importe='" + importe + "' WHERE factura='" + dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                    if (dataGridView1[16, dataGridView1.CurrentCell.RowIndex].Value.ToString() != "0")
                                    {
                                        X.insertar(" UPDATE fergeda.f_cuentasporpagar SET saldo='" + importe + "' WHERE  fk_ordencompra='" + Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "' and fk_Factura='" + dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                    }

                                }
                                else
                                {
                                    msg.cargarEl("no-molestar", "Error ", "Solo se puede colocar en dolares"); msg.ShowDialog();
                                }
                                break;
                            case 14:
                                X.insertar("UPDATE fergeda.f_cuentasporpagar SET intercambios='" + dataGridView1[14, dataGridView1.CurrentCell.RowIndex].Value.ToString().ToUpper() + "' WHERE fk_ordencompra='" + Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "';");
                                break;
                            case 17:
                                if (float.Parse(dataGridView1[16, dataGridView1.CurrentCell.RowIndex].Value.ToString()) <= 0)
                                {
                                    X.insertar("UPDATE fergeda.f_cuentasporpagar SET pagado='Si',vencido='" +
                                        float.Parse(dataGridView1[8, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "' WHERE fk_ordencompra='" +
                                        Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "' and fk_Factura='" +
                                        dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                }
                                else
                                {
                                    msg.cargarEl("stop", "Error ", "No se puede completar la peticion\nCompleta el valor factura"); msg.ShowDialog();
                                }

                                break;
                            case 18:
                                X.insertar("UPDATE fergeda.f_cuentasporpagar SET cobro='" + dataGridView1[18, dataGridView1.CurrentCell.RowIndex].Value.ToString().ToUpper() + "' WHERE fk_ordencompra='" + Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "';");
                                break;
                            case 19:
                                X.insertar("UPDATE fergeda.f_cuentasporpagar SET referencia='" + dataGridView1[19, dataGridView1.CurrentCell.RowIndex].Value.ToString().ToUpper() + "' WHERE fk_ordencompra='" + Convert.ToInt32(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "';");
                                break;
                        }
                        break;
                    case 2:
                        switch (Columna)
                        {
                            case 2:
                                X.insertar("update fergeda.f_cuentaspagadas set nota_Credito='" + dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' where idf_cuentaspagadas='" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                break;
                            case 3:
                                X.insertar("update fergeda.f_cuentaspagadas set sol_cheque='" + dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' where idf_cuentaspagadas='" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                break;
                            case 5:
                                float pago = float.Parse(X.elem3("SELECT pago FROM fergeda.f_cuentaspagadas where idf_cuentaspagadas='" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                if (float.Parse(dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString()) > 0)
                                {
                                    X.insertar("update fergeda.f_cuentaspagadas set pago = '" + (float.Parse(dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + pago) + "' where idf_cuentaspagadas = '" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "'; ");
                                    float importe = float.Parse(X.elem3("SELECT sum(pago+importe_Nc+importe_Cancelado)  FROM fergeda.f_cuentaspagadas where factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                    float importeFac = float.Parse(X.elem3("SELECT importe FROM fergeda.f_ban_cuentasporpagar where factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                    X.insertar("update fergeda.f_cuentasporpagar set pagado='No', saldo='" + (importeFac - importe) + "' where fk_Factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                }
                                break;
                            case 6:
                                pago = float.Parse(X.elem3("SELECT importe_Nc FROM fergeda.f_cuentaspagadas where idf_cuentaspagadas='" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                if (float.Parse(dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString()) > 0)
                                {
                                    X.insertar("update fergeda.f_cuentaspagadas set importe_Nc = '" + (float.Parse(dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + pago) + "' where idf_cuentaspagadas = '" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "'; ");
                                    float importe = float.Parse(X.elem3("SELECT sum(pago+importe_Nc+importe_Cancelado)  FROM fergeda.f_cuentaspagadas where factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                    float importeFac = float.Parse(X.elem3("SELECT importe FROM fergeda.f_ban_cuentasporpagar where factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                    X.insertar("update fergeda.f_cuentasporpagar set pagado='No', saldo='" + (importeFac - importe) + "',notas_credito='" + (float.Parse(dataGridView1[6, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + pago) + "' where fk_Factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                }
                                break;
                            case 7:
                                pago = float.Parse(X.elem3("SELECT importe_Cancelado FROM fergeda.f_cuentaspagadas where idf_cuentaspagadas='" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                if (float.Parse(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString()) > 0)
                                {
                                    X.insertar("update fergeda.f_cuentaspagadas set importe_Cancelado = '" + (float.Parse(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + pago) + "' where idf_cuentaspagadas = '" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "'; ");
                                    float importe = float.Parse(X.elem3("SELECT sum(pago+importe_Nc+importe_Cancelado)  FROM fergeda.f_cuentaspagadas where factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                    float importeFac = float.Parse(X.elem3("SELECT importe FROM fergeda.f_ban_cuentasporpagar where factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';"));
                                    X.insertar("update fergeda.f_cuentasporpagar set pagado='No', saldo='" + (importeFac - importe) + "',cancelado='" + (float.Parse(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + pago) + "' where fk_Factura='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");

                                }
                                break;
                            case 9:
                                if (dataGridView1[9, dataGridView1.CurrentCell.RowIndex].Value.ToString() != "")
                                {
                                    X.insertar("update fergeda.f_cuentaspagadas set complemento='X' where idf_cuentaspagadas='" + dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                }
                                break;
                        }
                        break;
                    case 4:
                        switch (Columna)
                        {
                            case 0:
                                string status = X.elem3("SELECT Estatus FROM fergeda.f_cheque where idf_cheque='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                if (status.Equals("Cobrado") || status.Equals("Cancelado"))
                                {
                                    msg.cargarEl("information", "No permitido", "No se puede aplicar el cambio"); msg.ShowDialog();
                                }
                                else
                                {
                                    if (status.Equals("Pendiente"))
                                    {
                                        if (dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString().Equals("T"))
                                            X.insertar("update fergeda.f_cheque set Estatus='Transito' where idf_cheque='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                    }
                                    else
                                        if (dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString().Equals("C"))
                                        X.insertar("update fergeda.f_cheque set Estatus='Cobrado',fecha_Cobro='" + DateTime.Now.ToString("yyyy/MM/dd") + "' where idf_cheque='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                }
                                break;
                            case 7:
                                if (X.elem3("SELECT solicitud FROM fergeda.f_cheque where idf_cheque='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';").Equals("0"))
                                {
                                    if (Convert.ToInt32(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString()) <= Convert.ToInt32(X.elem3("SELECT max(idf_solicitudcheque) FROM fergeda.f_solicitudcheque;")) && Convert.ToInt32(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString()) > 0)
                                    {
                                        if (Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.f_cheque where solicitud='" + dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';")) > 0)
                                        {
                                            if (X.elem3("SELECT Estatus FROM fergeda.f_cheque  where solicitud='" + dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';").Equals("Cancelado"))
                                            {
                                                X.insertar("update fergeda.f_cheque set solicitud='" + Convert.ToInt32(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "' where idf_cheque='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                            }
                                            else { msg.cargarEl("information", "No permitido", "Debe de Cancelar anterior Para continuar"); msg.ShowDialog(); }
                                        }
                                        else
                                        {
                                            X.insertar("update fergeda.f_cheque set solicitud='" + Convert.ToInt32(dataGridView1[7, dataGridView1.CurrentCell.RowIndex].Value.ToString()) + "' where idf_cheque='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                        }
                                    }
                                    else { msg.cargarEl("information", "No permitido", "Solicitud no Valida"); msg.ShowDialog(); }
                                }
                                else { msg.cargarEl("information", "No permitido", "No se puede aplicar el cambio"); msg.ShowDialog(); }
                                break;
                            case 11:
                                if (X.elem3("SELECT Complemento FROM fergeda.f_cheque where idf_cheque='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';").Equals("-"))
                                {
                                    X.insertar("update fergeda.f_cheque set Complemento='X' where idf_cheque='" + dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                }
                                break;
                        }
                        break;
                    case 5:
                        switch (Columna)
                        {
                            case 4:
                                if (!string.IsNullOrEmpty(dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString()))                                
                                    X.insertar("update fergeda.f_movimiento set cliente='"+ dataGridView1[4, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' where flag='"+ dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");                                
                                break;
                            case 5:
                                if (!string.IsNullOrEmpty(dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                                    X.insertar("update fergeda.f_movimiento set descripcion='" + dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "' where flag='" + dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString() + "';");
                                break;
                        }
                        break;

                }
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                switch (pagina)
                {
                    case 1:
                        Tb.FormatoCondicionalDatos(e);
                        Tb.FilaNegritas(e, 1);
                        Tb.alinear(e, 1, 0);
                        Tb.FilaNegritas(e, 5);
                        Tb.alinear(e, 7, 1);
                        Tb.alinear(e, 8, 1);
                        Tb.FilaNegritas(e, 8);
                        Tb.alinear(e, 10, 1);
                        Tb.alinear(e, 13, 1);
                        Tb.alinear(e, 15, 1);
                        Tb.FilaNegritas(e, 16);
                        Tb.alinear(e, 16, 1);
                        break;

                    case 2:
                        Tb.FilaNegritas(e, 1);
                        Tb.alinear(e, 1, 0);
                        Tb.alinear(e, 5, 1);
                        Tb.FilaNegritas(e, 5);
                        Tb.alinear(e, 6, 1);
                        Tb.alinear(e, 7, 1);
                        break;

                    case 4:
                        Tb.FilaNegritas(e, 1);
                        Tb.alinear(e, 1, 0);
                        Tb.FilaNegritas(e, 5);
                        Tb.alinear(e, 5, 1);
                        Tb.alinear(e, 7, 0);
                        break;

                    case 5:
                        Tb.FilaNegritas(e, 0);
                        Tb.alinear(e, 0, 0);
                        Tb.FilaNegritas(e, 2);
                        Tb.alinear(e, 2, 0);
                        Tb.alinear(e, 6, 1);
                        Tb.FilaNegritas(e, 6);
                        Tb.alinear(e, 7, 1);
                        Tb.FilaNegritas(e, 8);
                        Tb.alinear(e, 8, 1);
                        break;

                    case 9:
                        Tb.FormatoCondicionalDatos(e);
                        Tb.FilaNegritas(e, 1);
                        Tb.alinear(e, 1, 0);
                        Tb.FilaNegritas(e, 12);
                        Tb.alinear(e, 12, 1);              
                        Tb.alinear(e, 13, 1);
                        Tb.alinear(e, 15, 1);
                        Tb.FilaNegritas(e, 16);
                        Tb.alinear(e, 16, 1);
                        Tb.alinear(e, 21, 0);
                        Tb.FilaNegritas(e, 21);
                        break;

                    case 10:
                        Tb.FilaNegritas(e, 0);
                        Tb.alinear(e, 0, 0);
                        Tb.FilaNegritas(e, 4);
                        Tb.alinear(e, 4, 1);
                        Tb.alinear(e, 5, 1);
                        Tb.alinear(e, 6, 1);
                        Tb.FilaNegritas(e, 7);
                        Tb.alinear(e, 7, 1);
                        break;


                }
            }
        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                int Columna = dataGridView1.CurrentCell.ColumnIndex;
                switch (pagina)
                {
                    case 1:
                        switch (Columna)
                        {
                            case 1:
                                if (!string.IsNullOrEmpty(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                                    arc.abirArchivo(rt.OC(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                                break;
                        }
                        break;
                    case 9:
                        switch (Columna)
                        {
                            case 1:
                                if (!string.IsNullOrEmpty(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                                    arc.abirArchivo(rt.OC(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                                break;
                        }
                        break;
                    case 10:
                        switch (Columna)
                        {
                            case 0:
                                if (!string.IsNullOrEmpty(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                                    arc.abirArchivo(rt.SolicitudesCheque(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()));                                    
                                break;                            
                        }
                        break;

                    case 5:
                        switch (Columna)
                        {
                            case 0:
                                if (!string.IsNullOrEmpty(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()))
                                    arc.abirArchivo(rt.SolicitudesCheque(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                                break;
                        }
                        break;
                }
            }
        }
        private void bunifuImageButton8_Click(object sender, EventArgs e)
        {
            switch (pagina)
            {
                case 5:
                    hojaSaldos();
                    break;
            }
        }
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            switch (pagina)
            {
                case 1:CuentasporPagar(fecha());break;
                case 2:CuentasPagadas(fecha2()); break;
                case 3:CargasT();break;
                case 4:cheques(); break;
                case 5:movi(); break;
                case 9:ConsultasCompras(); break;
                case 10:vistaSolicitudes(); break;
                    
            }

        }
        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            switch (pagina)
            {
                case 1: guardarVista();    break;
                case 2: guardarPagos();    break;
                case 4: guardarCheques();  break;
                case 5: guardarSaldos();   break;
            }
        }
        private void limpiar()
        {
            switch (pagina)
            {
                case 1:
                    txt_Oc.Text = "";
                    txt_Factura.Text = txt_Oi.Text = txt_Descripcion.Text = "N/A";
                    lbl_Proveedor.Text = ".";
                    com_tipoCambio.Items.Clear(); com_tipoCambio.Items.Add("MXN"); com_tipoCambio.Items.Add("USD"); com_tipoCambio.Items.Add("EUR");
                    txt_Importe.Text = "0.0";
                    break;
                case 2:
                    txt_Factura2.Text = "";
                    txt_NotaC.Text = txt_SolCheque.Text = "0";
                    txt_importeNC.Text = txt_Pago.Text = txt_Cancelado.Text = "0.0";
                    lbl_Cliente2.Text = ".";
                    break;
            }
        }
        #endregion

        #region TABPAGE
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            CuentasporPagar(fecha());
            pagina = 1;
            lbl_Aviso.Text = " Cuentas por Pagar";
            calcular();
        }
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            CuentasPagadas(fecha2());
            lbl_Aviso.Text = "Cuentas Pagadas";
            pagina = 2;
        }

        private void tabPage3_Enter(object sender, EventArgs e)
        {
            CuentasPagadas(fecha2());
            lbl_Aviso.Text = "Importe Registrado";
            pagina = 3;
        }
        private void tabPage4_Enter(object sender, EventArgs e)
        {
            cheques();
            lbl_Aviso.Text = "Cheques";
            pagina = 4;
        }
        private void tabPage5_Enter(object sender, EventArgs e)
        {
            Console.WriteLine("Entra");
            pagina = 5;
            contadorSaldos = 1;
            lbl_Aviso.Text = "Saldos ";
            movi();

            com_SaldoBancos.Text = X.elem3("SELECT Banco FROM fergeda.f_banks where idf_banks=1;");
        }
        private void tabPage9_Enter(object sender, EventArgs e)
        {
            pagina = 9;
            ConsultasCompras();
            lbl_Aviso.Text = "Orden de Compra ";
        }


        private void tabPage10_Enter(object sender, EventArgs e)
        {
            pagina = 10;
            vistaSolicitudes();
            lbl_Aviso.Text = " Solicitud de Cheques  ";
        }
        #endregion

        #region HOJA1
        int proveedor, diasR = 0, num = 0; string diasP;
        bool selecc = false;
        private void calcular()
        {

            for (int contador = 0; contador < dataGridView1.Rows.Count; contador++)
            {
                int dia = fech.CalcularDias(DateTime.Parse(dataGridView1[6, contador].Value.ToString()), DateTime.Parse(DateTime.Now.ToString("yyyy/MM/dd")));
                if (dia >= 0 && dataGridView1[17, contador].Value.ToString() != "Si")
                {
                    fech.ActualizarDiasFergeda(dia, Convert.ToInt32(X.elem3("SELECT idf_cuentasporpagar FROM  fergeda.f_cuentasporpagar where fk_Factura='" + dataGridView1[5, contador].Value.ToString() + "';")));
                }
                else
                {
                    fech.valNegativosFergeda(dia, Convert.ToInt32(X.elem3("SELECT idf_cuentasporpagar FROM  fergeda.f_cuentasporpagar where fk_Factura='" + dataGridView1[5, contador].Value.ToString() + "';")));
                }
            }
        }
        private string fecha()
        {
            string fec = "";
            if (rb_Vencimiento.Checked)
            {
                if (rb_FacturaA.Checked)                
                    fec = "year(a.fecha_vencimiento)='" + dateTimePicker1.Value.ToString("yyyy") + "'";                
                else if (rb_FacturaM.Checked)                
                    fec = "year(a.fecha_vencimiento)='" + dateTimePicker1.Value.ToString("yyyy") + "' and month(a.fecha_vencimiento)='" + dateTimePicker1.Value.ToString("MM") + "' ";
                else if (rb_FacturaD.Checked)                
                    fec = "a.fecha_vencimiento='" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "'"; 
            }
            if (rb_Revision.Checked)
            {
                if (rb_FacturaA.Checked)                
                    fec = "year(a.fecha_revision)='" + dateTimePicker1.Value.ToString("yyyy") + "'";                
                else if (rb_FacturaM.Checked)               
                    fec = "year(a.fecha_revision)='" + dateTimePicker1.Value.ToString("yyyy") + "' and month(a.fecha_revision)='" + dateTimePicker1.Value.ToString("MM") + "' ";
                else if (rb_FacturaD.Checked)                
                    fec = "a.fecha_revision='" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "'";                
            }
            if (ch_Proveedor.Checked)
            {
                if (!string.IsNullOrEmpty(com_Proveedores.Text))
                {
                    string datpro = X.elem3("SELECT idf_proveedor FROM fergeda.f_proveedores where razon_social='" + com_Proveedores.Text + "';");
                    fec = fec + " and c.idf_proveedor='" + Convert.ToInt32(datpro) + "'";
                }
            }

            return fec;
        }

        private string pag()
        {
            string p = " and a.pagado<>'Si'";
            if (rb_NoPagado.Checked) { p = " and a.pagado<>'Si'"; }
            else if (rb_Pagado.Checked) { p = " and a.pagado<>'No' and a.pagado<>'V'"; }
            else if (rb_Todo.Checked) { p = ""; }
            return p;
        }

        private void CuentasporPagar(string comple)
        {
            selecc = false;
            dataGridView1.Columns.Clear();
            com_Proveedores.Items.Clear();
            Tb.elimElemConConsulta(dataGridView1);

            X.tablaDatos(dataGridView1, "SELECT c.razon_social 'Proveedor', a.fk_ordencompra 'Orden de Compra', e.nombre 'Comprador', a.descripcion 'Descripción'," +
                " a.orden_impresion 'Orden de Impresión', b.factura 'Factura', a.fecha_vencimiento 'Fecha de Vencimiento', format(a.Vencido,2) 'Vencido'," +
                "format(b.importe,2) 'Importe Factura', a.tipo_cambio 'Moneda', a.costo 'Tipo de Cambio', a.fecha_revision 'Fecha de Revisión'," +
                "CONCAT(c.dias, ' ', c.condiciones) 'Crédito', a.notas_credito 'Notas de Crédito', a.intercambios 'Intercambios', a.cancelado 'Cancelado'," +
                " a.saldo 'Saldo', a.pagado 'Pagado', a.cobro 'Tipo de Pago', a.referencia 'Referencia' FROM fergeda.f_cuentasporpagar a INNER JOIN " +
                " fergeda.f_ban_cuentasporpagar b ON a.idf_cuentasporpagar = b.idf_ban_cuentasporpagar INNER JOIN fergeda.f_proveedor c ON " +
                "b.fk_proveedor = c.idf_proveedor inner join fergeda.c_ordencompra d on a.fk_ordencompra = d.idc_ordencompra inner join " +
                "fergeda.r_empleado e on d.fk_empleado = e.idr_empleado where " + comple + " " + pag() + " order by a.idf_cuentasporpagar desc; ");
           
            X.comb("SELECT razon_social FROM fergeda.f_proveedores  order by razon_social asc;", com_Proveedores);
            Tb.colorStatus(dataGridView1, "Pagado", "Si", 180, 245, 192);
            Tb.colorStatus(dataGridView1, "Pagado", "V", 252, 239, 135);
   
            selecc = true;
           
        }
        private void txt_Oc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                proveedor = Convert.ToInt32(X.elem3("SELECT fk_proveedor FROM fergeda.c_listadocompras where fk_ordencompra='" + Convert.ToInt32(txt_Oc.Text) + "';"));
                lbl_Proveedor.Text = X.elem3("SELECT razon_social FROM fergeda.f_proveedores where idf_proveedor='" + proveedor + "';");
                X.text("SELECT orden_impres FROM fergeda.c_listadocompras where fk_ordencompra='" + Convert.ToInt32(txt_Oc.Text) + "';", txt_Oi);
                string text = X.elem3("SELECT descripcion FROM fergeda.c_listadocompras where fk_ordencompra='" + Convert.ToInt32(txt_Oc.Text) + "';");
                if (txt_Descripcion.Text.Length > 100)
                {
                    txt_Descripcion.Text = text.Substring(0, 100);
                }
                else { txt_Descripcion.Text = text; }

                diasP = X.elem3("SELECT dias FROM fergeda.f_proveedor where idf_proveedor='" + proveedor + "';");
            }
        }


        private void guardarVista()
        {
            if (!string.IsNullOrEmpty(txt_Oc.Text) && !string.IsNullOrEmpty(txt_Oi.Text) && !string.IsNullOrEmpty(txt_Importe.Text)
                       && !string.IsNullOrEmpty(txt_Factura.Text) && !string.IsNullOrEmpty(com_tipoCambio.Text))
            {
                if (Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.f_ban_cuentasporpagar where factura='" + txt_Factura.Text + "';")) == 0)
                {
                    X.insertar("INSERT INTO fergeda.f_cuentasporpagar (fk_ordencompra, orden_impresion, fk_Factura, fecha_vencimiento, fecha_revision, " +
                    "dias_restantes,descripcion,tipo_Cambio,saldo) VALUES ('" + Convert.ToInt32(txt_Oc.Text) + "', '" + txt_Oi.Text + "', '" + txt_Factura.Text + "', '" +
                    fech.diasFergeda(diasP, dateTimePicker2).ToString("yyyy/MM/dd") + "', '" + dateTimePicker2.Value.ToString("yyyy/MM/dd") + "', '" + diasR + "','" + txt_Descripcion.Text +
                    "','" + com_tipoCambio.Text + "','" + float.Parse(txt_Importe.Text) + "');");

                    X.insertar("INSERT INTO fergeda.f_ban_cuentasporpagar (idf_ban_cuentasporpagar, factura, fk_proveedor,importe,fecha,fk_empleado) VALUES ('" +
                            Convert.ToInt32(X.elem3("SELECT max(idf_cuentasporpagar) FROM fergeda.f_cuentasporpagar;")) + "', '" + txt_Factura.Text
                            + "', '" + proveedor + "','" + float.Parse(txt_Importe.Text) + "','" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleado + "');");
                    CuentasporPagar(fecha());
                    limpiar();
                    txt_Factura.BackColor = Color.White;
                }
                else
                {
                    msg.cargarEl("no-molestar", "Error ", "Esta factura ya se encuentra ingresada"); msg.ShowDialog();
                    txt_Factura.BackColor = Color.Coral;
                }
            }
            else { msg.cargarEl("no-molestar", "Error ", "Completa todos los Campos"); msg.ShowDialog(); }
        }
        private void bunifuImageButton26_Click(object sender, EventArgs e)
        {
            if (num.Equals(0))
            {
                num++;
                lb_Valida.Visible = true;
            }
            else { num = 0; lb_Valida.Visible = false; }
        }
        #endregion

        #region HOJA2
        string fact = "";

        private void txt_Factura2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txt_Factura2.Text))
                {
                    if (Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.f_cuentaspagadas where factura='" + txt_Factura2.Text + "';")).Equals(0))
                    {
                        proveedor = Convert.ToInt32(X.elem3("SELECT fk_proveedor FROM fergeda.f_ban_cuentasporpagar where factura='" + txt_Factura2.Text + "';"));
                        lbl_Cliente2.Text = X.elem3("SELECT razon_social FROM fergeda.f_proveedores where idf_proveedor='" + proveedor + "';");
                        txt_Pago.Text = X.elem3("SELECT importe FROM fergeda.f_ban_cuentasporpagar where factura='" + txt_Factura2.Text + "';");
                        txt_NotaC.Text = txt_SolCheque.Text = "0";
                    }
                    else
                    {
                        msg.cargarEl("no-molestar ", "Error ", "Ya se encuentra esta factura Ingresada"); msg.ShowDialog();
                    }
                }
                else
                {
                    msg.cargarEl("no-molestar", "Error ", "No deje campos vacios"); msg.ShowDialog();
                }
            }
        }


        private void CuentasPagadas(string comple)
        {
            selecc = false;
            dataGridView1.Columns.Clear();
            com_Proveedor2.Items.Clear();
            Tb.elimElemConConsulta(dataGridView1);
            X.tablaDatos(dataGridView1, "SELECT a.idf_cuentaspagadas 'Folio',a.factura 'Factura',a.nota_Credito 'Nota de Crédito',a.sol_cheque 'Solicitud de Cheque'," +
                "b.razon_social 'Proveedor',FORMAT(a.pago,2) 'Pago', format(a.importe_Nc,2) 'Importe Nota de Crédito', format(a.importe_Cancelado,2) 'Importe Cancelado', a.fecha 'Fecha'," +
                " a.complemento 'Complemento', c.referencia 'Referencia' FROM fergeda.f_cuentaspagadas as a inner join fergeda.f_proveedores b on " +
                "a.fk_proveedor = b.idf_proveedor inner join fergeda.f_cuentasporpagar c on a.factura = c.fk_Factura where " + comple + " order by a.idf_cuentaspagadas desc; ");
            txt_Factura2.Text = fact;
            X.comb("SELECT razon_social FROM fergeda.f_proveedores order by razon_social asc;", com_Proveedor2);
            Tb.colorStatus(dataGridView1, "Complemento", "-", 252, 239, 135);
            selecc = true;
        }
        private string fecha2()
        {

            string fecha = "";
            if (rb_Fecha.Checked)
            {
                if (rb_A2.Checked)                
                    fecha = "year(a.fecha) = '" + dateTimePicker5.Value.ToString("yyyy") + "'";                
                else if (rb_D2.Checked)                
                    fecha = "year(a.fecha) = '" + dateTimePicker5.Value.ToString("yyyy") + "' and month(a.fecha) = '" + dateTimePicker5.Value.ToString("MM") + "'";                
                else if (rb_DiaCP.Checked)                
                    fecha = "a.fecha='" + dateTimePicker5.Value.ToString("yyyy/MM/dd") + "'";                
            }
            else if (rb_Factura.Checked)            
                fecha = "a.factura='" + txt_BusquedaP.Text + "'";            
            else if (rb_Solicitud_Cheque.Checked)            
                fecha = "a.sol_cheque='" + txt_BusquedaP.Text + "'";            
            else if (rb_Proveedor2.Checked)
            {
                if (rb_A2.Checked)                
                    fecha = "year(a.fecha) = '" + dateTimePicker5.Value.ToString("yyyy") + "'";                
                else if (rb_D2.Checked)                
                    fecha = "year(a.fecha) = '" + dateTimePicker5.Value.ToString("yyyy") + "' and month(a.fecha) = '" + dateTimePicker5.Value.ToString("MM") + "'";                
                else if (rb_DiaCP.Checked)                
                    fecha = "a.fecha='" + dateTimePicker5.Value.ToString("yyyy/MM/dd") + "'";
                
                fecha = fecha + "and  b.Razon_Social='" + com_Proveedor2.Text + "'";
            }
            return fecha;
        }

        private void guardarPagos()
        {
            if (!string.IsNullOrEmpty(txt_Factura2.Text) && !string.IsNullOrEmpty(txt_NotaC.Text) && !string.IsNullOrEmpty(txt_importeNC.Text) &&
                        !string.IsNullOrEmpty(txt_SolCheque.Text) && !string.IsNullOrEmpty(txt_Cancelado.Text) && !string.IsNullOrEmpty(txt_Referencia.Text))
            {
                if (Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.f_cuentasporpagar where fk_Factura='" + txt_Factura2.Text + "';")) > 0)
                {
                    X.insertar("INSERT INTO fergeda.f_cuentaspagadas (factura,nota_Credito, sol_cheque,fk_proveedor, pago, importe_Nc, importe_Cancelado, fecha) VALUES " +
                    "('" + txt_Factura2.Text + "','" + txt_NotaC.Text + "','" + Convert.ToInt32(txt_SolCheque.Text) + "', '" + proveedor + "', '" + float.Parse(txt_Pago.Text)
                    + "', '" + float.Parse(txt_importeNC.Text) + "', '" + float.Parse(txt_Cancelado.Text) + "', '" + dateTimePicker4.Value.ToString("yyyy/MM/dd") + "');");

                    actualizarSaldos(txt_Factura2.Text, proveedor, float.Parse(txt_importeNC.Text), float.Parse(txt_Cancelado.Text), com_Pago.Text, txt_Referencia.Text);
                    CuentasPagadas(fecha2());
                    limpiar();
                }
                else { msg.cargarEl("stop", "Error ", "No se encuentra la factura"); msg.ShowDialog(); }
            }
            else { msg.cargarEl("stop", "Error ", "Completa todos los Campos"); msg.ShowDialog(); }
        }
        private void actualizarSaldos(string fac, int prove, float not, float cancelado, string tipoPago, string referencia)
        {
            string id_Pafo = X.elem3("SELECT idf_ban_cuentasporpagar FROM fergeda.f_ban_cuentasporpagar where factura='" + fac + "' and fk_proveedor='" + prove + "';");
            int idpago = Convert.ToInt32(id_Pafo);
            float pago = float.Parse(X.elem3(" SELECT sum(pago+importe_Nc+importe_Cancelado)  FROM fergeda.f_cuentaspagadas where factura='" + fac + "'  and fk_proveedor=" + prove + ";"));
            float impor = float.Parse(X.elem3("SELECT importe FROM fergeda.f_ban_cuentasporpagar where idf_ban_cuentasporpagar='" + idpago + "';"));
            X.insertar("UPDATE fergeda.f_cuentasporpagar SET saldo='" + (impor - pago) + "', notas_credito='" + not + "', cancelado='" + cancelado + "',pagado='No'" +
                ",Cobro='" + tipoPago + "',referencia='" + referencia + "' WHERE idf_cuentasporpagar='" + idpago + "';");
        }

        #endregion

        #region HOJA3
        private void ImporteRegistrado()
        {
            selecc = false;
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);
            X.importePaFer(dataGridView1, "call fergeda.f_importePagar(" + Convert.ToInt32(dateTimePicker6.Value.Year) + ");", "SELECT sum(importe) 'impor' FROM fergeda.f_ban_cuentasporpagar where year(fecha)='" + Convert.ToInt32(dateTimePicker6.Value.Year) + "' ;");
            Tb.colorStatus(dataGridView1, "Mes", "", 252, 239, 135);

        }
        private void importePagado()
        {
            selecc = false;
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);

            X.importePagos(dataGridView1, "SELECT month(fecha) 'mes',week(fecha) 'Semana',fecha ,round(sum(pago),2) FROM fergeda.f_cuentaspagadas where year(fecha)='" + dateTimePicker6.Value.ToString("yyyy") + "' group by fecha asc ; ");
            X.importeApag2(dataGridView1, "SELECT round(sum(a.pago),2)'Pago' FROM fergeda.f_cuentaspagadas a where year(a.fecha) = '" + dateTimePicker6.Value.ToString("yyyy") + "'; ");
            Tb.colorStatus(dataGridView1, "Mes", "", 252, 239, 135);
        }

        private void reportePagos()
        {
            selecc = false;
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);
            X.repPagos(dataGridView1, "SELECT  MONTH(fecha) 'Mes', WEEK(fecha, 1) 'Semana Pago', fecha 'Fecha de Vencimiento', ROUND(SUM(pago + " +
                "importe_Nc + importe_Cancelado),2) 'Saldo Pendiente de Pago' FROM  fergeda.f_cuentaspagadas WHERE YEAR(fecha) = '" + dateTimePicker6.Value.ToString("yyyy") + "' GROUP BY WEEK(fecha, 0) ASC; ");
            Tb.colorStatus(dataGridView1, "Mes", "", 252, 239, 135);
        }
        private void imporA_Pagar()
        {
            selecc = false;
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);
            X.importePaFer(dataGridView1, "SELECT  month(fecha_vencimiento) 'Mes',week(fecha_vencimiento) 'Semana', fecha_vencimiento 'Fecha de Vencimiento'," +
                " round(sum(saldo),2) 'Saldo' FROM fergeda.f_cuentasporpagar  where year(fecha_vencimiento) = '" + dateTimePicker6.Value.ToString("yyyy") + "' group by" +
                " week(fecha_vencimiento)  asc  order by fecha_vencimiento asc; ", "SELECT round(sum(saldo),2) 'impor' FROM    fergeda.f_cuentasporpagar where year(fecha_vencimiento)='" + dateTimePicker6.Value.ToString("yyyy") + "';");

            Tb.colorStatus(dataGridView1, "Mes", "", 252, 239, 135);
        }

        private void PagosProvee()
        {
            X.pagProveedores(dataGridView1, "SELECT c.razon_social,sum(a.saldo) FROM    fergeda.f_cuentasporpagar a inner join fergeda.f_ban_cuentasporpagar " +
               "b on a.idf_cuentasporpagar = b.idf_ban_cuentasporpagar inner join fergeda.f_proveedores c on b.fk_proveedor = c.idf_proveedor where a.pagado <> 'Si' group by c.razon_social desc order by c.razon_social asc; ");
        }

  

        private async void rotacionPagos()
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);

            rot.llenarTb(dataGridView1, Convert.ToInt32(dateTimePicker5.Value.ToString("yyyy")));
            Task task = new Task(ColorFact);
            task.Start();
            await task;

           
        }
        private void ColorFact()
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                if (dataGridView1[0, i].Value.ToString().Equals("") || dataGridView1[0, i].Value.ToString().Equals(null))
                {

                }
                else
                {
                    if (dataGridView1[0, i].Value.ToString().Equals("Cuenta de Importe Factura") || dataGridView1[0, i].Value.ToString().Equals("Suma de Importe Factura"))
                    {
                        dataGridView1[0, i].Style.BackColor = Color.FromArgb(252, 239, 135);
                    }/**/
                    if (dataGridView1[0, i].Value.ToString().Equals("Total General"))
                    {
                        dataGridView1[0, i].Style.BackColor = Color.FromArgb(180, 255, 171);
                    }

                    if (dataGridView1[0, i].Value.ToString().Equals("Dias de Crédito"))
                    {
                        for (int j = 0; j < 14; j++)
                        {
                            if (dataGridView1[j, i].Value.ToString().Equals("") || dataGridView1[j, i].Value.ToString().Equals(null))
                            {

                            }
                            else
                            {
                                if (!dataGridView1[j, i].Value.ToString().Equals(""))
                                {
                                    dataGridView1[j, i].Style.BackColor = Color.FromArgb(252, 239, 135);
                                }
                            }
                        }
                    }


                }
            }
        }

 
        private void CargasT()
        {
            if (rb_importeRegistrado.Checked)
                ImporteRegistrado();
            else if (rb_importePagar.Checked)
                imporA_Pagar();
            else if (rb_reportePagos.Checked)
                reportePagos();
            else if (rb_importePagadol.Checked)
                importePagado();
            else if (rb_ResumenProve.Checked)
                PagosProvee();
            else if (rb_rotacionpagos.Checked)
                rotacionPagos();
        }
        #endregion

        #region HOJA4
        private void cheques()
        {
            //selecc = false;
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);
            com_CheProveedor.Items.Clear(); com_CheSolicitante.Items.Clear(); com_ProveedorChe2.Items.Clear();


            X.tablaDatos(dataGridView1, " SELECT a.Estatus,idf_cheque 'Cheque',a.fecha_Gen 'Fecha Generada',b.razon_social 'Proveedor',a.concepto 'Concepto' ,format(a.importe,2) 'Importe',a.fecha_Ven 'Fecha Vencimiento',a.solicitud 'Solicitud de Cheque', c.nombre 'Nombre', " +
                "a.Notas,a.fecha_Cobro  'Fecha de Cobro', a.Complemento, Motivo_Cancelado 'Motivo de Cancelación' FROM fergeda.f_cheque a inner join fergeda.f_proveedores b on " +
                "a.fk_proveedor = b.idf_proveedor inner join fergeda.r_empleado c on a.fk_solicitante = c.idr_empleado " +
                "where year(a.fecha_Gen) = '" + dateTimePicker9.Value.Year + "' order by idf_cheque desc; ");


            com_CheProveedor.Items.Clear(); com_CheSolicitante.Items.Clear();
            txt_ConceptoCheq.Text = txt_importeCheq.Text = com_CheProveedor.Text = ""; txt_SolicitudCheq.Text = "0";

            X.comb("SELECT razon_social FROM fergeda.f_proveedores order by razon_social asc;", com_CheProveedor);
            X.comb("SELECT razon_social FROM fergeda.f_proveedores order by razon_social asc;", com_ProveedorChe2);
            X.comb("SELECT nombre FROM fergeda.c_solicitante;", com_CheSolicitante);
            label31.Text = X.elem3(" select max(idf_cheque)+1 from fergeda.f_cheque;");

            Tb.colorStatus(dataGridView1, "Estatus", "Pendiente", 252, 239, 135);
            Tb.colorStatus(dataGridView1, "Estatus", "Transito", 245, 165, 184);
            Tb.colorStatus(dataGridView1, "Estatus", "Cobrado", 147, 243, 130);
            Tb.colorStatus(dataGridView1, "Estatus", "Cancelado", 245, 69, 56);

            actualizarSaldos(lbl_Transito, lbl_Pendientes, lbl_Saldos, lbl_SaldoBanco2);
            //selecc = true;

        }
        private void actualizarSaldos(Label transito, Label Pendiente, Label saldos, Label saldoBanco)
        {
            string tras = X.elem3("SELECT sum(importe) FROM fergeda.f_cheque where Estatus='Transito';");
            if (tras.Equals("") || tras.Equals(null)) { tras = "0"; } else { tras = X.elem3("SELECT sum(importe) FROM fergeda.f_cheque where Estatus='Transito';"); }
            transito.Text = "" + float.Parse(tras).ToString("N2");
            string pen = X.elem3("SELECT sum(importe) FROM fergeda.f_cheque where Estatus='Pendiente';");
            if (pen.Equals("") || pen.Equals(null)) { pen = "0"; }
            else { pen = X.elem3("SELECT sum(importe) FROM fergeda.f_cheque where Estatus='Pendiente';"); }
            Pendiente.Text = "" + float.Parse(pen).ToString("N2");
            string saldo = X.elem3("SELECT max(saldo) FROM fergeda.f_movimiento;");
            saldos.Text = "" + float.Parse(saldo).ToString("N2");
            saldoBanco.Text = tr.Mostrar(X.elem3("SELECT saldo_disponoble FROM fergeda.f_banks where idf_banks=1;"), tr.Formato);
            X.insertar("UPDATE fergeda.f_banks SET saldo_disponoble='" + tr.Contraseña(transito.Text, tr.Formato) + "' WHERE idf_banks='2';");
            X.insertar("UPDATE fergeda.f_banks SET saldo_disponoble='" + tr.Contraseña(Pendiente.Text, tr.Formato) + "' WHERE idf_banks='3';");
        }

        private void guardarCheques()
        {
            if (!string.IsNullOrEmpty(com_CheProveedor.Text) && !string.IsNullOrEmpty(com_CheSolicitante.Text) && !string.IsNullOrEmpty(txt_importeCheq.Text) &&
                       !string.IsNullOrEmpty(txt_SolicitudCheq.Text) && !string.IsNullOrEmpty(txt_ConceptoCheq.Text))
            {
                if (Convert.ToInt32(txt_SolicitudCheq.Text) >= 0 && Convert.ToInt32(txt_SolicitudCheq.Text) <= Convert.ToInt32(X.elem3("SELECT max(idf_solicitudcheque) FROM fergeda.f_solicitudcheque;")))
                {
                    if (txt_SolicitudCheq.Text.Equals("0"))
                    {
                        X.insertar("call fergeda.f_cheques('" + dateTimePicker10.Value.ToString("yyyy/MM/dd") + "','" + Convert.ToInt32(X.elem3("SELECT idf_proveedor FROM fergeda.f_proveedores where razon_social='" +
                        com_CheProveedor.Text + "';")) + "','" + txt_ConceptoCheq.Text + "','" + double.Parse(txt_importeCheq.Text) + "','" + Convert.ToInt32(txt_SolicitudCheq.Text) + "','" + Convert.ToInt32(X.elem3("SELECT idr_empleado FROM fergeda.c_solicitante where nombre='" + com_CheSolicitante.Text + "';")) + "');");
                    }
                    else
                    {
                        if (Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.f_cheque where solicitud='" + txt_SolicitudCheq.Text + "';")) > 0)
                        {
                            if (X.elem3("SELECT Estatus FROM litopolis_v5.fin_cheque where solicitud='" + txt_SolicitudCheq.Text + "';").Equals("Cancelado"))
                            {
                                X.insertar("call fergeda.f_cheques('" + dateTimePicker10.Value.ToString("yyyy/MM/dd") + "','" + Convert.ToInt32(X.elem3("SELECT idf_proveedor FROM fergeda.f_proveedores where razon_social='" +
                                com_CheProveedor.Text + "';")) + "','" + txt_ConceptoCheq.Text + "','" + double.Parse(txt_importeCheq.Text) + "','" + Convert.ToInt32(txt_SolicitudCheq.Text) + "','" + Convert.ToInt32(X.elem3("SELECT idr_empleado FROM fergeda.c_solicitante where nombre='" + com_CheSolicitante.Text + "';")) + "');");
                            }
                            else { msg.cargarEl("bell(1)", "No permitido", "Debe de Cancelar anterior Para continuar"); msg.ShowDialog(); }
                        }
                        else
                        {
                            X.insertar("call fergeda.f_cheques('" + dateTimePicker10.Value.ToString("yyyy/MM/dd") + "','" + Convert.ToInt32(X.elem3("SELECT idf_proveedor FROM fergeda.f_proveedores where razon_social='" +
                            com_CheProveedor.Text + "';")) + "','" + txt_ConceptoCheq.Text + "','" + double.Parse(txt_importeCheq.Text) + "','" + Convert.ToInt32(txt_SolicitudCheq.Text) + "','" + Convert.ToInt32(X.elem3("SELECT idr_empleado FROM fergeda.c_solicitante where nombre='" + com_CheSolicitante.Text + "';")) + "');");
                        }
                    }
                    cheques();

                }
                else { msg.cargarEl("no-molestar", "Error", "Ingrese una solicitud Valida"); msg.ShowDialog(); }
            }
        }

        private void bunifuImageButton25_Click(object sender, EventArgs e)
        {
            new chequePrint().Show();
        }


      
        #endregion

        #region HOJA 5
        int contadorSaldos = 1;

       

        string bancoAsignado = "";



        private void movi()
        {
            com_SaldoBancos.Items.Clear();
            selecc = false;
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);
            X.comb("SELECT Banco FROM fergeda.f_banks;", com_SaldoBancos);
            actualizarSaldos(lbl_Transito2, lbl_Pendientes2, lbl_Saldos2, lbl_SaldoBanco);

            bancoAsignado = (string.IsNullOrEmpty(com_SaldoBancos.Text) ? "1" : X.elem3("SELECT idf_banks FROM fergeda.f_banks where Banco='" + com_SaldoBancos.Text + "';"));
        
            cargarConsultaSaldos();
        }


        private void cargarConsultaSaldos()
        {
            bancoAsignado = (string.IsNullOrEmpty(com_SaldoBancos.Text) ? "1" : X.elem3("SELECT idf_banks FROM fergeda.f_banks where Banco='" + com_SaldoBancos.Text + "';"));
            string comple = "", comple2 ="";
            if (rb_DiaSaldo.Checked)
                comple = "fecha='" + dateTimePicker12.Value.ToString("yyyy/MM/dd") + "'";
            else if (rb_AñoSaldo.Checked)
                comple = "year(fecha)='" + dateTimePicker12.Value.ToString("yyyy") + "'";
            else if (rb_MesSaldo.Checked)
                comple = "year(fecha)='" + dateTimePicker12.Value.ToString("yyyy") + "' and month(fecha)='" + dateTimePicker12.Value.ToString("MM") + "'";

            comple2 = rb_cliente.Checked ? " and cliente like '%" + textBox1.Text + "%' " : " and descripcion like '%"+ textBox1.Text + "%' ";


            string min = X.elem3("SELECT min(flag)-1 FROM fergeda.f_movimiento where "+comple+ "and fk_bancoMostrado='"+ bancoAsignado + "';");
            string max = X.elem3("SELECT max(flag) FROM fergeda.f_movimiento where " + comple + " and fk_bancoMostrado='" + bancoAsignado + "';");
            X.tablaDatos(dataGridView1, "SELECT if(fk_cheque is null,'0',fk_cheque) 'Cheque', if(transferencia is null,'0',transferencia)'Transferencia', " +
                "if(factura is null,'0',factura)'factura', if(fk_bancos is null,'0',fk_bancos) 'Bancos', cliente 'Cliente', descripcion 'Descripción', " +
                "format(cargo, 2) 'Cargo', format(abono, 2)'Abono', format(saldo, 2)'Saldo', fecha 'Fecha', flag 'ID' FROM fergeda.f_movimiento where " +
                "(idf_movimiento between " + min + " and " + max + ") and fk_bancoMostrado='" + bancoAsignado + "'"+comple2+";");

        }


        private void hojaSaldos()
        {
            if (dataGridView1.Rows.Count > 0)
            {


                int Columna = dataGridView1.CurrentCell.ColumnIndex;
                string dat = dataGridView1[Columna, dataGridView1.CurrentCell.RowIndex].Value.ToString();

               
                string banco = (string.IsNullOrEmpty(com_SaldoBancos.Text) ? "1" : X.elem3("SELECT idf_banks FROM fergeda.f_banks where Banco='" + com_SaldoBancos.Text + "';"));

                /***/
                string comple = "";
                string fecha = FechaRes(banco);

                if (rb_DiaSaldo.Checked)
                    comple = "fecha='" + DateTime.Parse(fecha).ToString("yyyy/MM/dd") + "'";
                else if (rb_AñoSaldo.Checked)
                    comple = "year(fecha)='" + DateTime.Parse(fecha).ToString("yyyy") + "'";
                else if (rb_MesSaldo.Checked)
                    comple = "year(fecha)='" + DateTime.Parse(fecha).ToString("yyyy") + "' and month(fecha)='" + DateTime.Parse(fecha).ToString("MM") + "'";

                if (!X.elem3("SELECT count(*) FROM fergeda.f_movimiento where fk_bancoMostrado='" + banco + "';").Equals("0"))
                {
                    int max = Convert.ToInt32(X.elem3("SELECT max(flag) FROM fergeda.f_movimiento where fk_bancoMostrado='" + banco + "' and " + comple + ";"));
                    int min = Convert.ToInt32(dataGridView1[10, dataGridView1.CurrentCell.RowIndex].Value.ToString());

                    dataGridView1.Columns.Clear();
                    Tb.elimElemConConsulta(dataGridView1);
                    X.tablaDatos(dataGridView1, "SELECT fecha 'Fecha',if(fk_cheque is null,if (transferencia is null,'0',Concat('B-', transferencia)),if (fk_cheque is null,'0',fk_cheque)) 'Cheque'," +
                        "  if (factura is null,if (fk_bancos is null,'0',fk_bancos),if (factura is null,'0',concat('F-', factura))) 'Bancos',cliente,descripcion,format(cargo, 2) 'Cargo'" +
                        ",format(abono, 2)'Abono',  format(saldo, 2) 'Saldo' FROM fergeda.f_movimiento" +
                        " where fk_bancoMostrado = '" + banco + "' and flag between '" + min + "' and '" + max + "' order by flag asc; ");


                    flu.generaPDF(dataGridView1, "" + banco);
                }
            }
            string FechaRes(string banco)
            {
                string fecha = "";
                if (dateTimePicker12.Value.ToString("yyyy/MM/dd").Equals(DateTime.Now.ToString("yyyy/MM/dd")))
                {
                    if (Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.f_movimiento where fecha='" + dateTimePicker12.Value.ToString("yyyy/MM/dd") + "' and fk_bancoMostrado='" + banco + "';")) > 0)                    
                        fecha = dateTimePicker12.Value.ToString("yyyy/MM/dd");                    
                    else                    
                        fecha = X.elem3("SELECT max(fecha) FROM fergeda.f_movimiento  where fk_bancoMostrado='" + banco + "';");
                    
                }
                else
                {
                    if (Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.f_movimiento where fecha='" + dateTimePicker12.Value.ToString("yyyy/MM/dd") + "' and fk_bancoMostrado='" + banco + "';")) > 0)                    
                        fecha = dateTimePicker12.Value.ToString("yyyy/MM/dd");                    
                    else
                    {
                        fecha = dateTimePicker12.Value.ToString("yyyy/MM/dd");
                        for (int i = 0; i < 30; i++)
                        {
                            fecha = RestarDia(fecha);
                            if (Convert.ToInt32(X.elem3("SELECT count(*) FROM fergeda.f_movimiento where fecha='" + fecha + "' and fk_bancoMostrado='" + banco + "';")) > 0)
                                break;
                        }
                    }
                }
                return fecha;
            }
            string RestarDia(string fecha) => X.elem3("select DATE_SUB('" + fecha + "',INTERVAL " + 1 + " DAY);");
        }

        private void guardarSaldos()
        {
            if (!string.IsNullOrEmpty(txt_BusquedaS.Text) && !string.IsNullOrEmpty(txt_MontoS.Text) && !string.IsNullOrEmpty(txt_ClienteS.Text) && !string.IsNullOrEmpty(txt_DescripcionS.Text))
            {
                if (com_SaldoBancos.Text == "")
                {
                    bancoAsignado = X.elem3("select Banco from fergeda.f_banks where fergeda.f_banks='1';");
                    bancoAsignado = X.elem3("select idf_banks from fergeda.f_banks where Banco='" + bancoAsignado + "';");
                }
                else { bancoAsignado = X.elem3("select idf_banks from fergeda.f_banks where Banco='" + com_SaldoBancos.Text + "';"); }
                int maxF = Convert.ToInt32(X.elem3("SELECT max(flag)+1 FROM fergeda.f_movimiento where fk_bancoMostrado='" + bancoAsignado + "';"));
                double cantf = 0;
                string val = X.elem3("SELECT saldo FROM fergeda.f_movimiento where idf_movimiento=(select max(idf_movimiento)FROM fergeda.f_movimiento where fk_bancoMostrado='" + Convert.ToInt32(bancoAsignado) + "');");
                //MessageBox.Show(""+val);
                if (val.Equals(null) || val.Equals(""))
                {
                    cantf = 0;
                    //MessageBox.Show("");
                }
                else { cantf = double.Parse(val); }
                if (rb_ChequeS.Checked || rb_Transferencia.Checked)
                {
                    X.insertar("INSERT INTO fergeda.f_movimiento (" + opG + ", cliente, descripcion, cargo, saldo, fecha, fk_bancoMostrado,flag) VALUES ('" + txt_BusquedaS.Text + "', '" + txt_ClienteS.Text + "', '" + txt_DescripcionS.Text + "', '" + double.Parse(txt_MontoS.Text) + "', '" + (cantf - double.Parse(txt_MontoS.Text)) + "', '" + dateTimePicker12.Value.ToString("yyyy/MM/dd") + "', '" + Convert.ToInt32(bancoAsignado) + "','" + maxF + "');");
                    movi();
                }
                else if (rb_FacturasSal.Checked)
                {
                    X.insertar("INSERT INTO fergeda.f_movimiento (" + opG + ", cliente, descripcion, abono, saldo, fecha, fk_bancoMostrado,flag) VALUES ('" + txt_BusquedaS.Text + "', '" + txt_ClienteS.Text + "', '" + txt_DescripcionS.Text + "', '" + double.Parse(txt_MontoS.Text) + "', '" + (cantf + double.Parse(txt_MontoS.Text)) + "', '" + dateTimePicker12.Value.ToString("yyyy/MM/dd") + "', '" + Convert.ToInt32(bancoAsignado) + "','" + maxF + "');");

                    movi();
                }

                else if (rb_BancosS.Checked)
                {
                    insertarBancos(Convert.ToInt32(bancoAsignado));
                    if (rb_Abono.Checked)
                    {
                        X.insertar("INSERT INTO fergeda.f_movimiento (" + opG + ", cliente, descripcion, abono, saldo, fecha, fk_bancoMostrado,flag) VALUES ('" + txt_BusquedaS.Text + "', '" + txt_ClienteS.Text + "', '" + txt_DescripcionS.Text + "', '" + double.Parse(txt_MontoS.Text) + "', '" + (cantf + double.Parse(txt_MontoS.Text)) + "', '" + dateTimePicker12.Value.ToString("yyyy/MM/dd") + "', '" + Convert.ToInt32(bancoAsignado) + "','" + maxF + "');");
                        movi();
                    }
                    else
                    {

                        X.insertar("INSERT INTO fergeda.f_movimiento (" + opG + ", cliente, descripcion, cargo, saldo, fecha, fk_bancoMostrado,flag) VALUES ('" + txt_BusquedaS.Text + "', '" + txt_ClienteS.Text + "', '" + txt_DescripcionS.Text + "', '" + double.Parse(txt_MontoS.Text) + "', '" + (cantf - double.Parse(txt_MontoS.Text)) + "', '" + dateTimePicker12.Value.ToString("yyyy/MM/dd") + "', '" + Convert.ToInt32(bancoAsignado) + "','" + maxF + "');");
                        movi();

                    }
                }

                movi();
                limpiarSaldos();

            }
            else { msg.cargarEl("Stop", "Error", "Complete los campos para \ncontinuar"); msg.ShowDialog(); }

        }

        private void insertarBancos(int banco)
        {
            string tipo = "Cargo";
            if (rb_Cargo.Checked) { tipo = "Cargo"; }
            else if (rb_Abono.Checked) { tipo = "Abono"; }
            X.insertar("INSERT INTO fergeda.f_traspasos (concepto, descripcion, tipo, fk_Banco, monto) VALUES ('" + txt_ClienteS.Text + "', '" + txt_DescripcionS.Text + "', '" + tipo + "', '" + bancoAsignado + "', '" + float.Parse(txt_MontoS.Text) + "');");
        }
        private void limpiarSaldos()
        {
            txt_DescripcionS.Text = txt_ClienteS.Text = txt_MontoS.Text = "";
            RadioSaldo((rb_ChequeS.Checked ? 1 : (rb_Transferencia.Checked ? 2 : (rb_FacturasSal.Checked ? 3 : 4))));
        }

        private void rb_ChequeS_CheckedChanged(object sender, EventArgs e)=>RadioSaldo(1);   
        private void rb_Transferencia_CheckedChanged(object sender, EventArgs e) => RadioSaldo(2); 

        private void rb_FacturasSal_CheckedChanged(object sender, EventArgs e) => RadioSaldo(3);  

        private void rb_BancosS_CheckedChanged(object sender, EventArgs e) => RadioSaldo(4);

        private void RadioSaldo(int radio)
        {
            string dato = ""; int bac = 0;

            if (string.IsNullOrEmpty(com_SaldoBancos.Text))
                bac = 1;
            else
                bac = Convert.ToInt32(X.elem3("SELECT idf_banks FROM fergeda.f_banks where Banco='" + com_SaldoBancos.Text + "';"));

            switch (radio)
            {
                case 1:
                    dato = X.elem3("SELECT max(fk_cheque)+1 FROM fergeda.f_movimiento  where fk_bancoMostrado='" + bac + "';");
                    if (dato.Equals("") || dato.Equals(null))
                        dato = "1";
                    groupBox7.Visible = false;
                    txt_BusquedaS.Enabled = true;
                    txt_BusquedaS.Text = dato;
                    opG = "fk_cheque";
                    break;
                case 2:
                    dato = X.elem3("SELECT max(transferencia)+1 FROM fergeda.f_movimiento   where fk_bancoMostrado='" + bac + "';");
                    if (dato.Equals("") || dato.Equals(null))
                        dato = "1";
                    groupBox7.Visible = false;
                    txt_BusquedaS.Enabled = true;
                    txt_BusquedaS.Text = dato;
                    opG = "transferencia";
                    break;
                case 3:
                    dato = X.elem3("SELECT max(Factura)+1 FROM fergeda.f_movimiento   where fk_bancoMostrado='" + bac + "';");
                    if (dato.Equals("") || dato.Equals(null))
                        dato = "1";
                    groupBox7.Visible = false;
                    txt_BusquedaS.Enabled = true;
                    txt_BusquedaS.Text = dato;
                    opG = "factura";
                    break;
                case 4:
                    opG = "fk_bancos";
                    groupBox7.Visible = true;
                    txt_BusquedaS.Enabled = false;
                    if (X.elem3("SELECT max(fk_bancos)+1 FROM fergeda.f_movimiento  where fk_bancoMostrado='" + bac + "';").Equals("") || X.elem3("SELECT max(bancos)+1 FROM litopolis_v5.fin_movimiento  where fin_bancoMostrado='" + bac + "';").Equals(null))
                    {
                        txt_BusquedaS.Text = "1";
                    }
                    else { txt_BusquedaS.Text = X.elem3("SELECT max(fk_bancos)+1 FROM fergeda.f_movimiento  where fk_bancoMostrado='" + bac + "';"); }
                    break;

            }
            selecc = true;
        }

        #endregion

        #region HOJA 9
        private void ConsultasCompras()
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);

            string comple = "", comple2 = "";

            if (rb_añoO.Checked)
                comple = "year(a.fecha_entrega)= '" + dateTimePicker3.Value.Year + "'";
            else if (rb_mesO.Checked)
                comple = "year(a.fecha_entrega)= '" + dateTimePicker3.Value.Year + "' and month(a.fecha_entrega)= '" + dateTimePicker3.Value.Month + "'";
            else if (rb_diaO.Checked)
                comple = "a.fecha_entrega='" + dateTimePicker3.Value.ToString("yyyy/MM/dd") + "'";

            if (rb_generalO.Checked)
                comple2 = "";
            else if (rb_proveedorO.Checked)
                comple2 = "and c.razon_social like '%" + com_busquedaO.Text + "%'";
            else if (rb_generoO.Checked)
                comple2 = "and e.nombre like '%" + com_busquedaO.Text + "%'";
            else if (rb_proyectoO.Checked)
                comple2 = "and a.motivo_compra like '%" + com_busquedaO.Text + "%'";
            else if (rb_clienteO.Checked)
                comple2 = "and b.razon_social" + (com_busquedaO.Text == "-" ? "is null" : "like '%" + com_busquedaO.Text + "%'") + "";
            else if (rb_departamentoO.Checked)
                comple2 = "and  d.departamento like '%" + com_busquedaO.Text + "%'";
            else if (rb_estatusO.Checked)
                comple2 = "and a.estatus like '%" + com_busquedaO.Text + "%'";
            else if (rb_descripcionO.Checked)
                comple2 = "and a.descripcion  like '%" + txt_busquedaO.Text + "%'";
            else if (rb_notasO.Checked)
                comple2 = "and a.notas like '%" + txt_busquedaO.Text + "%'";




            X.tablaDatos(dataGridView1, "SELECT  a.vobo 'Vo.Bo.',a.fk_ordencompra 'OC', a.partida 'Partida', a.tipo_proyecto " +
                "'Proyecto', concat_ws(' ',e.nombre,e.ap_paterno) 'Solicitante', d.departamento 'Departamento', a.motivo_compra 'Motivo de compra', " +
                "c.razon_social 'Proveedor', b.razon_social 'Cliente', a.descripcion 'Descripción', a.cantidad 'Cantidad', a.unidad" +
                " 'Unidad', a.precio_unitario 'C/U', a.sub_total, 'SubTotal', a.iva 'Iva', a.total 'Total', a.fecha_entrega " +
                "'Fecha Entrega', a.notas 'Notas', a.estatus 'Estatus', if (a.factura is null,'N/A',a.factura) 'Factura' , " +
                "a.fk_solicitudcheque 'Solicitud de Cheque', a.notas_internas 'Notas Internas', a.motivo_cancelado 'Motivo de " +
                "Cancelación' FROM fergeda.c_listadocompras a inner join fergeda.f_cliente b  on a.fk_cliente = b.idf_cliente " +
                "inner join fergeda.f_proveedor c on a.fk_proveedor = c.idf_proveedor inner join fergeda.r_idempleados d on " +
                "a.fk_departamento = d.idr_depatamento  inner join fergeda.r_empleado e on a.fk_solicitaempleado = e.idr_empleado " +
                " where " + comple + " " + comple2 + "group by a.partida  order by  a.idc_listadocompras desc;");

            Tb.colorStatus(dataGridView1, "Estatus", "En Tramite de Pago", 250, 234, 157);
            Tb.colorStatus(dataGridView1, "Estatus", "Cancelado", 245, 120, 117);
            Tb.colorStatus(dataGridView1, "Estatus", "Pagado", 180, 245, 192);
            Tb.colorStatus(dataGridView1, "Estatus", "Recibida", 233, 217, 242);
            Tb.colorStatus(dataGridView1, "Estatus", "Esperando Factura", 242, 174, 114);
            Tb.colorStatus(dataGridView1, "Estatus", "Devolucion", 255, 192, 254);
        }

        private void rb_generalO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = false;
            com_busquedaO.Visible = false;
            radios();
        }

        private void rb_proveedorO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = false;
            com_busquedaO.Visible = true;
            radios();
        }

        private void rb_generoO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = false;
            com_busquedaO.Visible = true;
            radios();
        }

        private void rb_proyectoO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = false;
            com_busquedaO.Visible = true;
            radios();
        }

        private void rb_clienteO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = false;
            com_busquedaO.Visible = true;
            radios();
        }

        private void rb_departamentoO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = false;
            com_busquedaO.Visible = true;
            radios();
        }

        private void rb_estatusO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = false;
            com_busquedaO.Visible = true;
            radios();
        }

        private void rb_ordenO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = true;
            com_busquedaO.Visible = false;
            radios();
        }

        private void rb_descripcionO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = true;
            com_busquedaO.Visible = false;
            radios();
        }

        private void rb_notasO_CheckedChanged(object sender, EventArgs e)
        {
            txt_busquedaO.Visible = true;
            com_busquedaO.Visible = false;
            radios();
        }

        private void rb_diaO_CheckedChanged(object sender, EventArgs e) => radios();

        private void rb_mesO_CheckedChanged(object sender, EventArgs e) => radios();

        private void rb_añoO_CheckedChanged(object sender, EventArgs e) => radios();

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e) => radios();

        private void bunifuImageButton12_Click(object sender, EventArgs e)
        {
            new chequeAsignado().Show();
        }

        private int radiosGen() => (rb_generalO.Checked ? 0 : (rb_proveedorO.Checked ? 1 : (rb_generoO.Checked ? 2 : (rb_proyectoO.Checked ? 3 :
      (rb_clienteO.Checked ? 4 : (rb_departamentoO.Checked ? 5 : (rb_estatusO.Checked ? 6 : 0)))))));

        private void radios()
        {
            int op = radiosGen();
            com_busquedaO.Items.Clear();
            com_busquedaO.Text = "";
            string dat = "";
            if (rb_diaO.Checked)
                dat = "FechaEntrega='" + dateTimePicker3.Value.ToString("yyyy/MM/dd") + "'";
            else if (rb_mesO.Checked)
                dat = "year(FechaEntrega) = '" + dateTimePicker3.Value.Year + "' and month(FechaEntrega)='" + dateTimePicker3.Value.Month + "'";
            else if (rb_añoO.Checked)
                dat = "year(FechaEntrega) = '" + dateTimePicker3.Value.Year + "'";

            switch (op)
            {
                case 1:
                    X.comb("SELECT distinct Proveedor FROM fergeda.c_compras where " + dat + " order by Proveedor;", com_busquedaO);
                    break;
                case 2:
                    X.comb("SELECT distinct Solicitante FROM fergeda.c_compras where " + dat + " order by Solicitante ;", com_busquedaO);
                    break;
                case 3:
                    X.comb("SELECT distinct motivo_compra FROM fergeda.c_compras where " + dat + " order by motivo_compra ;", com_busquedaO);
                    break;
                case 4:
                    X.comb("SELECT distinct Cliente FROM fergeda.c_compras where " + dat + " order by Cliente;", com_busquedaO);
                    break;
                case 5:
                    X.comb("SELECT distinct Departamento FROM fergeda.c_compras where " + dat + " order by Departamento;", com_busquedaO);
                    break;
                case 6:
                    X.comb("SELECT distinct Estatus FROM fergeda.c_compras where " + dat + " order by Estatus;", com_busquedaO);
                    break;
                default:
                    Console.WriteLine("sin def");
                    break;
            }
        }

        #endregion

        #region HOJA 10
        private void vistaSolicitudes()
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);

            X.tablaDatos(dataGridView1, "SELECT a.fk_solicitud 'Solicitud',b.razon_social 'Proveedor',a.numero_Oc 'Orden de Compra'," +
                "a.concepto 'Concepto',format(sum(a.importe),2) 'Importe',format(sum(a.subtotal),2)'Subtotal',format(sum(a.iva),2) " +
                "'Iva',format((a.total),2) 'Total',format(sum(a.retencion), 2)'Retención', a.contiene_iva 'C/Iva', a.estatus " +
                "'Estatus', a.fecha_pago 'Fecha de Pago' FROM fergeda.f_solicitudlistado a inner join   fergeda.f_proveedor b on " +
                "a.fk_proveedor = b.idf_proveedor  group by fk_solicitud order by a.fk_solicitud desc ; ");

            Tb.colorStatus(dataGridView1, "Estatus", "Cancelada", 245, 120, 117);
            Tb.colorStatus(dataGridView1, "Estatus", "Pagada", 180, 245, 192);
        }
        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            new SolicitudCheque(empleado).Show();

        }
        #endregion



    }


}
