using Fergeda_2023.Clases;
using Fergeda_2023.Finanzas;
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
    public partial class ConsultasGeneral : Form
    {
        private formatoTablas Tb;
        private estiloVentana est;
        private Consultas X;
        private Info info;
        private MensageC msg;
        private Archivos arc;
        private Rutas rt;
        int pagina = 1, empleado,apli;

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

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            MenuCompras n = new MenuCompras(empleado);
            n.Show();
            Close();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            new Info().cerrarConex("" + empleado, apli); Application.Exit();
        }



        public ConsultasGeneral(int empleado,int apli)
        {
            Tb = new formatoTablas();
            est = new estiloVentana();
            X = new Consultas();
            info = new Info();
            msg = new MensageC();
            arc = new Archivos();
            rt = new Rutas();
            this.empleado = empleado;
            this.apli = apli;
            InitializeComponent();
            Tb.colores(dataGridView1,  1);
            est.estilo(this);

        }
        #region TABPAGE

       
        private void tabPage1_Enter(object sender, EventArgs e)
        {
            ConsultasCompras();
            pagina = 1;
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            pagina = 2;
            vistaSolicitudes();
        }
        private void tabPage3_Enter(object sender, EventArgs e)
        {
            pagina = 3;
            requisiciones();
        }

        #endregion

        #region GENERALES
        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            switch (pagina)
            {
                case 1:
                    ConsultasCompras();
                    break;
                case 2:
                    vistaSolicitudes();
                    break;
                case 3:
                    requisiciones();
                    break;
            }
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
            {
                int Columna = dataGridView1.CurrentCell.ColumnIndex;
                switch (pagina)
                {
                    case 1:
                        arc.abirArchivo(rt.OC(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                        arc.abirArchivo(rt.OCCancelada(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                        arc.abirArchivo(rt.OCFirmada(dataGridView1[1, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                        arc.abirArchivo(rt.SolicitudesCheque(dataGridView1[21, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                        break;
                    case 2:
                        arc.abirArchivo(rt.SolicitudesCheque(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                        arc.abirArchivo(rt.OC(dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                        break;
                    case 3:
                        switch (Columna)
                        {
                            case 0:
                                arc.abirArchivo(rt.requiComprasMArca(Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString())));
                                arc.abirArchivo(rt.rutaimg(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString()));
                                break;
                            case 1:
                                new VistaRequ(Convert.ToInt32(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value.ToString())).ShowDialog();
                                break;
                        }
                        break;

                }
            }           
        }

        #endregion

        #region HOJA 1
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
                comple = "a.fecha_entrega='"+dateTimePicker3.Value.ToString("yyyy/MM/dd")+"'";

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
                " where "+comple+" "+comple2+ "group by a.partida  order by  a.idc_listadocompras desc;");

            Tb.colorStatus(dataGridView1, "Estatus", "En Tramite de Pago", 250, 234, 157);
            Tb.colorStatus(dataGridView1, "Estatus", "Baja", 245, 120, 117);
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


        private int radiosGen() => (rb_generalO.Checked ? 0 : (rb_proveedorO.Checked ? 1 : (rb_generoO.Checked ? 2 : (rb_proyectoO.Checked ? 3 :
           (rb_clienteO.Checked ? 4 : (rb_departamentoO.Checked ? 5 : (rb_estatusO.Checked ? 6 : 0)))))));

        private void bunifuImageButton11_Click(object sender, EventArgs e)
        {
            new SolicitudCheque(empleado).Show();
        }



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

        private void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            switch (pagina)
            {
                case 3:
                    guardarRequisicion();
                    break;
            }
        }

       



        #endregion

        #region HOJA2
        private void vistaSolicitudes()
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);

            X.tablaDatos(dataGridView1, "SELECT a.fk_solicitud 'Solicitud',b.razon_social 'Proveedor',a.numero_Oc 'Orden de Compra'," +
                "a.concepto 'Concepto',format(sum(a.importe),2) 'Importe',format(sum(a.subtotal),2)'Subtotal',format(sum(a.iva),2) " +
                "'Iva',format((a.total),2) 'Total',format(sum(a.retencion), 2)'Retención', a.contiene_iva 'C/Iva', a.estatus " +
                "'Estatus', a.fecha_pago 'Fecha de Pago' FROM fergeda.f_solicitudlistado a inner join   fergeda.f_proveedor b on " +
                "a.fk_proveedor = b.idf_proveedor  group by fk_solicitud order by a.fk_solicitud desc ; ");

            //Tb.colorStatus(dataGridView1, "Estatus", "En Tramite de Pago", 250, 234, 157);
            Tb.colorStatus(dataGridView1, "Estatus", "Cancelada", 245, 120, 117);
            Tb.colorStatus(dataGridView1, "Estatus", "Pagada", 180, 245, 192);
            //Tb.colorStatus(dataGridView1, "Estatus", "Recibida", 233, 217, 242);
            //Tb.colorStatus(dataGridView1, "Estatus", "Esperando Factura", 242, 174, 114);
            //Tb.colorStatus(dataGridView1, "Estatus", "Devolucion", 255, 192, 254);

        }

        private void bunifuImageButton9_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count>0)
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
        }
        #endregion

        #region HOJA 3
        private void requisiciones()
        {
            dataGridView1.Columns.Clear();
            Tb.elimElemConConsulta(dataGridView1);

            string comple = "";
            if (rb_añorR.Checked)
                comple = "year(a.fecha_generada)='" + dateTimePicker9.Value.Year + "'";
            else if (rb_mesR.Checked)
                comple = "year(a.fecha_generada)='" + dateTimePicker9.Value.Year + "' and month(a.fecha_generada)='" + dateTimePicker9.Value.Month + "'";
            else if (rb_diaR.Checked)
                comple = "a.fecha_generada='" + dateTimePicker9.Value.ToString("yyyy/MM/dd") + "'";


            X.tablaDatos(dataGridView1, "SELECT a.idc_requisicion 'Folio',b.Nombre 'Solicitante',a.fecha_generada 'Fecha Generada', c.Nombre " +
                "'Solicita A', if (a.tipo_sol = 0,'Normal', if (a.tipo_sol = 1,'Regular',if (a.tipo_sol = 2,'Urgente','Super Urgente'))) " +
                "'Tipo de Solicitud',GROUP_CONCAT(concat(d.no_pza, ' ', d.unidad, '-', d.descripcion, ' ') SEPARATOR '/ ') AS 'Material', a.fecha_programada 'Fecha Programada' , if (a.tipo_requi = 'serPre','Servicio Preventivo',if " +
                "(a.tipo_requi = 'serCorr','Servicio Correctivo',a.tipo_requi)) 'Tipo de Servicio', a.fec_recepcion 'Fecha Rececpción', " +
                "a.fecha_cierre 'Fecha de Cierre',  a.link 'Link', a.imagen 'Imagen', a.status 'Estatus', a.motivoC 'Motivo Cancelación'" +
                "   FROM fergeda.c_requisicion " +
                "a inner join fergeda.r_empleados b on a.fk_solicitante = b.idr_empleado inner join fergeda.r_empleados c on a.fk_solicitaA = " +
                "c.idr_empleado left join  fergeda.c_requsiciondesgloce d on a.idc_requisicion = d.fk_requi " +
                "where "+ comple + " group by a.idc_requisicion order by a.idc_requisicion desc; ");


            Tb.colorStatus(dataGridView1, "Estatus", "Baja", 245, 120, 117);
            Tb.colorStatus(dataGridView1, "Estatus", "Terminado", 180, 245, 192);
            Tb.colorStatus(dataGridView1, "Estatus", "Proceso", 250, 234, 157);
            Tb.colorStatus(dataGridView1, "Estatus", "Rechazado", 242, 174, 114);
        }


        private void guardarRequisicion()
        {
            if (!string.IsNullOrEmpty(txt_requi.Text) && !string.IsNullOrEmpty(rich_requi.Text))
            {
                if (X.elem3("SELECT status  FROM fergeda.c_requisicion where idc_requisicion='" + txt_requi.Text + "';").Equals("Alta"))
                {
                    X.insertar("update fergeda.c_requisicion set status='" + (rb_aceptarR.Checked ? "Proceso" : "Rechazado") + "',fec_recepcion=now(),fec_recepcion=now(),motivoC='" + (rb_aceptarR.Checked ? "N/A" : rich_requi.Text) + "' where idc_requisicion='" + txt_requi.Text + "';");
                    txt_requi.Text = "";
                    rich_requi.Text = "N/A";
                    rb_aceptarR.Checked = true;
                    requisiciones();
                }
                else
                {
                    msg.cargarEl("proteger", "Error", "Ingrese un folio valido"); msg.ShowDialog();
                }
            }
            else
            {
                msg.cargarEl("proteger", "Error", "Completa los campos para continuar"); msg.ShowDialog();
            }

        }
        #endregion
    }
}
