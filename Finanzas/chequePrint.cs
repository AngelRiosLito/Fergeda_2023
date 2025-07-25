using Fergeda_2023.Clases;
using Fergeda_2023.Finanzas.PDF;
using System;
using System.Collections;
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
    public partial class chequePrint : Form
    {
        private Consultas X;
        private numerosLetras Num2;
        private GeneraPDF Pdf;
        private estiloVentana est;
        ArrayList lista = new ArrayList();
        ArrayList pol = new ArrayList();
        public chequePrint()
        {
            InitializeComponent();
            X = new Consultas();
            Num2 = new numerosLetras();
            Pdf = new GeneraPDF();
            est = new estiloVentana();
            est.estilo(this);
        }

        private void txt_grupo1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                busqueda2(Convert.ToInt32(txt_grupo1.Text), lbl_Cheque1, txt_solicitante1, txt_monto1, lbl_importeL1, lbl_Fecha1);
                busqueda2(Convert.ToInt32(txt_grupo1.Text) + 1, lbl_Cheque2, txt_solicitante2, txt_monto2, lbl_importeL2, lbl_Fecha2);
                busqueda2(Convert.ToInt32(txt_grupo1.Text) + 2, lbl_Cheque3, txt_solicitante3, txt_monto3, lbl_importeL3, lbl_Fecha3);
            }
        }

        private void txt_grupo2_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                busqueda2(Convert.ToInt32(txt_grupo2.Text), lbl_Cheque4, txt_solicitante4, txt_monto4, lbl_importeL4, lbl_Fecha4);
                busqueda2(Convert.ToInt32(txt_grupo2.Text) + 1, lbl_Cheque5, txt_solicitante5, txt_monto5, lbl_importeL5, lbl_Fecha5);
                busqueda2(Convert.ToInt32(txt_grupo2.Text) + 2, lbl_Cheque6, txt_solicitante6, txt_monto6, lbl_importeL6, lbl_Fecha6);
            }
        }

        private void txt_grupo3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = char.IsWhiteSpace(e.KeyChar);
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                busqueda2(Convert.ToInt32(txt_grupo3.Text), lbl_Cheque7, txt_solicitante7, txt_monto7, lbl_importeL7, lbl_Fecha7);
                busqueda2(Convert.ToInt32(txt_grupo3.Text) + 1, lbl_Cheque8, txt_solicitante8, txt_monto8, lbl_importeL8, lbl_Fecha8);
                busqueda2(Convert.ToInt32(txt_grupo3.Text) + 2, lbl_Cheque9, txt_solicitante9, txt_monto9, lbl_importeL9, lbl_Fecha9);
            }
        }

        private void busqueda2(int num, Label lb_num, TextBox sol, TextBox monto, Label importeLetra, Label fecha)
        {
            if (X.elem3("SELECT impreso FROM fergeda.f_cheque where idf_cheque='" + num + "';").Equals("S"))
            {
                num = 0;
            }
            if (X.elem3("SELECT count(*) FROM fergeda.f_cheque where idf_cheque='" + num + "';").Equals("1"))
            {
                sol.Enabled = true;
                lb_num.Text = "" + num;
                sol.Text = X.elem3("SELECT razon_social FROM fergeda.f_proveedor where idf_proveedor=(SELECT fk_proveedor FROM fergeda.f_cheque where idf_cheque='" + num + "');"); ;
                String importe = "" + double.Parse(X.elem3("SELECT importe FROM fergeda.f_cheque  where idf_cheque='" + num + "';"));
                monto.Text = double.Parse(importe).ToString("N2");
                char[] sp = { ',' };
                string[] arr = monto.Text.Split(sp);
                string completo = "";
                if (arr.Length > 1)
                {
                    completo = arr[0] + arr[1];
                }
                else { completo = arr[0]; }

                //importeLetra.Text = "" + Num.text(completo, true, "PESOS");
                importeLetra.Text = "" + Num2.num(monto.Text);
                fecha.Text = "" + DateTime.Parse(X.elem3("SELECT fecha_Gen FROM fergeda.f_cheque where idf_cheque='" + num + "';")).ToString("dd/MM/yyyy");

            }
            else
            {
                lb_num.Text = sol.Text = monto.Text = importeLetra.Text = fecha.Text = "";
                sol.Enabled = false;
            }
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_monto1.Text = txt_monto1.Text;
                lbl_monto2.Text = txt_monto2.Text;
                lbl_monto3.Text = txt_monto3.Text;
                //lbl_importeL1.Text = Num.text(montF(lbl_monto1.Text), true, "PESOS");
                if (lbl_monto1.Text.Equals(null) || lbl_monto1.Text.Equals(""))
                {
                    lbl_monto1.Text = "";
                }
                else { lbl_importeL1.Text =/* Num.text(montF(lbl_monto1.Text), true, "PESOS");*/ Num2.num(lbl_monto1.Text); }

                if (lbl_monto2.Text.Equals(null) || lbl_monto2.Text.Equals(""))
                {
                    lbl_monto2.Text = "";
                }
                else { lbl_importeL2.Text = /*Num.text(montF(lbl_monto2.Text), true, "PESOS");*/ Num2.num(lbl_monto2.Text); }

                if (txt_monto3.Text.Equals(null) || txt_monto3.Text.Equals(""))
                {
                    lbl_importeL3.Text = "";
                }
                else
                {
                    lbl_importeL3.Text = /*Num.text(montF(lbl_monto3.Text), true, "PESOS");*/ Num2.num(lbl_monto3.Text);
                }

                lista.Clear();
                llenadoArr(txt_solicitante1, lbl_monto1, lbl_importeL1, lbl_Fecha1);
                llenadoArr(txt_solicitante2, lbl_monto2, lbl_importeL2, lbl_Fecha2);
                llenadoArr(txt_solicitante3, lbl_monto3, lbl_importeL3, lbl_Fecha3);
                //MessageBox.Show("asdads");
                Pdf.cheque(lista, 1);
                if (!lbl_Cheque1.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque1.Text), txt_solicitante1, lbl_monto1, lbl_importeL1, lbl_Fecha1); }
                if (!lbl_Cheque2.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque2.Text), txt_solicitante2, lbl_monto2, lbl_importeL2, lbl_Fecha2); }
                if (!lbl_Cheque3.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque3.Text), txt_solicitante3, lbl_monto3, lbl_importeL3, lbl_Fecha3); }

                lbl_Cheque1.Text = lbl_Cheque2.Text = lbl_Cheque3.Text = "#";
                txt_monto1.Text = txt_monto2.Text = txt_monto3.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex);
            }
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_monto4.Text = txt_monto4.Text;
                lbl_monto5.Text = txt_monto5.Text;
                lbl_monto6.Text = txt_monto6.Text;
                if (lbl_monto4.Text.Equals(null) || lbl_monto4.Text.Equals(""))
                {
                    lbl_monto4.Text = "";
                }
                else { lbl_importeL4.Text = /*Num.text(montF(lbl_monto4.Text), true, "PESOS");*/ Num2.num(lbl_monto4.Text); }

                if (lbl_monto5.Text.Equals(null) || lbl_monto5.Text.Equals(""))
                {
                    lbl_monto5.Text = "";
                }
                else { lbl_importeL5.Text = /*Num.text(montF(lbl_monto5.Text), true, "PESOS");*/ Num2.num(lbl_monto5.Text); }

                if (lbl_monto6.Text.Equals(null) || lbl_monto6.Text.Equals(""))
                {
                    lbl_monto6.Text = "";
                }
                else { lbl_importeL6.Text =/* Num.text(montF(lbl_monto6.Text), true, "PESOS");*/ Num2.num(lbl_monto6.Text); }


                lista.Clear();
                llenadoArr(txt_solicitante4, lbl_monto4, lbl_importeL4, lbl_Fecha4);
                llenadoArr(txt_solicitante5, lbl_monto5, lbl_importeL5, lbl_Fecha5);
                llenadoArr(txt_solicitante6, lbl_monto6, lbl_importeL6, lbl_Fecha6);
                Pdf.cheque(lista, 2);
                
                if (!lbl_Cheque4.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque4.Text), txt_solicitante4, lbl_monto4, lbl_importeL4, lbl_Fecha4); }
                if (!lbl_Cheque5.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque5.Text), txt_solicitante5, lbl_monto5, lbl_importeL5, lbl_Fecha5); }
                if (!lbl_Cheque6.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque6.Text), txt_solicitante6, lbl_monto6, lbl_importeL6, lbl_Fecha6); }


                lbl_Cheque4.Text = lbl_Cheque5.Text = lbl_Cheque6.Text = "#";
                txt_monto4.Text = txt_monto5.Text = txt_monto6.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex);
            }
        }

        private void bunifuImageButton4_Click(object sender, EventArgs e)
        {
            try
            {
                lbl_monto7.Text = txt_monto7.Text;
                lbl_monto8.Text = txt_monto8.Text;
                lbl_monto9.Text = txt_monto9.Text;
                if (lbl_monto7.Text.Equals(null) || lbl_monto7.Text.Equals(""))
                {
                    lbl_monto7.Text = "";
                }
                else { lbl_importeL7.Text = /*Num.text(montF(lbl_monto7.Text), true, "PESOS");*/ Num2.num(lbl_monto7.Text); }
                if (lbl_monto8.Text.Equals(null) || lbl_monto8.Text.Equals(""))
                {
                    lbl_monto8.Text = "";
                }
                else { lbl_importeL8.Text = /*Num.text(montF(lbl_monto8.Text), true, "PESOS"); */Num2.num(lbl_monto8.Text); }

                if (txt_monto9.Text.Equals(null) || txt_monto9.Text.Equals(""))
                {
                    lbl_importeL9.Text = "";
                }
                else
                {
                    lbl_importeL9.Text = /*Num.text(montF(lbl_monto9.Text), true, "PESOS"); */Num2.num(lbl_monto9.Text);
                }
                lista.Clear();
                llenadoArr(txt_solicitante7, lbl_monto7, lbl_importeL7, lbl_Fecha7);
                llenadoArr(txt_solicitante8, lbl_monto8, lbl_importeL8, lbl_Fecha8);
                llenadoArr(txt_solicitante9, lbl_monto9, lbl_importeL9, lbl_Fecha9);
                Pdf.cheque(lista, 3);
                if (!lbl_Cheque7.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque7.Text), txt_solicitante7, lbl_monto7, lbl_importeL7, lbl_Fecha7); }
                if (!lbl_Cheque8.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque8.Text), txt_solicitante8, lbl_monto8, lbl_importeL8, lbl_Fecha8); }
                if (!lbl_Cheque9.Text.Equals("")) { llenarPol(Convert.ToInt32(lbl_Cheque9.Text), txt_solicitante9, lbl_monto9, lbl_importeL9, lbl_Fecha9); }


                lbl_Cheque7.Text = lbl_Cheque8.Text = lbl_Cheque9.Text = "#";
                txt_monto7.Text = txt_monto8.Text = txt_monto9.Text = "";

            }
            catch (Exception ex)
            {
                Console.WriteLine("" + ex);
            }
        }

        private void llenadoArr(TextBox sol, Label monto, Label importeLetra, Label fecha)
        {
            if (fecha.Text != "")
            {
                lista.Add(fecha.Text);
                lista.Add(sol.Text);
                lista.Add(monto.Text);
                lista.Add(importeLetra.Text);
            }
            else
            {
                lista.Add("");
                lista.Add("");
                lista.Add("");
                lista.Add("");
            }

        }

        private void Limpiar(TextBox sol, Label monto, Label importeLetra, Label fecha)
        {
            sol.Text = ""; monto.Text = "$"; importeLetra.Text = "Importe Con Letra"; fecha.Text = "Fecha";
        }

        private void llenarPol(int num, TextBox sol, Label monto, Label importeLetra, Label fecha)
        {

            pol.Add(fecha.Text);
            pol.Add(sol.Text);
            pol.Add(monto.Text);
            pol.Add(importeLetra.Text);
            pol.Add(X.elem3("SELECT concepto FROM fergeda.f_cheque where idf_cheque='" + num + "';"));
            Pdf.polizaCheque(pol, num);
            modiConcep(num, sol.Text);
            pol.Clear();
            Limpiar(sol, monto, importeLetra, fecha);
        }

        private void modiConcep(int num, string sol)
        {
            if (X.elem3("SELECT razon_social FROM fergeda.f_proveedor where idf_proveedor=(SELECT fk_proveedor FROM fergeda.f_cheque where idf_cheque='" + num + "');") != sol)            
                X.insertar("update fergeda.f_cheque set Notas='" + sol + "' where idf_cheque='" + num + "';");
            
            if (num > 0)            
                X.insertar("update fergeda.f_cheque set impreso='S' where idf_cheque='" + num + "';");
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

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}
