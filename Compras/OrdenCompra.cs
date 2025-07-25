using Fergeda_2023.Clases;
using Fergeda_2023.Compras.PDF;
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
    public partial class OrdenCompra : Form
    {
        int empleado, partida = 1,apli=1;
        private formatoTablas Tb;
        private Consultas X;
        private GenerarOCF ord;
        private estiloVentana est;
        public OrdenCompra(int empleado, int apli)
        {
            this.empleado = empleado;
            this.apli = apli;
            Tb = new formatoTablas();
            X = new Consultas();
            ord = new GenerarOCF();
            est = new estiloVentana();
            InitializeComponent();
            est.estilo(this);
            Tb.colores(dataGridView1, 1);
            Tb.quitarCelda(dataGridView1);
            combos();
            agregarEncabezado();
        }

        #region GENERALES

       
        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            new Info().cerrarConex("" + empleado,apli); Application.Exit();
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void bunifuImageButton5_Click(object sender, EventArgs e)
        {
            MenuCompras n = new MenuCompras(empleado);
            n.Show();
            Close();
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
        #endregion

        #region CARGARCOMPONENTES
        private void combos()
        {
            com_tipo.Items.Clear();
            com_servicio.Items.Clear();
            com_solicitante.Items.Clear();
            com_cliente.Items.Clear();
            com_proveedor.Items.Clear();
            com_tipo.Text = com_solicitante.Text = com_servicio.Text = com_proveedor.Text = com_motivo.Text = com_cliente.Text = "";
            X.comb("SELECT distinct tipo_servicio FROM fergeda.c_servicio order by tipo_servicio asc;", com_tipo);
            X.comb("SELECT nombre FROM fergeda.c_solicitante;", com_solicitante);
            X.comb("SELECT razon_social FROM fergeda.f_liscliente;", com_cliente);
            X.comb("SELECT unidad FROM fergeda.c_unidadmedida where estatus<>'Baja' order by unidad asc;", com_servicio);
            X.comb("SELECT razon_social FROM fergeda.f_proveedor where estatus<>'Baja' order by estatus asc;", com_proveedor);

            richTextBox1.Text = richTextBox2.Text = rich_Descripcion.Text = "N/A";
            txt_cantidad.Text = txt_costoUni.Text = "";

        }

        List<string> EncabezadosTb = new List<string> { "Partida", "Proyecto", "Concepto", "Cantidad","Unidad", "Costo Unitario", "SubTotal" };

        private void agregarEncabezado()
        {
            for (int i = 0; i < EncabezadosTb.Count; i++)
                dataGridView1.Columns.Add(""+i, EncabezadosTb[i]); 
        }

       

        private void com_tipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!com_tipo.Text.Equals(""))
            {
                X.comb("SELECT descripcion FROM fergeda.c_servicio where tipo_servicio='" + com_tipo.Text + "' order by descripcion asc;", com_motivo);
            }
        }


        #endregion

        #region GENERAR OC
        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_cantidad.Text) && !string.IsNullOrEmpty(com_servicio.Text) && !string.IsNullOrEmpty(txt_costoUni.Text) && !string.IsNullOrEmpty(rich_Descripcion.Text))
            {
                double costo = double.Parse(txt_cantidad.Text) * double.Parse(txt_costoUni.Text);
                dataGridView1.Rows.Add("" + partida, com_motivo.Text, rich_Descripcion.Text, txt_cantidad.Text, com_servicio.Text, txt_costoUni.Text, costo.ToString("N2"));
                partida++;
                txt_costoUni.Text = txt_cantidad.Text = rich_Descripcion.Text = "";
            }
        }



        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(com_proveedor.Text) && !string.IsNullOrEmpty(com_solicitante.Text) && !string.IsNullOrEmpty(com_motivo.Text) && !string.IsNullOrEmpty(com_cliente.Text) &&
                    !string.IsNullOrEmpty(com_solicitante.Text) && !string.IsNullOrEmpty(com_servicio.Text))
                {
                    partida = 1;
                    X.insertar("insert into fergeda.c_ordencompra (fecha,fk_empleado,hora)values('" + DateTime.Now.ToString("yyyy/MM/dd") + "','" + empleado + "','" + DateTime.Now.ToString("hh:mm:ss") + "');");
                    int oc = Convert.ToInt32(X.elem3("SELECT max(idc_ordencompra) FROM fergeda.c_ordencompra where fk_empleado='" + empleado + "';"));
                    int empleasol = Convert.ToInt32(X.elem3("SELECT idr_empleado FROM fergeda.c_solicitante where nombre='" + com_solicitante.Text + "';"));
                    int depa = Convert.ToInt32(X.elem3("SELECT fk_Departamento FROM fergeda.r_puestos where idr_puestos=(SELECT fk_Puesto FROM fergeda.r_empleado where idr_empleado='" + empleasol + "');"));
                    int proveedor = Convert.ToInt32(X.elem3("SELECT idf_proveedor FROM fergeda.f_proveedor where razon_social='" + com_proveedor.Text + "';"));
                    int cliente = Convert.ToInt32(X.elem3("SELECT idf_cliente FROM fergeda.f_liscliente where razon_social='" + com_cliente.Text + "';"));
                    double iva = 0, total = 0;
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        iva = double.Parse(dataGridView1[6, i].Value.ToString()) * .16;
                        total = double.Parse(dataGridView1[6, i].Value.ToString()) + iva;
                        X.insertar("insert into fergeda.c_listadocompras (fk_ordencompra,partida,tipo_proyecto,fk_solicitaempleado,fk_departamento" +
                           ",motivo_compra,fk_proveedor,fk_cliente,descripcion, unidad, cantidad, precio_unitario, iva, sub_total, total, " +
                           "fecha_entrega, notas, estatus,notas_internas)values(" + oc + ",'" + (i + 1) + "','" + com_tipo.Text + "','" + empleasol + "','" + depa
                           + "','" + com_motivo.Text + "','" + proveedor + "','" + cliente + "','" + dataGridView1[2, i].Value.ToString() + "','" +
                           dataGridView1[4, i].Value.ToString() + "','" + double.Parse(dataGridView1[3, i].Value.ToString()) + "','" +
                           double.Parse(dataGridView1[5, i].Value.ToString()) + "','" + iva + "','" + double.Parse(dataGridView1[6, i].Value.ToString()) + "','" +
                           total + "','" + dateTimePicker1.Value.ToString("yyyy/MM/dd") + "','" + richTextBox1.Text + "','Alta','" + richTextBox2.Text + "'); ");
                    }
                    ord.generarOC(oc);
                    dataGridView1.Rows.Clear();
                    combos();
                } 
            }
        }

        #endregion
    }
}
