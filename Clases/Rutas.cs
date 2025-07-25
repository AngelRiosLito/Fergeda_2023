using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Clases
{
    class Rutas
    {
        public string OC(string OC) => @"\\192.168.0.231\Litopolis Publico\Fergeda\Compras\Orden de Compra\Orden de Compra " + OC + ".pdf";
        public string OCSimple(string OC)=> @"\\192.168.0.231\Litopolis Publico\Fergeda\Compras\Orden de Compra\Orden Compra " + OC + ".PDF";
        public string OCCancelada(string OC) => @"\\192.168.0.231\Litopolis Publico\Fergeda\Compras\Orden de Compra\Orden de Compra " + OC + " Cancelada.pdf";
        public string OCFirmada(string OC)=> @"\\192.168.0.231\Litopolis Publico\Fergeda\Compras\Ordenes Firmadas\"+OC+".pdf";

        public string requiCompras(int dat) => @"\\192.168.0.231\litopolis publico\Fergeda\Compras\Requisicion\Requisicion " + dat + ".pdf";
        public string requiComprasMArca(int dat) => @"\\192.168.0.231\litopolis publico\Fergeda\Compras\Requisicion\Requisicion de Compra " + dat + ".pdf";
        public string rutaimg(string dat) => @"\\192.168.0.231\litopolis publico\Fergeda\Compras\Requisicion\Imagenes\rq " + dat + ".png";
        public string rutaQRq(string dat) => @"\\192.168.0.231\litopolis publico\Fergeda\Compras\Requisicion\QR\rq " + dat + ".png";

        public string SolicitudesCheque(string fol) => @"\\192.168.0.231\Litopolis Publico\Fergeda\Finanzas\Solicitud de Cheque\Solicitud de Cheque "+ fol + ".pdf";
        public string solicitudCheque(string fol) => @"\\192.168.0.231\Litopolis Publico\Fergeda\Finanzas\Solicitud de Cheque\Solicitud de Cheque" + fol + ".pdf";
        public string solicitudCheque2(string fol) => @"\\192.168.0.231\Litopolis Publico\Fergeda\Finanzas\Solicitud de Cheque\Solicitud de Cheque " + fol + "";
        #region FINANZAS
        public string polizaCheque1(int numChe) => @"\\192.168.0.231\Litopolis Publico\Fergeda\Finanzas\Cheques\Poliza" + numChe + ".PDF";
        public string cheque(int num) => @"\\\192.168.0.231\Litopolis Publico\Fergeda\Finanzas\Cheques\cheque" + num + ".PDF";
        //public string saldos(string Banco, int fol) => @"\\192.168.0.231\Litopolis Publico\Fergeda\Finanzas\Cheques\Saldo Fergeda" + Banco + " " + DateTime.Now.ToString("yyyy-MM-dd") + " - " + fol + " .pdf";
        public string saldos(string Banco, int fol) => @"C:\Users\IvanG\Documents\Flujo en Bancos\Saldo Fergeda" + Banco + " " + DateTime.Now.ToString("yyyy-MM-dd") + " - " + fol + " .pdf";
        #endregion
        #region QR
        public string rutaQrimagen(int empleado) => @"\\192.168.0.231\litopolis publico\Fergeda\Vigilancia\QR\ImagenG" + empleado + ".png";
        #endregion

    }
}
