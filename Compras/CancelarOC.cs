using Fergeda_2023.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Compras
{
    public partial class CancelarOC : Form
    {
        private Consultas X;
        private MarcasAgua mar;
        private estiloVentana est;
        private Rutas rt;
        private Archivos arc;
        int empleado;
        public CancelarOC(int empleado)
        {
            this.empleado = empleado;
            X = new Consultas();
            mar = new MarcasAgua();
            est = new estiloVentana();
            rt = new Rutas();
            arc = new Archivos();
            InitializeComponent();
            est.estilo(this);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals("") && !richTextBox1.Text.Equals(""))
            {
                X.insertar("update fergeda.c_listadocompras set fk_empleadoCancelo='" + empleado + "' ," +
                    "motivo_cancelado='" + richTextBox1.Text + "',estatus='Cancelado' where fk_ordencompra='" + Convert.ToInt32(textBox1.Text) + "';");


                string ruta =rt.OC(textBox1.Text);
                if (File.Exists(ruta))
                {
                    string ruta2 = rt.OCCancelada(textBox1.Text);
                    mar.marcaAguaDiagonal(ruta, ruta2, "Canceldada " + empleado, 2);
                    arc.abirArchivo(ruta2);  
                }
                richTextBox1.Text = textBox1.Text = "";
            }
        }
    }
}
