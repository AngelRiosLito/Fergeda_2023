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

namespace Fergeda_2023.General.PDF
{
    class SolicitudChequePDF
    {
        int contador = 0;
        private Rutas Rt = new Rutas();
        Consultas x = new Consultas();
        private FormatoFechas fechas = new FormatoFechas();
        private FileStream fs;
        private PdfWriter writer;
        private Document doc;
        private Archivos arc = new Archivos();

        List<string> datos = new List<string>();
        List<string> listado = new List<string>();
        List<string> dat = new List<string>();
        List<string> listaTablaPP = new List<string>() { "Marcelino Dávalos #30, Col. Algarín \n C.P. 06880, Del. Cuahutemoc", "Norte 35 #983 Int. 6, Col. Industrial Vallejo, \n C.P. 07700, Del. Gustavo A. Madero" };
        List<string> listaFirmas = new List<string>() { "Firma Solicitante:", " ", " ", "Firma de autorización: ", " " };

        private void cheque(string folio)
        {
            listado.Clear();
            dat.Clear();

            dat = x.Litado("SELECT concat(if(factura='.','N/A',factura),'|',concepto,'|',importe) AS RESULT from fergeda.f_solicitudlistado where fk_solicitud=" + folio + ";");
            char[] sp = { '|' };
            for (int i = 0; i < dat.Count; i++)
            {
                string[] arr = dat[i].Split(sp);
                listado.Add(arr[0]);
                listado.Add(arr[1]);
                listado.Add(moneda(double.Parse(arr[2])));
            }
        }
        string conver = "";
        public string moneda(double valor)
        {
            conver = "";
            conver = valor.ToString("N2");
            return conver;
        }
        private void llenado(string folio)
        {
            datos.Clear();
            datos.Add(x.elem3("select distinct(fecha_pago) FROM fergeda.f_solicitudlistado where fk_solicitud ='" + folio + "';"));
            datos.Add(x.elem3("select distinct(fecha) FROM fergeda.f_solicitudlistado where fk_solicitud =" + folio + ";"));
            datos.Add(x.elem3("SELECT concat(b.nombre,' ',b.ap_paterno, ' ',b.ap_materno) from fergeda.f_solicitudcheque a inner join fergeda.r_empleado b on a.fk_empleado = b.idr_empleado where a.idf_solicitudcheque =" + folio + ";"));
            datos.Add(x.elem3("SELECT departamento FROM fergeda.r_idempleados where idr_empleado=(SELECT fk_empleado FROM fergeda.f_solicitudcheque where idf_solicitudcheque='" + folio + "'); "));
            datos.Add(x.elem3("select distinct(importe_cheque) FROM fergeda.f_solicitudlistado where fk_solicitud =" + folio + ";"));
            datos.Add(x.elem3("select distinct(orden_impresion) FROM fergeda.f_solicitudlistado where fk_solicitud =" + folio + ";"));
            datos.Add(x.elem3("select distinct(a.proveedor) from fergeda.f_proveedor a inner join fergeda.f_solicitudlistado b on a.idf_proveedor = b.fk_proveedor where b.fk_solicitud =" + folio + ";"));
            datos.Add(x.elem3("select distinct(numero_Oc) FROM fergeda.f_solicitudlistado where fk_solicitud =" + folio + ";"));
            datos.Add(x.elem3("select distinct(subtotal) from fergeda.f_solicitudlistado where fk_solicitud =" + folio + ";"));
            datos.Add(x.elem3("select distinct(iva) FROM fergeda.f_solicitudlistado where fk_solicitud = '" + folio + "';"));
            datos.Add(x.elem3("select distinct(total) FROM fergeda.f_solicitudlistado where fk_solicitud = '" + folio + "';"));
            datos.Add(x.elem3("select distinct(retencion) FROM fergeda.f_solicitudlistado where fk_solicitud = '" + folio + "';"));
            datos.Add(x.elem3("select distinct(motivo_cancelacion) from fergeda.f_solicitudlistado where fk_solicitud = '" + folio + "';"));

        }

        public void generarCheque(string folio)
        {
            try
            {
                string ruta = Rt.SolicitudesCheque(folio);
                cheque(folio);
                llenado(folio);
                fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
                doc = new Document(PageSize.LETTER, 20, 20, 20, 20);
                writer = PdfWriter.GetInstance(doc, fs);

                #region anchos
                int[] ancho = { 100, 360, 100 };
                int[] ancho2 = { 150, 270, 40, 100 };
                int[] anchoFechas = { 70, 130, 220, 40, 100 };
                int[] anchoNombreSolicita = { 150, 410 };
                int[] anchoDept = { 70, 490 };
                int[] anchoImporte = { 90, 470 };
                int[] anchoOI = { 120, 440 };
                int[] anchoProveedor = { 50, 480 };
                int[] anchoOC = { 100, 460 };
                int[] anchoDatos = { 150, 270, 40 };
                int[] anchoTotales = { 400, 60, 100 };
                int[] anchoPP = { 280, 280 };
                int[] anchoFirmas = { 70, 170, 50, 90, 170 };
                #endregion

                #region fuentes
                var fuente = FontFactory.GetFont("Aria Narrow", 9, new BaseColor(Color.Black));
                var fuenteDatos = FontFactory.GetFont("Aria Narrow", 9, new BaseColor(Color.Black));
                var fuentem = FontFactory.GetFont("Aria Narrow", 6, new BaseColor(Color.Black));
                var fuentemin = FontFactory.GetFont("Aria Narrow", 3, new BaseColor(Color.Black));

                #endregion

                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"logo-fergeda.png");
                iTextSharp.text.Image icono = iTextSharp.text.Image.GetInstance(@"usu.png");

                

                doc.Open();
                imagen.SetAbsolutePosition(0f, 710f);
                imagen.ScalePercent(52);
                doc.Add(imagen);
                #region encabezado                
                icono.ScalePercent(7);
                #endregion

                #region Tablas
                PdfPTable tabla1 = new PdfPTable(4);
                tabla1.TotalWidth = 560f;
                tabla1.LockedWidth = true;
                tabla1.HorizontalAlignment = 1;
                tabla1.SetWidths(ancho2);

                PdfPTable tablaFechas = new PdfPTable(5);
                tablaFechas.TotalWidth = 560f;
                tablaFechas.LockedWidth = true;
                tablaFechas.HorizontalAlignment = 1;
                tablaFechas.SetWidths(anchoFechas);

                PdfPTable tablaNombre = new PdfPTable(2);
                tablaNombre.TotalWidth = 560f;
                tablaNombre.LockedWidth = true;
                tablaNombre.HorizontalAlignment = 1;
                tablaNombre.SetWidths(anchoNombreSolicita);

                PdfPTable tablaDept = new PdfPTable(2);
                tablaDept.TotalWidth = 560f;
                tablaDept.LockedWidth = true;
                tablaDept.HorizontalAlignment = 1;
                tablaDept.SetWidths(anchoDept);

                PdfPTable tablaImporte = new PdfPTable(2);
                tablaImporte.TotalWidth = 560f;
                tablaImporte.LockedWidth = true;
                tablaImporte.HorizontalAlignment = 1;
                tablaImporte.SetWidths(anchoImporte);

                PdfPTable tablaOI = new PdfPTable(2);
                tablaOI.TotalWidth = 560f;
                tablaOI.LockedWidth = true;
                tablaOI.HorizontalAlignment = 1;
                tablaOI.SetWidths(anchoOI);

                PdfPTable tablaProveedor = new PdfPTable(2);
                tablaProveedor.TotalWidth = 560f;
                tablaProveedor.LockedWidth = true;
                tablaProveedor.HorizontalAlignment = 1;
                tablaProveedor.SetWidths(anchoProveedor);

                PdfPTable tablaOC = new PdfPTable(2);
                tablaOC.TotalWidth = 560f;
                tablaOC.LockedWidth = true;
                tablaOC.HorizontalAlignment = 1;
                tablaOC.SetWidths(anchoOC);

                PdfPTable tablaCuerpo = new PdfPTable(3);
                tablaCuerpo.TotalWidth = 560f;
                tablaCuerpo.LockedWidth = true;
                tablaCuerpo.HorizontalAlignment = 1;
                tablaCuerpo.SetWidths(ancho);

                PdfPTable tablaTotales = new PdfPTable(3);
                tablaTotales.TotalWidth = 560f;
                tablaTotales.LockedWidth = true;
                tablaTotales.HorizontalAlignment = 1;
                tablaTotales.SetWidths(anchoTotales);

                PdfPCell a = new PdfPCell(new Paragraph(""+(char)00, fuentem));
                a.Colspan = 3;
                a.HorizontalAlignment = Element.ALIGN_LEFT;
                a.BorderColor = new BaseColor(Color.Black);
                a.BorderWidthBottom = 0f;
                a.BorderWidthLeft = 0f;
                a.BorderWidthRight = 0f;
                a.BorderWidthTop = 0f;

                PdfPCell a3 = new PdfPCell(new Paragraph("RFFI-01-01" + (char)00, fuentem));
                a3.Colspan = 1;
                a3.HorizontalAlignment = Element.ALIGN_RIGHT;
                a3.BorderColor = new BaseColor(Color.Black);
                a3.BorderWidthBottom = 0f;
                a3.BorderWidthLeft = 0f;
                a3.BorderWidthRight = 0f;
                a3.BorderWidthTop = 0f;

                PdfPCell a4 = new PdfPCell(new Paragraph("Solicitud de Cheque" + (char)00, fuente));
                a4.Colspan = 1;
                a4.HorizontalAlignment = Element.ALIGN_LEFT;
                a4.BorderColor = new BaseColor(Color.Black);
                a4.BorderWidthBottom = 0f;
                a4.BorderWidthLeft = 0f;
                a4.BorderWidthRight = 0f;
                a4.BorderWidthTop = 0f;

                PdfPCell u = new PdfPCell(icono);
                u.Colspan = 3;
                u.HorizontalAlignment = Element.ALIGN_RIGHT;
                u.BorderColor = new BaseColor(Color.Black);
                u.BorderWidthBottom = 0f;
                u.BorderWidthLeft = 0f;
                u.BorderWidthRight = 0f;
                u.BorderWidthTop = 0f;

                PdfPCell a2 = new PdfPCell(new Paragraph("Folio:  " + (char)00, fuente));
                a2.Colspan = 1;
                a2.HorizontalAlignment = Element.ALIGN_RIGHT;
                a2.BorderColor = new BaseColor(Color.Black);
                a2.BackgroundColor = new BaseColor(Color.LightGray);
                a2.BorderWidthBottom = 0f;
                a2.BorderWidthLeft = 0f;
                a2.BorderWidthRight = 0f;
                a2.BorderWidthTop = 0f;

                var a5 = new PdfPCell(new Phrase("" + folio + (char)00, fuenteDatos));
                a5.Colspan = 1;
                a5.HorizontalAlignment = Element.ALIGN_CENTER;
                a5.BorderColor = new BaseColor(Color.LightGray);
                a5.BorderWidthBottom = 1f;
                a5.BorderWidthLeft = 0f;
                a5.BorderWidthRight = 1f;
                a5.BorderWidthTop = 0f;


                PdfPCell vacia = new PdfPCell(new Paragraph(" " + (char)00, fuentem));
                vacia.Colspan = 4;
                vacia.HorizontalAlignment = Element.ALIGN_CENTER;
                vacia.BorderColor = new BaseColor(Color.Black);
                vacia.BorderWidthBottom = 0f;
                vacia.BorderWidthLeft = 0f;
                vacia.BorderWidthRight = 0f;
                vacia.BorderWidthTop = 0f;

                PdfPCell v = new PdfPCell(new Paragraph(" " + (char)00, fuente));
                v.Colspan = 2;
                v.HorizontalAlignment = Element.ALIGN_CENTER;
                v.BorderColor = new BaseColor(Color.Black);
                v.BorderWidthBottom = 0f;
                v.BorderWidthLeft = 0f;
                v.BorderWidthRight = 0f;
                v.BorderWidthTop = 0f;

                PdfPCell v1 = new PdfPCell(new Paragraph(" " + (char)00, fuentemin));
                v1.Colspan = 4;
                v1.HorizontalAlignment = Element.ALIGN_CENTER;
                v1.BorderColor = new BaseColor(Color.Black);
                v1.BorderWidthBottom = 0f;
                v1.BorderWidthLeft = 0f;
                v1.BorderWidthRight = 0f;
                v1.BorderWidthTop = 0f;

                PdfPCell vt = new PdfPCell(new Paragraph(" " + (char)00, fuentemin));
                vt.Colspan = 4;
                vt.HorizontalAlignment = Element.ALIGN_CENTER;
                vt.BorderColor = new BaseColor(Color.LightGray);
                vt.BorderWidthBottom = 0f;
                vt.BorderWidthLeft = 0f;
                vt.BorderWidthRight = 1f;
                vt.BorderWidthTop = 0f;

                PdfPCell eTotales = new PdfPCell(new Paragraph(" " + (char)00, fuentemin));
                eTotales.Colspan = 4;
                eTotales.HorizontalAlignment = Element.ALIGN_CENTER;
                eTotales.BorderColor = new BaseColor(Color.LightGray);
                eTotales.BorderWidthBottom = 0f;
                eTotales.BorderWidthLeft = 0f;
                eTotales.BorderWidthRight = 1f;
                eTotales.BorderWidthTop = 0f;
                eTotales.PaddingTop = 0;


                tabla1.AddCell(a);
                tabla1.AddCell(a3);
                tabla1.AddCell(u);
                tabla1.AddCell(a4);
                tabla1.AddCell(v1);
                tabla1.AddCell(v);
                tabla1.AddCell(a2);
                tabla1.AddCell(a5);
                tabla1.AddCell(v1);
                #endregion

                PdfPCell a6 = new PdfPCell(new Paragraph("Fecha de pago: " + (char)00, fuente));
                a6.Colspan = 1;
                a6.HorizontalAlignment = Element.ALIGN_LEFT;
                a6.BorderColor = new BaseColor(Color.Black);
                a6.BackgroundColor = new BaseColor(Color.LightGray);
                a6.BorderWidthBottom = 0f;
                a6.BorderWidthLeft = 0f;
                a6.BorderWidthRight = 0f;
                a6.BorderWidthTop = 0f;
                tablaFechas.AddCell(a6);

                PdfPCell a7 = new PdfPCell(new Paragraph(" " + fechas.foratoFecha2(DateTime.Parse(datos[0]).ToString("dd/MM/yyyy")) + (char)00, fuente));
                a7.Colspan = 1;
                a7.HorizontalAlignment = Element.ALIGN_CENTER;
                a7.BorderColor = new BaseColor(Color.LightGray);
                a7.BorderWidthBottom = 1f;
                a7.BorderWidthLeft = 0f;
                a7.BorderWidthRight = 1f;
                a7.BorderWidthTop = 0f;
                tablaFechas.AddCell(a7);

                PdfPCell e1 = new PdfPCell(new Paragraph("" + (char)00, fuente));
                e1.Colspan = 1;
                e1.BorderWidthBottom = 0f;
                e1.BorderWidthLeft = 0f;
                e1.BorderWidthRight = 0f;
                e1.BorderWidthTop = 0f;
                tablaFechas.AddCell(e1);

                PdfPCell a8 = new PdfPCell(new Paragraph("Fecha:" + (char)00, fuente));
                a8.Colspan = 1;
                a8.HorizontalAlignment = Element.ALIGN_RIGHT;
                a8.BorderColor = new BaseColor(Color.Black);
                a8.BackgroundColor = new BaseColor(Color.LightGray);
                a8.BorderWidthBottom = 0f;
                a8.BorderWidthLeft = 0f;
                a8.BorderWidthRight = 0f;
                a8.BorderWidthTop = 0f;
                tablaFechas.AddCell(a8);

                PdfPCell a9 = new PdfPCell(new Paragraph(" " + fechas.foratoFecha2(DateTime.Parse(datos[0]).ToString("dd/MM/yyyy")) + (char)00, fuente));
                a9.Colspan = 1;
                a9.HorizontalAlignment = Element.ALIGN_CENTER;
                a9.BorderColor = new BaseColor(Color.LightGray);
                a9.BorderWidthBottom = 1f;
                a9.BorderWidthLeft = 0f;
                a9.BorderWidthRight = 1f;
                a9.BorderWidthTop = 0f;
                tablaFechas.AddCell(a9);

                tablaFechas.AddCell(v1);
                tablaFechas.AddCell(v1);
                tablaFechas.AddCell(v1);

                PdfPCell a10 = new PdfPCell(new Paragraph("Nombre de quien solicita el cheque:" + (char)00, fuente));
                a10.Colspan = 1;
                a10.HorizontalAlignment = Element.ALIGN_LEFT;
                a10.BorderColor = new BaseColor(Color.Black);
                a10.BackgroundColor = new BaseColor(Color.LightGray);
                a10.BorderWidthBottom = 0f;
                a10.BorderWidthLeft = 0f;
                a10.BorderWidthRight = 0f;
                a10.BorderWidthTop = 0f;
                tablaNombre.AddCell(a10);

                PdfPCell a11 = new PdfPCell(new Paragraph("" + datos[2] + (char)00, fuenteDatos));
                a11.Colspan = 3;
                a11.HorizontalAlignment = Element.ALIGN_LEFT;
                a11.BorderColor = new BaseColor(Color.LightGray);
                a11.BorderWidthBottom = 1f;
                a11.BorderWidthLeft = 0f;
                a11.BorderWidthRight = 1f;
                a11.BorderWidthTop = 0f;
                tablaNombre.AddCell(a11);
                tablaNombre.AddCell(v1);

                PdfPCell a12 = new PdfPCell(new Paragraph("Departamento:" + (char)00, fuente));
                a12.Colspan = 1;
                a12.HorizontalAlignment = Element.ALIGN_LEFT;
                a12.BorderColor = new BaseColor(Color.Black);
                a12.BackgroundColor = new BaseColor(Color.LightGray);
                a12.BorderWidthBottom = 0f;
                a12.BorderWidthLeft = 0f;
                a12.BorderWidthRight = 0f;
                a12.BorderWidthTop = 0f;
                tablaDept.AddCell(a12);

                PdfPCell a13 = new PdfPCell(new Paragraph("" + datos[3] + (char)00, fuenteDatos));
                a13.Colspan = 3;
                a13.HorizontalAlignment = Element.ALIGN_LEFT;
                a13.BorderColor = new BaseColor(Color.LightGray);
                a13.BorderWidthBottom = 1f;
                a13.BorderWidthLeft = 0f;
                a13.BorderWidthRight = 1f;
                a13.BorderWidthTop = 0f;
                tablaDept.AddCell(a13);
                tablaDept.AddCell(v1);

                PdfPCell a14 = new PdfPCell(new Paragraph("Importe del cheque:" + (char)00, fuente));
                a14.Colspan = 1;
                a14.HorizontalAlignment = Element.ALIGN_LEFT;
                a14.BorderColor = new BaseColor(Color.Black);
                a14.BackgroundColor = new BaseColor(Color.LightGray);
                a14.BorderWidthBottom = 0f;
                a14.BorderWidthLeft = 0f;
                a14.BorderWidthRight = 0f;
                a14.BorderWidthTop = 0f;
                tablaImporte.AddCell(a14);

                PdfPCell a15 = new PdfPCell(new Paragraph("" + moneda(double.Parse(datos[4])) + (char)00, fuenteDatos));
                a15.Colspan = 3;
                a15.HorizontalAlignment = Element.ALIGN_LEFT;
                a15.BorderColor = new BaseColor(Color.LightGray);
                a15.BorderWidthBottom = 1f;
                a15.BorderWidthLeft = 0f;
                a15.BorderWidthRight = 1f;
                a15.BorderWidthTop = 0f;
                tablaImporte.AddCell(a15);
                tablaImporte.AddCell(v1);

                PdfPCell a16 = new PdfPCell(new Paragraph("No. de Orden de Impresion:" + (char)00, fuente));
                a16.Colspan = 1;
                a16.HorizontalAlignment = Element.ALIGN_LEFT;
                a16.BorderColor = new BaseColor(Color.Black);
                a16.BackgroundColor = new BaseColor(Color.LightGray);
                a16.BorderWidthBottom = 0f;
                a16.BorderWidthLeft = 0f;
                a16.BorderWidthRight = 0f;
                a16.BorderWidthTop = 0f;
                tablaOI.AddCell(a16);

                PdfPCell a17 = new PdfPCell(new Paragraph("" + datos[5] + (char)00, fuenteDatos));
                a17.Colspan = 3;
                a17.HorizontalAlignment = Element.ALIGN_LEFT;
                a17.BorderColor = new BaseColor(Color.LightGray);
                a17.BorderWidthBottom = 1f;
                a17.BorderWidthLeft = 0f;
                a17.BorderWidthRight = 1f;
                a17.BorderWidthTop = 0f;
                tablaOI.AddCell(a17);
                tablaOI.AddCell(v1);

                PdfPCell a18 = new PdfPCell(new Paragraph("Proveedor:" + (char)00, fuente));
                a18.Colspan = 1;
                a18.HorizontalAlignment = Element.ALIGN_LEFT;
                a18.BorderColor = new BaseColor(Color.Black);
                a18.BackgroundColor = new BaseColor(Color.LightGray);
                a18.BorderWidthBottom = 0f;
                a18.BorderWidthLeft = 0f;
                a18.BorderWidthRight = 0f;
                a18.BorderWidthTop = 0f;
                tablaProveedor.AddCell(a18);

                PdfPCell a19 = new PdfPCell(new Paragraph("" + datos[6] + (char)00, fuenteDatos));
                a19.Colspan = 3;
                a19.HorizontalAlignment = Element.ALIGN_LEFT;
                a19.BorderColor = new BaseColor(Color.LightGray);
                a19.BorderWidthBottom = 1f;
                a19.BorderWidthLeft = 0f;
                a19.BorderWidthRight = 1f;
                a19.BorderWidthTop = 0f;
                tablaProveedor.AddCell(a19);
                tablaProveedor.AddCell(v1);

                PdfPCell a20 = new PdfPCell(new Paragraph("No. Orden de Compra:" + (char)00, fuente));
                a20.Colspan = 1;
                a20.HorizontalAlignment = Element.ALIGN_LEFT;
                a20.BorderColor = new BaseColor(Color.Black);
                a20.BackgroundColor = new BaseColor(Color.LightGray);
                a20.BorderWidthBottom = 0f;
                a20.BorderWidthLeft = 0f;
                a20.BorderWidthRight = 0f;
                a20.BorderWidthTop = 0f;
                tablaOC.AddCell(a20);

                PdfPCell a21 = new PdfPCell(new Paragraph("" + datos[7] + (char)00, fuente));
                a21.Colspan = 3;
                a21.HorizontalAlignment = Element.ALIGN_LEFT;
                a21.BorderColor = new BaseColor(Color.LightGray);
                a21.BorderWidthBottom = 1f;
                a21.BorderWidthLeft = 0f;
                a21.BorderWidthRight = 1f;
                a21.BorderWidthTop = 0f;
                tablaOC.AddCell(a21);
                tablaOC.AddCell(v1);
                tablaOC.AddCell(v1);
                tablaOC.AddCell(v1);
                tablaOC.AddCell(v1);

                PdfPCell b = new PdfPCell(new Paragraph("No. Factura" + (char)00, fuente));
                b.Colspan = 1;
                b.HorizontalAlignment = Element.ALIGN_CENTER;
                b.BorderColor = new BaseColor(Color.Black);
                b.BackgroundColor = new BaseColor(Color.LightGray);
                b.BorderWidthBottom = 0f;
                b.BorderWidthLeft = 0f;
                b.BorderWidthRight = 0f;
                b.BorderWidthTop = 0f;
                tablaCuerpo.AddCell(b);

                PdfPCell b1 = new PdfPCell(new Paragraph("Concepto de Pago" + (char)00, fuente));
                b1.Colspan = 1;
                b1.HorizontalAlignment = Element.ALIGN_CENTER;
                b1.BorderColor = new BaseColor(Color.Black);
                b1.BackgroundColor = new BaseColor(Color.LightGray);
                b1.BorderWidthBottom = 0f;
                b1.BorderWidthLeft = 0f;
                b1.BorderWidthRight = 0f;
                b1.BorderWidthTop = 0f;
                tablaCuerpo.AddCell(b1);

                PdfPCell b2 = new PdfPCell(new Paragraph("Importe" + (char)00, fuente));
                b2.Colspan = 1;
                b2.HorizontalAlignment = Element.ALIGN_CENTER;
                b2.BorderColor = new BaseColor(Color.Black);
                b2.BackgroundColor = new BaseColor(Color.LightGray);
                b2.BorderWidthBottom = 0f;
                b2.BorderWidthLeft = 0f;
                b2.BorderWidthRight = 0f;
                b2.BorderWidthTop = 0f;
                tablaCuerpo.AddCell(b2);

                for (int i = 0; i < listado.Count; i++)
                {
                    PdfPCell b10 = new PdfPCell(new Paragraph("" + listado[i] + (char)00, fuente));
                    b10.Colspan = 1;
                    b10.BorderColor = new BaseColor(Color.LightGray);
                    b10.BorderWidthBottom = 0f;
                    b10.BorderWidthLeft = 1f;
                    b10.BorderWidthRight = 1f;
                    b10.BorderWidthTop = 0f;
                    if (contador.Equals(3))
                        contador = 0;

                    b10.HorizontalAlignment = (contador.Equals(2) ? Element.ALIGN_RIGHT : Element.ALIGN_CENTER);
                    tablaCuerpo.AddCell(b10);
                    contador++;
                }

                PdfPCell tt = new PdfPCell(new Paragraph("" + (char)00, fuente));
                tt.Colspan = 1;
                tt.BorderColor = new BaseColor(Color.LightGray);
                tt.BorderWidthBottom = 0f;
                tt.BorderWidthLeft = 0f;
                tt.BorderWidthRight = 0f;
                tt.BorderWidthTop = 1f;
                tablaTotales.AddCell(tt);

                PdfPCell t = new PdfPCell(new Paragraph("" + (char)00, fuente));
                t.Colspan = 1;
                t.HorizontalAlignment = Element.ALIGN_RIGHT;
                t.BorderWidthBottom = 0f;
                t.BorderWidthLeft = 0f;
                t.BorderWidthRight = 0f;
                t.BorderWidthTop = 0f;

                PdfPCell b3 = new PdfPCell(new Paragraph("SUBTOTAL" + (char)00, fuente));
                b3.Colspan = 1;
                b3.HorizontalAlignment = Element.ALIGN_RIGHT;
                b3.BorderColor = new BaseColor(Color.LightGray);
                b3.BackgroundColor = new BaseColor(Color.LightGray);
                b3.BorderWidthBottom = 0f;
                b3.BorderWidthLeft = 1f;
                b3.BorderWidthRight = 0f;
                b3.BorderWidthTop = 1f;
                tablaTotales.AddCell(b3);

                PdfPCell b4 = new PdfPCell(new Paragraph("$" + moneda(double.Parse(datos[8])) + (char)00, fuente));
                b4.Colspan = 1;
                b4.HorizontalAlignment = Element.ALIGN_RIGHT;
                b4.BorderColor = new BaseColor(Color.LightGray);
                b4.BorderWidthBottom = 0f;
                b4.BorderWidthLeft = 1f;
                b4.BorderWidthRight = 1f;
                b4.BorderWidthTop = 1f;
                tablaTotales.AddCell(b4);
                tablaTotales.AddCell(eTotales);
                PdfPCell b13 = new PdfPCell(new Paragraph("IVA" + (char)00, fuente));
                b13.Colspan = 1;
                b13.HorizontalAlignment = Element.ALIGN_RIGHT;
                b13.BorderColor = new BaseColor(Color.LightGray);
                b13.BackgroundColor = new BaseColor(Color.LightGray);
                b13.BorderWidthBottom = 0f;
                b13.BorderWidthLeft = 1f;
                b13.BorderWidthRight = 0f;
                b13.BorderWidthTop = 0f;

                PdfPCell b11 = new PdfPCell(new Paragraph("$" + moneda(double.Parse(datos[9])) + (char)00, fuente));
                b11.Colspan = 1;
                b11.HorizontalAlignment = Element.ALIGN_RIGHT;
                b11.BorderColor = new BaseColor(Color.LightGray);
                b11.BorderWidthBottom = 0f;
                b11.BorderWidthLeft = 1f;
                b11.BorderWidthRight = 1f;
                b11.BorderWidthTop = 0f;

                if (!datos[9].Equals("0"))
                {
                    tablaTotales.AddCell(t);
                    tablaTotales.AddCell(b13);
                    tablaTotales.AddCell(b11);
                    tablaTotales.AddCell(eTotales);
                    tablaTotales.AddCell(t);
                }

                PdfPCell b12 = new PdfPCell(new Paragraph("RETENCIÓN" + (char)00, fuente));
                b12.Colspan = 1;
                b12.HorizontalAlignment = Element.ALIGN_RIGHT;
                b12.BorderColor = new BaseColor(Color.LightGray);
                b12.BorderWidthBottom = 0f;
                b12.BorderWidthLeft = 0f;
                b12.BorderWidthRight = 0f;
                b12.BorderWidthTop = 0f;

                PdfPCell b9 = new PdfPCell(new Paragraph("$" + moneda(double.Parse(datos[11])) + (char)00, fuente));
                b9.Colspan = 1;
                b9.HorizontalAlignment = Element.ALIGN_CENTER;
                b9.BorderColor = new BaseColor(Color.LightGray);
                b9.BorderWidthBottom = 0f;
                b9.BorderWidthLeft = 1f;
                b9.BorderWidthRight = 1f;
                b9.BorderWidthTop = 0f;

                if (!datos[11].Equals("0"))
                {
                    tablaTotales.AddCell(b12);
                    tablaTotales.AddCell(b9);
                    tablaTotales.AddCell(eTotales);
                    tablaTotales.AddCell(t);
                }

                if (datos[9].Equals("0") && datos[11].Equals("0"))
                {
                    tablaTotales.AddCell(eTotales);
                    tablaTotales.AddCell(t);
                }

                PdfPCell b5 = new PdfPCell(new Paragraph("TOTAL" + (char)00, fuente));
                b5.Colspan = 1;
                b5.HorizontalAlignment = Element.ALIGN_RIGHT;
                b5.BackgroundColor = new BaseColor(Color.LightGray);
                b5.BorderWidthBottom = 0f;
                b5.BorderWidthLeft = 0f;
                b5.BorderWidthRight = 0f;
                b5.BorderWidthTop = 0f;
                tablaTotales.AddCell(b5);

                PdfPCell b6 = new PdfPCell(new Paragraph("$" + moneda(double.Parse(datos[10])) + (char)00, fuente));
                b6.Colspan = 1;
                b6.HorizontalAlignment = Element.ALIGN_RIGHT;
                b6.BorderColor = new BaseColor(Color.LightGray);
                b6.BorderWidthBottom = 1f;
                b6.BorderWidthLeft = 1f;
                b6.BorderWidthRight = 1f;
                b6.BorderWidthTop = 0f;
                tablaTotales.AddCell(b6);

                tablaTotales.AddCell(v1);
                tablaTotales.AddCell(v1);
                tablaTotales.AddCell(v1);

                PdfPCell b7 = new PdfPCell(new Paragraph(" " + datos[12] + "\n\n\n\n" + (char)00, fuente));
                b7.Colspan = 2;
                b7.HorizontalAlignment = Element.ALIGN_LEFT;
                b7.BorderColor = new BaseColor(Color.LightGray);
                b7.BorderWidthBottom = 1f;
                b7.BorderWidthLeft = 1f;
                b7.BorderWidthRight = 1f;
                b7.BorderWidthTop = 1f;
                tablaTotales.AddCell(b7);

                PdfPCell b8 = new PdfPCell(new Paragraph(" " + (char)00, fuente));
                b8.Colspan = 2;
                b8.HorizontalAlignment = Element.ALIGN_CENTER;
                b8.BorderColor = new BaseColor(Color.LightGray);
                b8.BorderWidthBottom = 0f;
                b8.BorderWidthLeft = 0f;
                b8.BorderWidthRight = 0f;
                b8.BorderWidthTop = 0f;
                tablaTotales.AddCell(b8);

                tablaTotales.AddCell(v1);
                tablaTotales.AddCell(v1);

                PdfPTable firmas = new PdfPTable(5);
                firmas.TotalWidth = 550;
                firmas.SetWidths(anchoFirmas);
                for (int i = 0; i < 5; i++)
                {
                    Chunk myFirmas;
                    myFirmas = new Chunk(listaFirmas[i], FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8));
                    PdfPCell footerFirmas = new PdfPCell(new Phrase(myFirmas));
                    footerFirmas.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    footerFirmas.BorderWidthBottom = footerFirmas.BorderWidthLeft = footerFirmas.BorderWidthRight = footerFirmas.BorderWidthTop = 0f;
                    footerFirmas.HorizontalAlignment = Element.ALIGN_LEFT;
                    if (i.Equals(0) || i.Equals(3))
                        footerFirmas.BackgroundColor = new BaseColor(Color.LightGray);

                    if (i.Equals(1) || i.Equals(4))
                    {
                        footerFirmas.BorderColorRight = new BaseColor(Color.LightGray);
                        footerFirmas.BorderWidthBottom = 1f;
                        footerFirmas.BorderWidthRight = 1f;
                        footerFirmas.BorderColor = new BaseColor(Color.LightGray);
                    }

                    firmas.AddCell(footerFirmas);
                }
                firmas.WriteSelectedRows(0, -1, 40, (doc.BottomMargin + 40), writer.DirectContent);

                PdfPTable footerTbl = new PdfPTable(2);
                footerTbl.TotalWidth = 550;
                footerTbl.SetWidths(anchoPP);
                for (int i = 0; i < 2; i++)
                {
                    Chunk myFooter;
                    if (i == 2 || i == 3)
                        myFooter = new Chunk(listaTablaPP[i], FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 20));
                    else
                        myFooter = new Chunk(listaTablaPP[i], FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 8));

                    PdfPCell footer = new PdfPCell(new Phrase(myFooter));
                    footer.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    footer.HorizontalAlignment = Element.ALIGN_CENTER;
                    footerTbl.AddCell(footer);
                }
                footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin + 10), writer.DirectContent);

                doc.Add(tabla1);
                doc.Add(tablaFechas);
                doc.Add(tablaNombre);
                doc.Add(tablaDept);
                doc.Add(tablaImporte);
                doc.Add(tablaOI);
                doc.Add(tablaProveedor);
                doc.Add(tablaOC);
                doc.Add(tablaCuerpo);
                doc.Add(tablaTotales);
                doc.Close();
                writer.Close();
                arc.abirArchivo(ruta);
                //Process.Start(ruta);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                writer.Close();
            }
        }


    }
}
