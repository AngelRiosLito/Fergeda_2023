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

namespace Fergeda_2023.Finanzas.CuentasPorPagar
{
    public partial class CancelarSolicitud : Form
    {

        private MarcasAgua marcAgua;
        private MensageC msg;
        private Consultas X;
        private estiloVentana est;
        private Rutas rt;
        int empl;
        public CancelarSolicitud(int empleado)
        {
            InitializeComponent();
        }

        private void txt_Busqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
        }

        private void bunifuImageButton6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_Busqueda.Text) && !string.IsNullOrEmpty(richTextBox1.Text))
            {
                Console.WriteLine("SELECT max(idf_solicitudcheque) FROM fergeda.f_solicitudcheque;");
                if (Convert.ToInt32(txt_Busqueda.Text) <= Convert.ToInt32(X.elem3("SELECT max(idf_solicitudcheque) FROM fergeda.f_solicitudcheque;")))
                {
                    
                    string id_Partida = X.elem3("SELECT min( idf_solicitudlistado) FROM fergeda.f_solicitudlistado where fk_solicitud='" + txt_Busqueda.Text + "';");
                    string id_PartidaMax = X.elem3("SELECT max( idf_solicitudlistado)FROM fergeda.f_solicitudlistado where fk_solicitud='" + txt_Busqueda.Text + "';");
                    if (System.IO.File.Exists(rt.solicitudCheque(txt_Busqueda.Text)))
                    {
                        marcAgua.SelloCancelado(rt.solicitudCheque2(txt_Busqueda.Text), "Cancelada", empl);
                        for (int i = Convert.ToInt32(id_Partida); i <= Convert.ToInt32(id_PartidaMax); i++)
                        {
                            X.insertar("UPDATE fergeda.f_solicitudlistado SET estatus='Cancelada', cancelada_por='" + richTextBox1.Text + "',fk_empleadoCancelado='" + empl + "' WHERE idf_solicitudlistado= '" + i + "';");

                        }
                        msg.cargarEl("like", "Correcto", "Se a Cancelado Correctamente"); msg.ShowDialog();
                        richTextBox1.Text = txt_Busqueda.Text = "";
                    }
                    else
                    {
                        msg.cargarEl("no-molestar", "Error", "No se encuentra la solicitud"); msg.ShowDialog();
                    }
                }
                else
                {
                    msg.cargarEl("no-molestar", "Error", "Ingrese una Solicitud Valida"); msg.ShowDialog();
                }
            }
            else
            {
                msg.cargarEl("no-molestar", "Error", "Complete los campos para\npoder completar"); msg.ShowDialog();
            }
        }

    }
}

