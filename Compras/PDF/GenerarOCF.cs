using Fergeda_2023.Clases;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Compras.PDF
{
    class GenerarOCF
    {
        private FileStream fs;
        private PdfWriter writer;
        private Document doc;
        private MarcasAgua mar;
        private ArrayList arr = new ArrayList();
        private ArrayList compr = new ArrayList();
        private Archivos arc = new Archivos();
        private Rutas rt = new Rutas();
        private void llenarArr(int oc)
        {
            Consultas X = new Consultas();

            compr.Clear();
            arr.Clear();
            int proveedor = Convert.ToInt32(X.elem3("select distinct fk_proveedor FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';"));
            int solicitante = Convert.ToInt32(X.elem3("select distinct fk_solicitaempleado FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';"));
            int empGenero = Convert.ToInt32(X.elem3("SELECT fk_empleado FROM fergeda.c_ordencompra where idc_ordencompra='" + oc + "';"));
            arr.Add(X.elem3("SELECT rfc FROM fergeda.f_proveedor where idf_proveedor='" + proveedor + "';"));
            arr.Add(X.elem3("SELECT razon_social FROM fergeda.f_proveedor where idf_proveedor='" + proveedor + "';"));
            arr.Add(X.elem3("SELECT contacto FROM fergeda.f_proveedor where idf_proveedor='" + proveedor + "';"));
            arr.Add(X.elem3("SELECT correo FROM fergeda.f_proveedor where idf_proveedor='" + proveedor + "';"));
            arr.Add(X.elem3("SELECT telefono FROM fergeda.f_proveedor where idf_proveedor='" + proveedor + "';"));
            arr.Add(new FormatoFechas().foratoFecha(DateTime.Parse(X.elem3("select distinct fecha_entrega FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';")).ToString("yyyy/MM/dd")));
            arr.Add(X.elem3("SELECT concat(dias,' ',condiciones) FROM fergeda.f_proveedor where idf_proveedor='" + proveedor + "';"));
            arr.Add(X.elem3("SELECT nombre FROM fergeda.c_solicitante where idr_empleado='" + solicitante + "';"));
            arr.Add(X.elem3("SELECT departamento FROM fergeda.r_idempleados where idr_empleado='" + solicitante + "';"));
            arr.Add(X.elem3("select distinct motivo_compra FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';"));
            arr.Add(X.elem3("select concat(nombre,' ',ap_paterno,' ',ap_materno) from fergeda.r_empleado where idr_empleado='" + empGenero + "';"));
            arr.Add(X.elem3("SELECT departamento FROM fergeda.r_idempleados where idr_empleado='" + empGenero + "';"));
            arr.Add(new FormatoFechas().foratoFecha(DateTime.Parse(X.elem3("SELECT fecha FROM fergeda.c_ordencompra where idc_ordencompra='" + oc + "';")).ToString("yyyy/MM/dd")));
            arr.Add(double.Parse(X.elem3("select sum(sub_total) FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';")).ToString("N2"));
            arr.Add(double.Parse(X.elem3("select sum(iva) FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';")).ToString("N2"));
            arr.Add(double.Parse(X.elem3("select sum(total) FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';")).ToString("N2"));
            arr.Add(X.elem3("select distinct notas FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';"));
            int cantidad = Convert.ToInt32(X.elem3("select count(*) FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "';"));
            arr.Add(cantidad);
            for (int i = 1; i <= cantidad; i++)
            {
                compr.Add(X.elem3("select cantidad FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "' and partida='" + i + "';"));
                compr.Add(X.elem3("select unidad FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "' and partida='" + i + "';"));
                compr.Add(X.elem3("select descripcion FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "' and partida='" + i + "';"));
                compr.Add(double.Parse(X.elem3("select precio_unitario FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "' and partida='" + i + "';")).ToString("N2"));
                compr.Add(double.Parse(X.elem3("select sub_total FROM fergeda.c_listadocompras where fk_ordencompra='" + oc + "' and partida='" + i + "';")).ToString("N2"));
            }


        }

        public void generarOC(int oc)
        {
            try
            {
                mar = new MarcasAgua();
                llenarArr(oc);
                //string ruta = @"\\192.168.0.231\Litopolis Publico\Fergeda\Compras\Orden de Compra\Orden Compra " + oc + ".PDF";
                fs = new FileStream(rt.OCSimple(oc+""), FileMode.Create, FileAccess.Write, FileShare.None);
                doc = new Document(PageSize.LETTER, 0, 0, 0, 0);
                writer = PdfWriter.GetInstance(doc, fs);

                int[] ancho = { 193, 193, 195 };
                int[] ancho1 = { 226, 56, 144, 1, 143 };
                int[] ancho2 = { 66, 76, 156, 87, 97, 96 };
                var fuente = FontFactory.GetFont("Arial Narrow", 11, new BaseColor(System.Drawing.Color.Black));
                var fuente1 = FontFactory.GetFont("Arial Narrow", 11, new BaseColor(System.Drawing.Color.White));
                var fuente2 = FontFactory.GetFont("Arial Narrow", 14, new BaseColor(System.Drawing.Color.Black));
                var fuente3 = FontFactory.GetFont("Arial Narrow", 3, new BaseColor(System.Drawing.Color.Black));

                doc.AddTitle("Orden de Compra");
                doc.AddCreator("Angel Rios");
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"logo-fergeda.png");
                doc.Open();
                imagen.ScalePercent(45); //70 si es A6 
                imagen.SetAbsolutePosition(15f, 720f);
                doc.Add(imagen);


                PdfPTable table = new PdfPTable(3);
                table.TotalWidth = 580f;
                table.LockedWidth = true;
                table.HorizontalAlignment = 1;
                table.SetWidths(ancho);

                PdfPCell vacia = new PdfPCell(new Paragraph("" + (char)00, fuente2));
                vacia.Colspan = 1;
                vacia.HorizontalAlignment = Element.ALIGN_RIGHT;
                vacia.BorderColor = new BaseColor(Color.Transparent);

                PdfPCell vacia1 = new PdfPCell(new Paragraph("\n\nOrden de Compra " + oc + "\n\n\n" + (char)00, fuente2));
                vacia1.Colspan = 2;
                vacia1.HorizontalAlignment = Element.ALIGN_CENTER;
                vacia1.BorderColor = new BaseColor(Color.Transparent);

                table.AddCell(vacia);
                table.AddCell(vacia1);

                PdfPTable table1 = new PdfPTable(5);
                table1.TotalWidth = 580f;
                table1.LockedWidth = true;
                table1.HorizontalAlignment = 1;
                table1.SetWidths(ancho1);

                PdfPCell datos = new PdfPCell(new Paragraph("Proveedor:  R.F.C.  " + arr[0] + "" + (char)00, fuente1));
                datos.Colspan = 1;
                datos.HorizontalAlignment = Element.ALIGN_LEFT;
                datos.BorderColor = new BaseColor(Color.White);
                datos.BackgroundColor = new BaseColor(Color.DarkBlue);

                PdfPCell datos1 = new PdfPCell(new Paragraph("" + (char)00, fuente1));
                datos1.Colspan = 1;
                datos1.HorizontalAlignment = Element.ALIGN_LEFT;
                datos1.BorderColor = new BaseColor(Color.White);

                PdfPCell datos4 = new PdfPCell(new Paragraph("Solicitó" + (char)00, fuente1));
                datos4.Colspan = 1;
                datos4.HorizontalAlignment = Element.ALIGN_CENTER;
                datos4.BorderColor = new BaseColor(Color.Black);
                datos4.BackgroundColor = new BaseColor(Color.DarkBlue);

                PdfPCell datos5 = new PdfPCell(new Paragraph("Realizó" + (char)00, fuente1));
                datos5.Colspan = 2;
                datos5.HorizontalAlignment = Element.ALIGN_CENTER;
                datos5.BorderColor = new BaseColor(Color.Black);
                datos5.BackgroundColor = new BaseColor(Color.DarkBlue);


                PdfPCell datos2 = new PdfPCell(new Paragraph("" + arr[1] + "\nAt'n:" + arr[2] + "\nCorreo: " + arr[3] + "\nTelefono:" + arr[4] + " \nEntrega: " + arr[5] + "\nPago: " + arr[6] + "" + (char)00, fuente));
                datos2.Colspan = 1;
                datos2.Rowspan = 2;
                datos2.HorizontalAlignment = Element.ALIGN_LEFT;
                datos2.BorderColor = new BaseColor(Color.White);


                PdfPCell datos6 = new PdfPCell(new Paragraph("" + arr[7] + "\n" + arr[8] + "\n" + arr[9] + "\n" + (char)00, fuente));
                datos6.Colspan = 1;
                datos6.HorizontalAlignment = Element.ALIGN_LEFT;
                datos6.BorderColor = new BaseColor(Color.White);

                PdfPCell datos7 = new PdfPCell(new Paragraph("" + arr[10] + "\n" + arr[11] + "\n" + arr[12] + "\n" + (char)00, fuente));
                datos7.Colspan = 2;
                datos7.HorizontalAlignment = Element.ALIGN_LEFT;
                datos6.BorderColor = new BaseColor(Color.Black);

                PdfPCell datos8 = new PdfPCell(new Paragraph("Autorizo:\n" + (char)00, fuente));
                datos8.Colspan = 4;
                datos8.HorizontalAlignment = Element.ALIGN_LEFT;
                datos8.BorderColor = new BaseColor(Color.Black);


                table1.AddCell(datos);
                table1.AddCell(datos1);
                table1.AddCell(datos4);
                table1.AddCell(datos5);
                table1.AddCell(datos2);
                table1.AddCell(datos1);
                table1.AddCell(datos6);
                table1.AddCell(datos7);
                table1.AddCell(datos1);
                table1.AddCell(datos8);



                PdfPTable table2 = new PdfPTable(6);
                table2.TotalWidth = 580f;
                table2.LockedWidth = true;
                table2.HorizontalAlignment = 1;
                table2.SetWidths(ancho2);

                PdfPCell cuerpo = new PdfPCell(new Paragraph("Cantidad" + (char)00, fuente1));
                cuerpo.Colspan = 1;
                cuerpo.HorizontalAlignment = Element.ALIGN_CENTER;
                cuerpo.BorderColor = new BaseColor(Color.DarkBlue);
                cuerpo.BackgroundColor = new BaseColor(Color.DarkBlue);

                PdfPCell cuerpo1 = new PdfPCell(new Paragraph("Unidad" + (char)00, fuente1));
                cuerpo1.Colspan = 1;
                cuerpo1.HorizontalAlignment = Element.ALIGN_CENTER;
                cuerpo1.BorderColor = new BaseColor(Color.DarkBlue);
                cuerpo1.BackgroundColor = new BaseColor(Color.DarkBlue);

                PdfPCell cuerpo2 = new PdfPCell(new Paragraph("Descripción" + (char)00, fuente1));
                cuerpo2.Colspan = 2;
                cuerpo2.HorizontalAlignment = Element.ALIGN_CENTER;
                cuerpo2.BorderColor = new BaseColor(Color.DarkBlue);
                cuerpo2.BackgroundColor = new BaseColor(Color.DarkBlue);

                PdfPCell cuerpo3 = new PdfPCell(new Paragraph("Costo Unitario" + (char)00, fuente1));
                cuerpo3.Colspan = 1;
                cuerpo3.HorizontalAlignment = Element.ALIGN_CENTER;
                cuerpo3.BorderColor = new BaseColor(Color.DarkBlue);
                cuerpo3.BackgroundColor = new BaseColor(Color.DarkBlue);

                PdfPCell cuerpo4 = new PdfPCell(new Paragraph("Monto" + (char)00, fuente1));
                cuerpo4.Colspan = 1;
                cuerpo4.HorizontalAlignment = Element.ALIGN_CENTER;
                cuerpo4.BorderColor = new BaseColor(Color.DarkBlue);
                cuerpo4.BackgroundColor = new BaseColor(Color.DarkBlue);

                table2.AddCell(cuerpo);
                table2.AddCell(cuerpo1);
                table2.AddCell(cuerpo2);
                table2.AddCell(cuerpo3);
                table2.AddCell(cuerpo4);
                int au = 0;
                for (int i = 0; i < Convert.ToInt32(arr[17]); i++)
                {
                    PdfPCell cuerpo5 = new PdfPCell(new Paragraph("" + compr[0 + au] + (char)00, fuente));
                    cuerpo5.Colspan = 1;
                    cuerpo5.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuerpo5.BorderColor = new BaseColor(Color.Black);

                    PdfPCell cuerpo6 = new PdfPCell(new Paragraph("" + compr[1 + au] + (char)00, fuente));
                    cuerpo6.Colspan = 1;
                    cuerpo6.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuerpo6.BorderColor = new BaseColor(Color.Black);

                    PdfPCell cuerpo7 = new PdfPCell(new Paragraph("" + compr[2 + au] + (char)00, fuente));
                    cuerpo7.Colspan = 2;
                    cuerpo7.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuerpo7.BorderColor = new BaseColor(Color.Black);

                    PdfPCell cuerpo8 = new PdfPCell(new Paragraph("" + compr[3 + au] + (char)00, fuente));
                    cuerpo8.Colspan = 1;
                    cuerpo8.HorizontalAlignment = Element.ALIGN_CENTER;
                    cuerpo8.BorderColor = new BaseColor(Color.Black);

                    PdfPCell cuerpo9 = new PdfPCell(new Paragraph("" + compr[4 + au] + (char)00, fuente));
                    cuerpo9.Colspan = 1;
                    cuerpo9.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cuerpo9.BorderColor = new BaseColor(Color.Black);

                    table2.AddCell(cuerpo5);
                    table2.AddCell(cuerpo6);
                    table2.AddCell(cuerpo7);
                    table2.AddCell(cuerpo8);
                    table2.AddCell(cuerpo9);
                    au += 5;
                }
                PdfPCell cuerpo10 = new PdfPCell(new Paragraph("Notas: " + arr[16] + (char)00, fuente));
                cuerpo10.Colspan = 4;
                cuerpo10.Rowspan = 3;
                cuerpo10.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cuerpo10.BorderColor = new BaseColor(Color.Black);

                PdfPCell cuerpo11 = new PdfPCell(new Paragraph("SubTotal:" + (char)00, fuente));
                cuerpo11.Colspan = 1;
                cuerpo11.HorizontalAlignment = Element.ALIGN_LEFT;
                cuerpo11.BorderColor = new BaseColor(Color.Black);
                cuerpo11.BackgroundColor = new BaseColor(Color.LightGray);

                PdfPCell cuerpo12 = new PdfPCell(new Paragraph(" $ " + arr[13] + (char)00, fuente));
                cuerpo12.Colspan = 1;
                cuerpo12.HorizontalAlignment = Element.ALIGN_RIGHT;
                cuerpo12.BorderColor = new BaseColor(Color.Black);

                PdfPCell cuerpo13 = new PdfPCell(new Paragraph("IVA:" + (char)00, fuente));
                cuerpo13.Colspan = 1;
                cuerpo13.HorizontalAlignment = Element.ALIGN_LEFT;
                cuerpo13.BorderColor = new BaseColor(Color.Black);
                cuerpo13.BackgroundColor = new BaseColor(Color.LightGray);

                PdfPCell cuerpo14 = new PdfPCell(new Paragraph(" $ " + arr[14] + +(char)00, fuente));
                cuerpo14.Colspan = 1;
                cuerpo14.HorizontalAlignment = Element.ALIGN_RIGHT;
                cuerpo14.BorderColor = new BaseColor(Color.Black);

                PdfPCell cuerpo15 = new PdfPCell(new Paragraph("Total:" + (char)00, fuente));
                cuerpo15.Colspan = 1;
                cuerpo15.HorizontalAlignment = Element.ALIGN_LEFT;
                cuerpo15.BorderColor = new BaseColor(Color.Black);
                cuerpo15.BackgroundColor = new BaseColor(Color.LightGray);

                PdfPCell cuerpo16 = new PdfPCell(new Paragraph(" $ " + arr[15] + (char)00, fuente));
                cuerpo16.Colspan = 1;
                cuerpo16.HorizontalAlignment = Element.ALIGN_RIGHT;
                cuerpo16.BorderColor = new BaseColor(Color.Black);


                table2.AddCell(cuerpo10);
                table2.AddCell(cuerpo11);
                table2.AddCell(cuerpo12);
                table2.AddCell(cuerpo13);
                table2.AddCell(cuerpo14);
                table2.AddCell(cuerpo15);
                table2.AddCell(cuerpo16);

                doc.Add(table);
                doc.Add(table1);
                doc.Add(new Paragraph("" + (char)00));
                doc.Add(table2);
                doc.Close();
                writer.Close();

                string ruta = rt.OC(oc + "");
                mar.marcaAguaDiagonal(rt.OCSimple(oc + ""), ruta, "O.C. " + oc, 1);
                arc.abirArchivo(ruta);

            }
            catch (Exception ex)
            {
                doc.Close();
                writer.Close();
                Console.WriteLine(ex.Message);
            }
        }
    }
}
