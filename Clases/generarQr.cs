using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static QRCoder.PayloadGenerator;

namespace Fergeda_2023.Clases
{
    class generarQr
    {
        private Rutas rt = new Rutas();
        private Transformacion tr = new Transformacion();
        QRCodeGenerator qrGenerado = new QRCodeGenerator();
        QRCodeData datos;

        private Url a;
        private SMS a1;
        WhatsAppMessage a2;
        PhoneNumber pon;
        Mail mail;
        Geolocation geo;
        int empleado;
        public void Generar(string n, PictureBox pic, int op, int empleado)
        {
            this.empleado = empleado;
            switch (op)
            {
                case 1:
                    mail = new Mail(n);
                    datos = qrGenerado.CreateQrCode(mail, QRCodeGenerator.ECCLevel.H);
                    generaQR(datos, pic);
                    break;
                case 3:
                    pon = new PhoneNumber("5510844343;" + n);
                    datos = qrGenerado.CreateQrCode(pon, QRCodeGenerator.ECCLevel.H);
                    generaQR(datos, pic);
                    break;
                case 4:
                    pon = new PhoneNumber(n);
                    datos = qrGenerado.CreateQrCode(pon, QRCodeGenerator.ECCLevel.H);
                    generaQR(datos, pic);
                    break;
                case 5:
                    a2 = new WhatsAppMessage("+52" + n, "Hola, Buen Dia.");
                    datos = qrGenerado.CreateQrCode(a2, QRCodeGenerator.ECCLevel.H);
                    generaQR(datos, pic);
                    break;
                case 6:
                    a1 = new SMS(n);
                    datos = qrGenerado.CreateQrCode(a1, QRCodeGenerator.ECCLevel.H);
                    generaQR(datos, pic);
                    break;
                case 7:
                    a = new Url(n);
                    datos = qrGenerado.CreateQrCode(a, QRCodeGenerator.ECCLevel.H);
                    generaQR(datos, pic);
                    break;
                case 8:
                    geo = new Geolocation("19.6533981", "-99.1980321");
                    string payload = geo.ToString();
                    datos = qrGenerado.CreateQrCode(payload, QRCodeGenerator.ECCLevel.H);
                    generaQR(datos, pic);
                    break;

                default:
                    break;
            }

        }

        private void generaQR(QRCodeData datos, PictureBox pic)
        {
            QRCode qrCodigo = new QRCode(datos);
            Bitmap image = qrCodigo.GetGraphic(200, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("lito.PNG"), 50, 50, true);
            image.Save(rt.rutaQrimagen(empleado));
            pic.Image = image;
        }

        //public void Genera2r(string n, int empleado, string tip, string folio)
        //{
        //    this.empleado = empleado;
        //    datos = qrGenerado.CreateQrCode(n, QRCodeGenerator.ECCLevel.H);
        //    generaQR2(datos, tip, folio);
        //}
        //private void generaQR2(QRCodeData datos, string tip, string folio)
        //{
        //    QRCode qrCodigo = new QRCode(datos);
        //    Bitmap image = qrCodigo.GetGraphic(15, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("lito.PNG"), 30, 30, false);
        //    image.Save(rt.rutaQrimagenActivo(empleado, tip, folio));
        //}



        #region GENERAR QR Materiales
        //public void GenQR(string n, int i, string rt)
        //{
        //    string cifrado = tr.Contraseña(n, tr.KeyProducto);
        //    Console.WriteLine(cifrado);
        //    QRCodeGenerator qrGenerado = new QRCodeGenerator();
        //    QRCodeData datos;
        //    datos = qrGenerado.CreateQrCode(cifrado, QRCodeGenerator.ECCLevel.M);
        //    QRCode qrCodigo = new QRCode(datos);
        //    Bitmap image = qrCodigo.GetGraphic(20, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("lito.png"), 50, 50, false);
        //    image.Save(rt);
        //}


        public void GenQRGeneral(string n, string rt)
        {

            QRCodeGenerator qrGenerado = new QRCodeGenerator();
            QRCodeData datos;
            datos = qrGenerado.CreateQrCode(n, QRCodeGenerator.ECCLevel.H);
            QRCode qrCodigo = new QRCode(datos);
            Bitmap image = qrCodigo.GetGraphic(80, Color.Turquoise, Color.White, (Bitmap)Bitmap.FromFile("lito.png"), 50, 50, true);
            image.Save(rt);
        }
        #endregion

        #region QR COMPRAS
        public void linkCompras(string n, string fol)
        {
            a = new Url(n);
            datos = qrGenerado.CreateQrCode(a, QRCodeGenerator.ECCLevel.H);
            QRCode qrCodigo = new QRCode(datos);
            Bitmap image = qrCodigo.GetGraphic(60, Color.Black, Color.White, (Bitmap)Bitmap.FromFile("lito.png"), 50, 50, false);
            image.Save(rt.rutaQRq(fol));

        }
        #endregion
    }
}
