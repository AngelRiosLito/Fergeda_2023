using Fergeda_2023.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Finanzas.PDF
{
    class FlujoBancos
    {
        private FileStream fs;
        private PdfWriter writer;
        private Document doc;
        private Archivos arc = new Archivos();
        private Rutas rt = new Rutas();
        private CalculosFechas fech = new CalculosFechas();
        private Consultas X = new Consultas();
        private FormatoFechas fecha1 = new FormatoFechas();
        private Transformacion tr = new Transformacion();

        List<string> listaTabla = new List<string>() { "FECHA", "CHEQUE", "BANCOS", "CLIENTE", "DESCRIPCIÓN", "CARGO", "ABONO", "SALDO" };
        List<string> listaTablaR = new List<string>();
        List<string> listaTabla2 = new List<string>() { " ", "SALDO" };
  
        private string banco1(string banco) => X.elem3("SELECT Banco FROM fergeda.f_banks where idf_banks='" + banco + "';");

        private string saldosB(string banco)
        {
            //MessageBox.Show(banco);

            listaTabla2.Clear();
            double saldoB = double.Parse(tr.Mostrar(X.elem3("SELECT saldo_disponoble FROM fergeda.f_banks where Banco='" + banco1(banco) + "';"),new Transformacion().Formato));
            
            listaTabla2.Add("Saldo en bancos");
            listaTabla2.Add(saldoB.ToString("N2"));

            if (banco.Equals("BANCOMER SA"))
            {
                double trans = double.Parse(X.elem3("SELECT sum(importe) FROM fergeda.f_cheque where Estatus='Transito';"));
                listaTabla2.Add("Transito");
                listaTabla2.Add(trans.ToString("N2"));

                listaTabla2.Add("Saldo-Cheques");
                listaTabla2.Add((saldoB - trans).ToString("N2"));

                listaTabla2.Add("Pendiente");
                double pen = double.Parse(X.elem3("SELECT sum(importe) FROM fergeda.f_cheque where Estatus='Pendiente';"));
                listaTabla2.Add(pen.ToString("N2"));

                listaTabla2.Add("Diferencia de Flujo");
                listaTabla2.Add(((saldoB - trans) - pen).ToString("N2"));

                listaTabla2.Add("Comprobacion");
                listaTabla2.Add((((saldoB - trans) - pen) - double.Parse(listaTablaR[listaTablaR.Count - 1])).ToString("N2"));

            }
            else
            {
                listaTabla2.Add("Transito");
                listaTabla2.Add("0");
                listaTabla2.Add("Saldo-Cheques");
                listaTabla2.Add("0");
                listaTabla2.Add("Pendiente");
                listaTabla2.Add("0");

                /*no tiene*/
            }

            return null;
        }

        private void LlenarT1(DataGridView Tb)
        {
            listaTablaR.Clear();
            for (int i = 0; i < Tb.RowCount; i++)
            {
                for (int j = 0; j < Tb.ColumnCount; j++)
                {
                    listaTablaR.Add(Tb[j, i].Value.ToString());
                }
            }

        }
        public void generaPDF(DataGridView tb, string bancoNom)
        {
            try
            {

                string ruta = @"\\192.168.0.231\litopolis publico\Fergeda\Finanzas\Cobranza\Bancos\estado.pdf";
                for (int i = 0; i < 10; i++)
                    if (!File.Exists(rt.saldos(banco1(bancoNom), i)))
                        ruta = rt.saldos(banco1(bancoNom), i);

                LlenarT1(tb);
                
                saldosB(bancoNom);
                

                fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
                doc = new Document(PageSize.LETTER.Rotate(), 0, 0, 0, 0);
                writer = PdfWriter.GetInstance(doc, fs);
                //MessageBox.Show("paso 1");
                int[] ancho = { 250, 250, 200 };
                int[] ancho1 = { 80, 60, 60, 195, 190, 80, 80, 80 };
                int[] ancho2 = { 20, 125, 125 };
                int[] ancho3 = { 40, 60, 60, 240, 180 };
                int[] anchoT = { 290, 290 };

                var fuente10 = FontFactory.GetFont("Arial Narrow", 12, new BaseColor(System.Drawing.Color.Black));
                var fuenteTIT = FontFactory.GetFont("Arial Narrow", 15, new BaseColor(System.Drawing.Color.Black));
                var fuente5 = FontFactory.GetFont("Arial Narrow", 10, new BaseColor(System.Drawing.Color.Black));
                var fuente10T = FontFactory.GetFont("Arial Narrow", 10, new BaseColor(System.Drawing.Color.White));
                var fuente5_ = FontFactory.GetFont("Arial Narrow", 10, new BaseColor(System.Drawing.Color.Black));
                var fuente6 = FontFactory.GetFont("Arial Narrow", 8, new BaseColor(System.Drawing.Color.Black));
                //MessageBox.Show("paso 2");
                doc.AddTitle("Entrega De Proyecto");
                doc.AddCreator("Angel Rios");

                doc.Open();
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"logo-fergeda.png");
                imagen.ScalePercent(47);


                //MessageBox.Show("paso 2");
                #region ENCABEZADO

                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = 700;
                table.LockedWidth = true;
                table.HorizontalAlignment = 1;
                table.SetWidths(ancho);

                PdfPCell espacio = new PdfPCell(new Paragraph("" + (char)00, fuente5_));
                espacio.Colspan = 3;
                espacio.HorizontalAlignment = Element.ALIGN_CENTER;
                espacio.VerticalAlignment = Element.ALIGN_CENTER;
                espacio.BorderColor = new BaseColor(Color.Transparent);

                PdfPCell imag = new PdfPCell(imagen);
                imag.Colspan = 1;
                imag.Rowspan = 2;
                imag.HorizontalAlignment = Element.ALIGN_LEFT;
                imag.BorderColor = new BaseColor(Color.Transparent);

                PdfPCell fecha = new PdfPCell(new Paragraph("\n \n \n \n " + fecha1.formatoFecha(DateTime.Now.ToString("yyyy/MM/dd")) + (char)00, fuente5));
                fecha.Colspan = 1;
                fecha.Rowspan = 2;
                fecha.HorizontalAlignment = Element.ALIGN_RIGHT;
                fecha.BorderColor = new BaseColor(Color.Transparent);

                PdfPCell titulo = new PdfPCell(new Paragraph("\n \n FLUJO DE BANCOS" + (char)00, fuenteTIT));
                titulo.Colspan = 1;
                titulo.Rowspan = 2;
                titulo.HorizontalAlignment = Element.ALIGN_CENTER;
                titulo.VerticalAlignment = Element.ALIGN_CENTER;
                titulo.BorderColor = new BaseColor(Color.Transparent);


                PdfPCell banco = new PdfPCell(new Paragraph("BANCO: " + banco1(bancoNom), fuente5));
                banco.Colspan = 3;
                banco.HorizontalAlignment = Element.ALIGN_LEFT;
                banco.VerticalAlignment = Element.ALIGN_BOTTOM;
                banco.BorderColor = new BaseColor(Color.Transparent);

                table.AddCell(espacio);
                table.AddCell(imag);
                table.AddCell(titulo);
                table.AddCell(fecha);
                table.AddCell(espacio);
                table.AddCell(banco);
                table.AddCell(espacio);

                doc.Add(table);
                #endregion

                #region TABLA 1


                PdfPTable table2 = new PdfPTable(8);
                table2.TotalWidth = 760;
                table2.LockedWidth = true;
                table2.HorizontalAlignment = 1;
                table2.SetWidths(ancho1);

                for (int i = 0; i < listaTabla.Count; i++)
                {
                    PdfPCell pdfT1 = new PdfPCell(new Paragraph("" + listaTabla[i], fuente10T));
                    pdfT1.Colspan = 1;
                    pdfT1.HorizontalAlignment = Element.ALIGN_CENTER;
                    pdfT1.BorderColor = new BaseColor(Color.Transparent);
                    pdfT1.BackgroundColor = new BaseColor(Color.Black);
                    table2.AddCell(pdfT1);
                }

                double sumC = 0, sumA = 0, sumS = 0;
                int cont = 0;
                for (int j = 0; j < listaTablaR.Count; j++)
                {

                    if (cont.Equals(8)) cont = 0;
                    if (cont.Equals(5))
                        sumC += Convert.ToDouble(listaTablaR[j]);

                    if (cont.Equals(6))
                        sumA += Convert.ToDouble(listaTablaR[j]);

                    cont++;
                }
                double dato = double.Parse(listaTablaR[5]);
                double dato1 = double.Parse(listaTablaR[6]);
                sumC = sumC - dato;
                sumA = sumA - dato1;
                sumS = double.Parse(listaTablaR[listaTablaR.Count - 1]);
                List<double> suma = new List<double>() { sumC, sumA, sumS };


                for (int i = 0; i < listaTablaR.Count; i++)
                {
                    if (cont.Equals(8)) cont = 0;

                    var fuent = fuente5;
                    if (cont.Equals(3))
                        fuent = fuente6;
                    if (cont.Equals(4))
                        fuent = fuente6;

                    string dat = "" + (cont.ToString() == "0" ? fecha1.foratoFecha(DateTime.Parse(listaTablaR[i]).ToString("yyyy/MM/dd")) : listaTablaR[i]);
                    if (dat.Equals("0.00"))
                        dat = " " + (char)00;

                    else if (dat.Equals("0"))
                        dat = " " + (char)00;

                    PdfPCell relleno = new PdfPCell(new Paragraph(dat, fuent));
                    relleno.Colspan = 1;
                    if (cont.Equals(3))
                        relleno.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    else if (cont.Equals(4))
                        relleno.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    else
                        relleno.HorizontalAlignment = Element.ALIGN_CENTER;

                    relleno.BorderColor = new BaseColor(Color.Black);
                    table2.AddCell(relleno);
                    cont++;
                }


                PdfPCell esp = new PdfPCell(new Paragraph(" TOTAL:          \t \t  ", fuente5));
                esp.Colspan = 5;
                esp.HorizontalAlignment = Element.ALIGN_RIGHT;
                esp.BackgroundColor = new BaseColor(Color.LightGray);
                table2.AddCell(esp);

                #region Columnas Sumas
                cont = 0;
                for (int i = 0; i < suma.Count; i++)
                {
                    PdfPCell sumas = new PdfPCell(new Paragraph("" + suma[i].ToString("N2"), fuente5));
                    sumas.Colspan = 1;
                    sumas.HorizontalAlignment = Element.ALIGN_CENTER;
                    sumas.BorderColor = new BaseColor(Color.Black);
                    if (cont < 2)
                        sumas.BackgroundColor = new BaseColor(Color.LightGray);
                    table2.AddCell(sumas);
                    cont++;
                }
                #endregion


                table.DeleteBodyRows();
                table.AddCell(espacio);
                table.AddCell(espacio);
                //table.AddCell(espacio);
                doc.Add(table2);
                doc.Add(table);

                #endregion

                #region TABLA 2



                PdfPTable table3 = new PdfPTable(3);
                table3.TotalWidth = 270;
                table3.LockedWidth = true;
                table3.HorizontalAlignment = 0;
                table3.SetWidths(ancho2);

                PdfPCell espacioBlanco = new PdfPCell(new Paragraph(" " + (char)00, fuente5));
                espacioBlanco.Colspan = 1;
                if (bancoNom.Equals("1"))
                    espacioBlanco.Rowspan = 7;
                else
                    espacioBlanco.Rowspan = 3;
                espacioBlanco.BorderColor = new BaseColor(Color.Transparent);

                table3.AddCell(espacioBlanco);
                for (int i = 0; i < listaTabla2.Count; i++)
                {
                    PdfPCell pdfT2 = new PdfPCell();
                    if (i % 2 == 0 || i == 0 || i == 1)
                    {
                        pdfT2 = new PdfPCell(new Paragraph("" + listaTabla2[i], fuente6));
                        pdfT2.BackgroundColor = new BaseColor(Color.LightGray);
                        pdfT2.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    }
                    else
                    {
                        pdfT2 = new PdfPCell(new Paragraph("" + listaTabla2[i], fuente6));

                        pdfT2.HorizontalAlignment = Element.ALIGN_CENTER;
                    }
                    pdfT2.Colspan = 1;

                    pdfT2.BorderColor = new BaseColor(Color.Black);

                    table3.AddCell(pdfT2);
                }
                doc.Add(table3);
                #endregion

                #region TABLA 5
                PdfPTable table4 = new PdfPTable(3);
                table4.TotalWidth = 700;
                table4.LockedWidth = true;
                table4.HorizontalAlignment = 1;
                table4.SetWidths(ancho);

                PdfPCell firma = new PdfPCell(new Paragraph(" " + (char)00, fuente5));
                firma.Colspan = 1;
                firma.BorderColor = new BaseColor(Color.Transparent);

                PdfPCell firma1 = new PdfPCell(new Paragraph(" _________________________________\nFinanzas" + (char)00, fuente5));
                firma1.HorizontalAlignment = Element.ALIGN_CENTER;
                firma1.Colspan = 1;
                firma1.BorderColor = new BaseColor(Color.Transparent);

                PdfPCell firma2 = new PdfPCell(new Paragraph(" _________________________________\nDireccion General" + (char)00, fuente5));
                firma2.HorizontalAlignment = Element.ALIGN_CENTER;
                firma2.Colspan = 1;
                firma2.BorderColor = new BaseColor(Color.Transparent);

                table4.AddCell(firma);
                table4.AddCell(firma1);
                table4.AddCell(firma2);

                doc.Add(table4);
                #endregion

                doc.Close();
                writer.Close();
                arc.abirArchivo(ruta);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                writer.Close();
               

            }
        }



    }
}
