using Fergeda_2023.Clases;
using Fergeda_2023.Compras.PDF;
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

namespace Fergeda_2023.Compras
{
    public partial class RequisicionCompra : Form
    {
        bool t = false;
        private estiloVentana est;
        private Consultas X;
        private formatoTablas Tb;
        private FormatoFechas fech;
        private MensageC msg;
        private Archivos arc;
        private Rutas rt;
        private generarQr qr;
        int empleado;

        List<string> tipos = new List<string> { "Normal", "Regular", "Urgente", "Super Urgente" };
        List<string> tipRequ = new List<string> { "Material", "Mantenimiento Correctivo", "Mantenimiento Preventivo", "Otros" };
        public RequisicionCompra(int empleado)
        {
            this.empleado = empleado;
            rt = new Rutas();
            arc = new Archivos();
            msg = new MensageC();
            est = new estiloVentana();
            X = new Consultas();
            Tb = new formatoTablas();
            fech = new FormatoFechas();
            qr = new generarQr();
            InitializeComponent();
            est.estilo(this);
            Tb.colores(dataGridView1, 1);
            Tb.quitarCelda(dataGridView1);
            Tb.colores(dataGridView2, 1);
            Tb.quitarCelda(dataGridView2);
            Height = 550;
            panel2.Height = 0;
            datosIniciales();
        }
        private void datosIniciales()
        {
            char[] sp = { '|' };
            string[] arr = X.elem3("SELECT concat_ws('|',Nombre,puesto) FROM fergeda.r_empleados where idr_empleado='" + empleado + "';").Split(sp);
            X.comb("SELECT Nombre FROM  fergeda.r_empleados where idr_depatamento=3 and Status<>'Baja' order by Nombre asc;", com_solicita);
            lbl_solicitante.Text = arr[0];
            lbl_area.Text = arr[1];
            label6.Text = fech.formatoFecha(DateTime.Now.ToString("yyyy/MM/dd"));
            com_tipo.Items.AddRange(tipRequ.ToArray());
            com_solicitud.Items.AddRange(tipos.ToArray());
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if (!t)
            {
                Height = 700;
                t = true;
                solicitudes();
                panel2.Height = 150;

            }
            else { Height = 550; t = false; panel2.Height = 0; }
        }

        private void solicitudes()
        {
            Tb.elimElemConConsulta(dataGridView1);
            X.tablaDatos(dataGridView1, " SELECT a.idc_requisicion 'Folio', b.Nombre 'Se solicita', if(a.tipo_requi='serCorr','Servicio Correctivo'," +
                "if(a.tipo_requi='serPre','Servicio Correctivo',a.tipo_requi)) 'Tipo',a.fecha_generada 'Fecha',if (a.tipo_sol = 0,'Normal',if " +
                "(a.tipo_sol = 1,'Regular',if (a.tipo_sol = 3,'Urgente','Super Urgente'))) 'Tipo de Solicitud',a.imagen 'Imagen',a.status 'status',a.link " +
                "'Link' FROM fergeda.c_requisicion a left join  fergeda.r_empleados b on a.fk_solicitaA = b.idr_empleado  where a.fk_solicitante =" +
                " '"+empleado+"' order by a.idc_requisicion desc; ");
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_descripcion.Text) && !string.IsNullOrEmpty(txt_marca.Text) && !string.IsNullOrEmpty(txt_referencia.Text) &&
               !string.IsNullOrEmpty(rich_departamento.Text) && !string.IsNullOrEmpty(com_unidad.Text))
            {
                dataGridView2.Rows.Add(txt_descripcion.Text, num_cantidad.Value.ToString(), com_unidad.Text, txt_referencia.Text, txt_marca.Text, rich_departamento.Text);
                txt_descripcion.Text = com_unidad.Text = txt_referencia.Text = txt_marca.Text = rich_departamento.Text = "";
            }
            else
            {
                msg.cargarEl("information", "Aviso", "Complete los campos para poder\ncontinuar"); msg.ShowDialog();
            }
        }
        string ruta = "", FolioT = "";


        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            OpenFileDialog x = new OpenFileDialog();
            x.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif;*.bmp|Todos los archivos|*.*";
            x.Title = "Selecciona el archivo";
            if (x.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (x.FileName.Equals("") == false)
                {
                    ruta = x.FileName;
                    lbl_estatus.Text = "CARGADO";
                    lbl_estatus.ForeColor = Color.Green;
                }
            }
            else { lbl_estatus.Text = "Error"; lbl_estatus.ForeColor = Color.Crimson; }
        }


        private async void bunifuImageButton10_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(com_solicitud.Text) && !string.IsNullOrEmpty(com_tipo.Text) && !string.IsNullOrEmpty(com_solicita.Text))
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    string comp = X.elem3("SELECT idr_empleado FROM fergeda.r_empleados where Nombre='" + com_solicita.Text + "';");
                    string dat = "" + tipos.IndexOf(com_solicitud.Text);
                    string tip = (com_tipo.Text.Equals("Mantenimiento Correctivo") ? "serCorr" : (com_tipo.Text.Equals("Mantenimiento Preventivo") ? "serPre" : com_tipo.Text));

                    X.insertar("INSERT INTO fergeda.c_requisicion (fk_solicitante, fecha_generada,fecha_programada, tipo_sol, tipo_requi, motivoC, link, imagen, fk_solicitaA) VALUES" +
                        " ('" + empleado + "', now(),'" + dateTimePicker1.Value.ToString("yyyy/MM/dd HH:mm:ss") + "', '" + dat + "', '" + tip + "', 'N/A', '" + (textBox4.Text.Equals("N/A") ? "-" : textBox4.Text) + "', '" + (lbl_estatus.Text.Equals("CARGADO") ? "Si" : "No") + "', '" + comp + "');");

                    string fol = X.elem3("SELECT max(idc_requisicion) FROM fergeda.c_requisicion where fk_solicitante='" + empleado + "';");

                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        X.insertar("INSERT INTO fergeda.c_requsiciondesgloce (fk_requi, descripcion, no_pza, unidad, parte_producto, Marca, Obgetivo) VALUES" +
                            " ('" + fol + "', '" + dataGridView2[0, i].Value.ToString() + "', '" + Convert.ToInt32(dataGridView2[1, i].Value.ToString())
                            + "', '" + dataGridView2[2, i].Value.ToString() + "', '" + dataGridView2[3, i].Value.ToString() + "', '" + dataGridView2[4, i].Value.ToString()
                            + "', '" + dataGridView2[5, i].Value.ToString() + "');");
                    }



                    if (lbl_estatus.Text.Equals("CARGADO"))
                        arc.copiarArchivo(ruta, rt.rutaimg(fol));
                    if (!textBox4.Text.Equals("N/A"))
                        qr.linkCompras(textBox4.Text, fol);

                    new requisicioncompra().LlenadoPdf(Convert.ToInt32(fol));
                    FolioT = fol;
                    Task task = new Task(envioCorreo);
                    task.Start();
                    await task;

                }
                else
                {
                    msg.cargarEl("information", "Aviso", "Ingrese Registros para\ncontinuar"); msg.ShowDialog();
                }
            }
            else
            {
                msg.cargarEl("information", "Aviso", "Complete los campos para poder\ncontinuar"); msg.ShowDialog();
            }
        }
        private void envioCorreo()
        {
            new Correo().compras("enajera@litopolis.com", "chalco@litopolis.com", "Requisición de Compra Chalco", new correoRequiCompras().envio(FolioT), "dYSRRZRaCg");
        }

    }
}
