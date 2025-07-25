using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Clases
{
    class MarcasAgua
    {
        public void SelloCanceladoTexto(string ruta, string status)
        {
            Console.WriteLine(ruta);
            string watermarkedFile = "" + ruta + " " + status + ".pdf"; /*archivo que se lee + nombre.extencion*//*@"\\192.168.0.4\Users\Public\Documents\Ordenes Compra\Litopolis\Orden de Compra " + Convert.ToInt32(this.textBox1.Text) + " Cancelada .pdf";*/
            PdfReader reader1 = new PdfReader(ruta + ".pdf");
            using (FileStream fs2 = new FileStream(watermarkedFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (PdfStamper stamper = new PdfStamper(reader1, fs2))
            {
                int pageCount = reader1.NumberOfPages;

                PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                for (int i = 1; i <= pageCount; i++)
                {

                    iTextSharp.text.Rectangle rect = reader1.GetPageSize(i);
                    PdfContentByte cb = stamper.GetUnderContent(i);
                    cb.BeginLayer(layer);
                    cb.SetFontAndSize(BaseFont.CreateFont(
                    BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 50);
                    PdfGState gState = new PdfGState();
                    gState.FillOpacity = 0.35f;//25
                    cb.SetGState(gState);
                    cb.SetColorFill(BaseColor.BLACK);
                    cb.BeginText();
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "\t\' " + status + " \'\t", rect.Rotate().Width / 2, rect.Rotate().Height / 2, 20f); //PdfContentByte.ALIGN_CENTER, " CANCELADA", rect.Width / 2, rect.Height / 2, 45f
                    cb.EndText();
                    cb.EndLayer();
                }
            }
            reader1.Close();
            File.Delete(ruta + ".pdf");

        }
        public void marcaAguaDiagonal(string archivoOrigen, string archivoSalida, string texto, int i1)
        {

            float ancho = 0, rotacion = 0;
            PdfReader reader1 = new PdfReader(archivoOrigen);
            using (FileStream fs2 = new FileStream(archivoSalida, FileMode.Create, FileAccess.Write, FileShare.None))
            using (PdfStamper stamper = new PdfStamper(reader1, fs2))
            {
                int pageCount = reader1.NumberOfPages;


                PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                for (int i = 1; i <= pageCount; i++)
                {

                    iTextSharp.text.Rectangle rect = reader1.GetPageSize(i);
                    PdfContentByte cb = stamper.GetUnderContent(i);
                    cb.BeginLayer(layer);
                    cb.SetFontAndSize(BaseFont.CreateFont(
                    BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 50);
                    PdfGState gState = new PdfGState();
                    gState.FillOpacity = 0.35f;//25
                    cb.SetGState(gState);
                    cb.SetColorFill(BaseColor.BLACK);
                    cb.BeginText();
                    if (i1.Equals(1))
                    {
                        ancho = rect.Height / 2;
                        rotacion = 45f;
                    }
                    else
                    {
                        ancho = 100F;
                        rotacion = 0f;
                    }


                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, texto, rect.Width / 2, ancho, rotacion);

                    cb.EndText();
                    cb.EndLayer();
                }

            }
            reader1.Close();
            File.Delete(archivoOrigen);
        }

        public void SelloCancelado(string ruta, string status, int empleado)
        {
            string watermarkedFile = "" + ruta + " " + status + ".pdf"; /*archivo que se lee + nombre.extencion*//*@"\\192.168.0.4\Users\Public\Documents\Ordenes Compra\Litopolis\Orden de Compra " + Convert.ToInt32(this.textBox1.Text) + " Cancelada .pdf";*/
            PdfReader reader1 = new PdfReader(ruta + ".pdf");
            using (FileStream fs2 = new FileStream(watermarkedFile, FileMode.Create, FileAccess.Write, FileShare.None))
            using (PdfStamper stamper = new PdfStamper(reader1, fs2))
            {
                int pageCount = reader1.NumberOfPages;

                PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                for (int i = 1; i <= pageCount; i++)
                {

                    iTextSharp.text.Rectangle rect = reader1.GetPageSize(i);
                    PdfContentByte cb = stamper.GetUnderContent(i);
                    cb.BeginLayer(layer);
                    cb.SetFontAndSize(BaseFont.CreateFont(
                    BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 50);
                    PdfGState gState = new PdfGState();
                    gState.FillOpacity = 0.35f;//25
                    cb.SetGState(gState);
                    cb.SetColorFill(BaseColor.BLACK);
                    cb.BeginText();
                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "\t\' " + status + " \'\t" + empleado, rect.Width / 2, 100F, 0f); //PdfContentByte.ALIGN_CENTER, " CANCELADA", rect.Width / 2, rect.Height / 2, 45f
                    cb.EndText();
                    cb.EndLayer();
                }
            }
            reader1.Close();
            File.Delete(ruta + ".pdf");

        }
        public void marcaAguaDiagonalCarta(string archivoOrigen, string archivoSalida, string texto)
        {

            PdfReader reader1 = new PdfReader(archivoOrigen);
            using (FileStream fs2 = new FileStream(archivoSalida, FileMode.Create, FileAccess.Write, FileShare.None))
            using (PdfStamper stamper = new PdfStamper(reader1, fs2))
            {
                int pageCount = reader1.NumberOfPages;

                PdfLayer layer = new PdfLayer("WatermarkLayer", stamper.Writer);
                for (int i = 1; i <= pageCount; i++)
                {

                    iTextSharp.text.Rectangle rect = reader1.GetPageSize(i);
                    PdfContentByte cb = stamper.GetUnderContent(i);
                    cb.BeginLayer(layer);
                    cb.SetFontAndSize(BaseFont.CreateFont(
                    BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 50);
                    PdfGState gState = new PdfGState();
                    gState.FillOpacity = 0.35f;//25
                    cb.SetGState(gState);
                    cb.SetColorFill(BaseColor.GRAY);
                    cb.BeginText();


                    cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, texto, rect.Width / 2, rect.Height / 2, 45);

                    cb.EndText();
                    cb.EndLayer();
                }

            }
            reader1.Close();
            File.Delete(archivoOrigen);
        }

    }
}
