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

namespace Fergeda_2023.Finanzas.PDF
{
    class GeneraPDF
    {
        #region VARIABLES
        private FileStream fs;
        private PdfWriter writer;
        private Rutas rt = new Rutas();
        private Archivos arc = new Archivos();
        #endregion

        #region POLIZA DE CHEQUE
        public void polizaCheque(ArrayList arr, int numChe)
        {
            string ruta = rt.polizaCheque1(numChe);
            fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(PageSize.LETTER, 0, 0, 0, 0);
            writer = PdfWriter.GetInstance(doc, fs);

            int[] ancho = { 15, 135, 50, 50, 50 };
            int[] ancho2 = { 50, 50, 50, 50, 50, 50 };
            var fuente = FontFactory.GetFont("Arial Narrow", 12, new BaseColor(System.Drawing.Color.Black));
            var fuente2 = FontFactory.GetFont("Arial Narrow", 3, new BaseColor(System.Drawing.Color.Black));
            var fuente3 = FontFactory.GetFont("Arial Narrow", 2, new BaseColor(System.Drawing.Color.Black));
            var fuente4 = FontFactory.GetFont("Arial Narrow", 3, new BaseColor(System.Drawing.Color.Black));
            var fuente6 = FontFactory.GetFont("Arial Narrow", 5, new BaseColor(System.Drawing.Color.Black));
            var fuente5 = FontFactory.GetFont("Arial Narrow", 10, new BaseColor(System.Drawing.Color.Black));
            doc.AddTitle("Poliza de Cheque");
            doc.AddCreator("Angel Rios");

            doc.Open();

            PdfPTable table2 = new PdfPTable(6);
            table2.TotalWidth = 580f;
            table2.LockedWidth = true;
            table2.HorizontalAlignment = 1;
            table2.SetWidths(ancho2);

            PdfPCell vacia = new PdfPCell(new Paragraph("" + (char)00, fuente));
            vacia.Colspan = 6;
            vacia.HorizontalAlignment = Element.ALIGN_LEFT;
            vacia.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell sol1 = new PdfPCell(new Paragraph("IGT", fuente));
            sol1.Colspan = 1;
            sol1.HorizontalAlignment = Element.ALIGN_CENTER;
            sol1.BorderColor = new BaseColor(Color.Transparent);
            PdfPCell sol2 = new PdfPCell(new Paragraph("CGG", fuente));
            sol2.Colspan = 1;
            sol2.HorizontalAlignment = Element.ALIGN_CENTER;
            sol2.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell sol3 = new PdfPCell(new Paragraph("GRR", fuente));
            sol3.Colspan = 1;
            sol3.HorizontalAlignment = Element.ALIGN_CENTER;
            sol3.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell sol4 = new PdfPCell(new Paragraph("" + (char)00, fuente));
            sol4.Colspan = 1;
            sol4.HorizontalAlignment = Element.ALIGN_CENTER;
            sol4.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell sol5 = new PdfPCell(new Paragraph("" + numChe, fuente));
            sol5.Colspan = 1;
            sol5.HorizontalAlignment = Element.ALIGN_CENTER;
            sol5.BorderColor = new BaseColor(Color.Transparent);

            table2.AddCell(vacia);
            //table2.AddCell(vacia);

            table2.AddCell(sol1);
            table2.AddCell(sol2);
            table2.AddCell(sol3);
            table2.AddCell(sol4);
            table2.AddCell(sol4);
            table2.AddCell(sol5);


            PdfPTable table = new PdfPTable(5);
            table.TotalWidth = 580f;
            table.LockedWidth = true;
            table.HorizontalAlignment = 1;
            table.SetWidths(ancho);
            PdfPCell proyecto1 = new PdfPCell(new Paragraph("" + (char)00, fuente));
            proyecto1.Colspan = 1;
            proyecto1.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto1.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell proyecto2 = new PdfPCell(new Paragraph("" + (char)00, fuente2));/*espacio entre lineas*/
            proyecto2.Colspan = 1;
            proyecto2.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto2.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell proyecto3 = new PdfPCell(new Paragraph("" + (char)00, fuente3));/*espacio entre lineas*/
            proyecto3.Colspan = 1;
            proyecto3.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto3.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell proyecto4 = new PdfPCell(new Paragraph("" + (char)00, fuente4));/*espacio entre lineas*/
            proyecto4.Colspan = 1;
            proyecto4.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto4.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell proyecto5 = new PdfPCell(new Paragraph("" + (char)00, fuente6));/*espacio entre lineas*/
            proyecto5.Colspan = 1;
            proyecto5.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto5.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell fecha = new PdfPCell(new Paragraph("" + arr[0], fuente5));/*fecha*/
            fecha.Colspan = 1;
            fecha.HorizontalAlignment = Element.ALIGN_CENTER;
            fecha.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell portador = new PdfPCell(new Paragraph("" + arr[1], fuente5));/*Solicitante*/
            portador.Colspan = 3;
            portador.HorizontalAlignment = Element.ALIGN_LEFT;
            portador.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell monto = new PdfPCell(new Paragraph("" + arr[2], fuente5));/*importe*/
            monto.Colspan = 1;
            monto.HorizontalAlignment = Element.ALIGN_CENTER;
            monto.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell importeLetra = new PdfPCell(new Paragraph("" + arr[3], fuente5));/*importe con letra*/
            importeLetra.Colspan = 4;
            importeLetra.HorizontalAlignment = Element.ALIGN_LEFT;
            importeLetra.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell concepto = new PdfPCell(new Paragraph("" + arr[4], fuente5));/*Concepto Factura tablas*/
            concepto.Colspan = 3;
            concepto.HorizontalAlignment = Element.ALIGN_LEFT;
            concepto.BorderColor = new BaseColor(Color.Transparent);


            /**/
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(fecha);
            table.AddCell(proyecto1);

            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);

            table.AddCell(proyecto1);
            table.AddCell(portador);
            table.AddCell(monto);

            table.AddCell(proyecto5);
            table.AddCell(proyecto5);
            table.AddCell(proyecto5);
            table.AddCell(proyecto5);
            table.AddCell(proyecto5);

            table.AddCell(proyecto3);
            table.AddCell(importeLetra);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(concepto);
            table.AddCell(proyecto1);

            for (int i = 0; i < 155; i++)
            {
                table.AddCell(proyecto1);
            }
            //table.AddCell(proyecto1);

            /**/

            doc.Add(table);
            doc.Add(table2);
            doc.Close();
            writer.Close();
            arc.abirArchivo(ruta);



        }


        #endregion

        #region CHEQUE   

        public void cheque(ArrayList arr, int num)
        {
            //arreglos();
            string ruta = rt.cheque(num);
            fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
            Document doc = new Document(PageSize.LETTER, 0, 0, 0, 0);
            writer = PdfWriter.GetInstance(doc, fs);

            int[] ancho = { 75, 85, 45, 50, 50 };
            var fuente = FontFactory.GetFont("Arial Narrow", 12, new BaseColor(System.Drawing.Color.Black));
            var fuente2 = FontFactory.GetFont("Arial Narrow", 3, new BaseColor(System.Drawing.Color.Black));
            var fuente3 = FontFactory.GetFont("Arial Narrow", 2, new BaseColor(System.Drawing.Color.Black));
            var fuente4 = FontFactory.GetFont("Arial Narrow", 3, new BaseColor(System.Drawing.Color.Black));
            var fuente6 = FontFactory.GetFont("Arial Narrow", 5, new BaseColor(System.Drawing.Color.Black));
            var fuente5 = FontFactory.GetFont("Arial Narrow", 10, new BaseColor(System.Drawing.Color.Black));
            doc.AddTitle("Solicitud de Cheque");
            doc.AddCreator("Angel Rios");

            doc.Open();

            PdfPTable table = new PdfPTable(5);
            table.TotalWidth = 580f;
            table.LockedWidth = true;
            table.HorizontalAlignment = 1;
            table.SetWidths(ancho);

            PdfPCell proyecto1 = new PdfPCell(new Paragraph("" + (char)00, fuente));
            proyecto1.Colspan = 1;
            proyecto1.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto1.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell proyecto2 = new PdfPCell(new Paragraph("" + (char)00, fuente2));/*espacio entre lineas*/
            proyecto2.Colspan = 1;
            proyecto2.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto2.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell proyecto3 = new PdfPCell(new Paragraph("" + (char)00, fuente3));/*espacio entre lineas*/
            proyecto3.Colspan = 1;
            proyecto3.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto3.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell proyecto4 = new PdfPCell(new Paragraph("" + (char)00, fuente4));/*espacio entre lineas*/
            proyecto4.Colspan = 1;
            proyecto4.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto4.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell proyecto5 = new PdfPCell(new Paragraph("" + (char)00, fuente6));/*espacio entre lineas*/
            proyecto5.Colspan = 1;
            proyecto5.HorizontalAlignment = Element.ALIGN_LEFT;
            proyecto5.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell fecha = new PdfPCell(new Paragraph("" + arr[0], fuente));/*fecha*/
            fecha.Colspan = 1;
            fecha.HorizontalAlignment = Element.ALIGN_CENTER;
            fecha.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell portador = new PdfPCell(new Paragraph("" + arr[1], fuente5));/*Solicitante*/
            portador.Colspan = 3;
            portador.HorizontalAlignment = Element.ALIGN_LEFT;
            portador.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell monto = new PdfPCell(new Paragraph("" + arr[2], fuente));/*importe*/
            monto.Colspan = 1;
            monto.HorizontalAlignment = Element.ALIGN_CENTER;
            monto.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell importeLetra = new PdfPCell(new Paragraph("   " + arr[3], fuente5));/*importe con letra*/
            importeLetra.Colspan = 4;
            importeLetra.HorizontalAlignment = Element.ALIGN_LEFT;
            importeLetra.BorderColor = new BaseColor(Color.Transparent);

            /*Segundo cheque*/

            PdfPCell fecha2 = new PdfPCell(new Paragraph("" + arr[4], fuente));/*fecha*/
            fecha2.Colspan = 1;
            fecha2.HorizontalAlignment = Element.ALIGN_CENTER;
            fecha2.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell portador2 = new PdfPCell(new Paragraph("" + arr[5], fuente5));/*Solicitante*/
            portador2.Colspan = 3;
            portador2.HorizontalAlignment = Element.ALIGN_LEFT;
            portador2.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell monto2 = new PdfPCell(new Paragraph("" + arr[6], fuente));/*importe*/
            monto2.Colspan = 1;
            monto2.HorizontalAlignment = Element.ALIGN_CENTER;
            monto2.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell importeLetra2 = new PdfPCell(new Paragraph("   " + arr[7], fuente5));/*importe con letra*/
            importeLetra2.Colspan = 4;
            importeLetra2.HorizontalAlignment = Element.ALIGN_LEFT;
            importeLetra2.BorderColor = new BaseColor(Color.Transparent);
            /*tecer cheque*/
            PdfPCell fecha3 = new PdfPCell(new Paragraph("" + arr[8], fuente));/*fecha*/
            fecha3.Colspan = 1;
            fecha3.HorizontalAlignment = Element.ALIGN_CENTER;
            fecha3.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell portador3 = new PdfPCell(new Paragraph("" + arr[9], fuente5));/*Solicitante*/
            portador3.Colspan = 3;
            portador3.HorizontalAlignment = Element.ALIGN_LEFT;
            portador3.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell monto3 = new PdfPCell(new Paragraph("" + arr[10], fuente));/*importe*/
            monto3.Colspan = 1;
            monto3.HorizontalAlignment = Element.ALIGN_CENTER;
            monto2.BorderColor = new BaseColor(Color.Transparent);

            PdfPCell importeLetra3 = new PdfPCell(new Paragraph("   " + arr[11], fuente5));/*importe con letra*/
            importeLetra3.Colspan = 4;
            importeLetra3.HorizontalAlignment = Element.ALIGN_LEFT;
            importeLetra3.BorderColor = new BaseColor(Color.Transparent);

            /*primer cheque*/
            //table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            //table.AddCell(proyecto2); table.AddCell(proyecto2);

            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1);


            table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(fecha); table.AddCell(proyecto1);

            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2);

            table.AddCell(proyecto1);
            table.AddCell(portador);
            table.AddCell(monto);

            table.AddCell(proyecto5);
            table.AddCell(proyecto5);
            table.AddCell(proyecto5);
            table.AddCell(proyecto5);
            table.AddCell(proyecto5);

            table.AddCell(proyecto3);
            table.AddCell(importeLetra);
            /***/

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);
            table.AddCell(proyecto1);

            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);

            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);

            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);

            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            table.AddCell(proyecto3);
            /**********************segundo***************************/
            table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto2);
            table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto2); table.AddCell(proyecto2);
            /*table.AddCell(proyecto1);*/
            table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(fecha2); table.AddCell(proyecto1); table.AddCell(proyecto2);
            table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto2); table.AddCell(proyecto1); table.AddCell(portador2);
            table.AddCell(monto2); table.AddCell(proyecto5); table.AddCell(proyecto5);
            table.AddCell(proyecto5); table.AddCell(proyecto5); table.AddCell(proyecto5);
            table.AddCell(proyecto3); table.AddCell(importeLetra2); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3);
            /************************************************************************/
            table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto2);

            table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);

            /*table.AddCell(proyecto2);*/ /*table.AddCell(proyecto1);*/
                                          //table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
                                          /*table.AddCell(proyecto1);*/
                                          //table.AddCell(proyecto2); table.AddCell(proyecto2);
                                          //table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(fecha3); table.AddCell(proyecto1); table.AddCell(proyecto2);
            table.AddCell(proyecto2); table.AddCell(proyecto2); table.AddCell(proyecto2);
            table.AddCell(proyecto2); table.AddCell(proyecto1); table.AddCell(portador3);
            table.AddCell(monto3); table.AddCell(proyecto5); table.AddCell(proyecto5);
            table.AddCell(proyecto5); table.AddCell(proyecto5); table.AddCell(proyecto5);
            table.AddCell(proyecto3); table.AddCell(importeLetra3); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto1);
            table.AddCell(proyecto1); table.AddCell(proyecto1); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3); table.AddCell(proyecto3);
            table.AddCell(proyecto3); table.AddCell(proyecto3);


            /******************************************************/
            //for (int i = 0; i < 2; i++)
            //{
            doc.Add(table);           //}


            doc.Close();
            writer.Close();
            arc.abirArchivo(ruta);
        }
        #endregion

        
    }
}
