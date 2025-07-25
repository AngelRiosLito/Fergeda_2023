using Fergeda_2023.Clases;
using Fergeda_2023.General;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Compras.PDF
{
    class requisicioncompra
    {
        private Rutas Rt = new Rutas();
        private Consultas X = new Consultas();
        private FileStream fs;
        private PdfWriter writer;
        private MensageC msg;
        private Document doc = new Document();
        private Archivos arc = new Archivos();
        List<string> datosGen = new List<string>();
        List<string> datosGenLlenado = new List<string>();
        List<string> LlenadoGenerales = new List<string>();
        List<string> LlenadoGeneralesLlenado = new List<string>();
        DateTime fechaProgramada;
        DateTime fechaRecepcion;
        DateTime fechaCierre;
        bool banderaOC;
        bool banderafechaCierre;
        string oc;
        string[] arr1;
        List<string> consultaImagenes = new List<string>();
        int urgencia = 0;

        private void llenadoDatos(int requisicion)
        {
            datosGen.Clear();
            datosGenLlenado.Clear();
            LlenadoGenerales.Clear();
            consultaImagenes.Clear();
            LlenadoGeneralesLlenado.Clear();

            datosGen = X.Litado("SELECT CONCAT(b.Nombre,'|',b.departamento,'|',IF(tipo_requi = 'Material',tipo_requi,(IF(tipo_requi = 'serCorr'," +
                "'Servicio correctivo',(IF(tipo_requi = 'SerPre','Servicio preventivo',(IF(tipo_requi = 'Otro','Otro', tipo_requi))))))),'|',CASE WHEN " +
                "a.tipo_sol = 0 THEN 'NORMAL' WHEN a.tipo_sol = 1 THEN 'REGULAR'   WHEN a.tipo_sol = 2 THEN 'URGENTE' WHEN a.tipo_sol = 3 " +
                "THEN 'SUPER URGENTE'     END,'|', a.fecha_programada, '|', a.fecha_generada, '|', IF(a.fecha_cierre IS NULL, 'N/A', a.fecha_cierre), " +
                " '|', a.motivoC, '|', (SELECT DISTINCT a.Nombre FROM fergeda.r_empleados a INNER JOIN fergeda.c_requisicion b ON a.idr_empleado = " +
                "fk_solicitaA WHERE b.idc_requisicion = '"+requisicion +"')) AS consulta FROM fergeda.c_requisicion a INNER JOIN fergeda.r_empleados b " +
                "ON a.fk_solicitante = b.idr_empleado WHERE idc_requisicion = '" + requisicion + "'; ");

            for (int i = 0; i < datosGen.Count; i++)
            {
                string[] arr = datosGen[i].Split('|');
                for (int j = 0; j < arr.Length; j++)
                    datosGenLlenado.Add(arr[j].ToUpper());
            }

            LlenadoGenerales = X.Litado("select concat(descripcion,'|',no_pza,'|',unidad,'|',parte_producto,'|',marca,'|',Obgetivo) FROM fergeda.c_requsiciondesgloce where fk_requi = " + requisicion + ";");

            for (int i = 0; i < LlenadoGenerales.Count; i++)
            {
                string[] arr = LlenadoGenerales[i].Split('|');
                for (int j = 0; j < arr.Length; j++)
                    LlenadoGeneralesLlenado.Add(arr[j]);
            }

            fechaProgramada = DateTime.Parse(datosGenLlenado[4].ToString());
            fechaRecepcion = DateTime.Parse(datosGenLlenado[5].ToString());

            if (datosGenLlenado[6].Equals("N/A"))
                datosGenLlenado[6] = "N/A";
            else
                fechaCierre = DateTime.Parse(datosGenLlenado[6].ToString());


            banderaOC = (Convert.ToInt32(X.elem3("select count(fk_oc) FROM fergeda.c_requsiciondesgloce where fk_requi = " + requisicion + ";")).Equals(0) ? false : true);
            banderafechaCierre = (Convert.ToInt32(X.elem3("select count(fecha_cierre) FROM fergeda.c_requisicion where idc_requisicion = " + requisicion + ";")).Equals(0) ? false : true);
            oc = (X.elem3("select group_concat(fk_oc separator ',') 'Consulta' FROM fergeda.c_requsiciondesgloce where fk_requi = " + requisicion + ";"));
            consultaImagenes = X.Litado("select concat(imagen,'|',link) FROM fergeda.c_requisicion where idc_requisicion= " + requisicion + ";");
            for (int i = 0; i < consultaImagenes.Count; i++)
                arr1 = consultaImagenes[i].Split('|');
            urgencia = (Convert.ToInt32(X.elem3("select tipo_sol FROM fergeda.c_requisicion where idc_requisicion = " + requisicion + ";")));
        }

        public void LlenadoPdf(int requisicion)
        {
            try
            {
                llenadoDatos(requisicion);
                msg = new MensageC();
                string ruta = Rt.requiCompras(requisicion);
                fs = new FileStream(ruta, FileMode.Create, FileAccess.Write, FileShare.None);
                doc = new Document(PageSize.LETTER, 30, 30, 30, 30);
                writer = PdfWriter.GetInstance(doc, fs);
                #region fuentes
                var fuenteRequisicion = FontFactory.GetFont("Aria Narrow", 9, new BaseColor(Color.Crimson));
                var fuente = FontFactory.GetFont("Aria Narrow", 9, new BaseColor(Color.Black));
                var fuenteReqFecha = FontFactory.GetFont("Aria Narrow", 15, new BaseColor(Color.Black));
                var fuenteReqTitulo = FontFactory.GetFont("Aria Narrow", 12, new BaseColor(Color.White));
                var fuenteSolicitanteArea = FontFactory.GetFont("Aria Narrow", 9, new BaseColor(Color.White));
                var fuenteVacGen = FontFactory.GetFont("Aria Narrow", 5, new BaseColor(Color.Black));
                #endregion

                #region anchos tablas
                int[] anchoNumReqFecha = { 400, 160 };
                int[] anchoTitReq = { 560 };
                int[] anchoSolArea = { 50, 220, 80, 30, 180 };
                int[] anchoSolicitudTipo = { 50, 150, 200, 40, 110 };
                int[] anchofechaprogrecep = { 95, 110, 160, 75, 120 };
                int[] anchoocFechaCierre = { 90, 125, 150, 75, 120 };
                int[] anchoMotivo = { 35, 525 };
                int[] anchoCampos = { 109, 70, 65, 85, 101, 130 };
                int[] anchoDatosAdicionales = { 80, 210, 60, 210 };
                int[] anchoFirmas = { 50, 205, 150, 205, 50 };
                int[] anchoFirmaSolicitante = { 177, 205, 177 };
                #endregion

                #region Listas
                List<string> primeraLista = new List<string> { "Descripción del producto", "No. Pza.", "Unidad", "No. Parte", "Marca", "Objetivo o requerimiento" };
                List<string> segundaLista = new List<string> { "Descripción del producto", "No. Pza.", "Unidad", "No. Parte", "Marca", "Objetivo o requerimiento" };
                List<string> listaFirmas = new List<string> { "Gerente del area", "Compras" };
                #endregion

                doc.AddTitle("LITOPOLIS");
                doc.AddAuthor("Angel");

                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(@"logo-fergeda.png");
                logo.SetAbsolutePosition(15, 720);
                logo.ScalePercent(50);
                writer.PageEvent = new HeaderFooter();
                doc.Open();

                #region Requisición y fecha
                PdfPTable tablaNumReqFecha = new PdfPTable(2);
                tablaNumReqFecha.TotalWidth = 560f;
                tablaNumReqFecha.LockedWidth = true;
                tablaNumReqFecha.HorizontalAlignment = 1;
                tablaNumReqFecha.SetWidths(anchoNumReqFecha);

                PdfPCell requisicionVacia = new PdfPCell(new Paragraph("" + (char)00, fuente));
                requisicionVacia.Colspan = 1;
                requisicionVacia.HorizontalAlignment = Element.ALIGN_LEFT;
                requisicionVacia.BorderWidthBottom = 0f;
                requisicionVacia.BorderWidthLeft = 0f;
                requisicionVacia.BorderWidthRight = 0f;
                requisicionVacia.BorderWidthTop = 0f;

                PdfPCell requisicionTit = new PdfPCell(new Paragraph("Requisición " + requisicion + (char)00, fuenteRequisicion));
                requisicionTit.Colspan = 1;
                requisicionTit.HorizontalAlignment = Element.ALIGN_LEFT;
                requisicionTit.BorderWidthBottom = 0f;
                requisicionTit.BorderWidthLeft = 0f;
                requisicionTit.BorderWidthRight = 0f;
                requisicionTit.BorderWidthTop = 0f;

                PdfPCell requisicionFecha = new PdfPCell(new Paragraph("Fecha: " + DateTime.Now.ToString("dd/MMM/yy HH:mm:ss").Replace(".", "") + (char)00, fuente));
                requisicionFecha.Colspan = 1;
                requisicionFecha.HorizontalAlignment = Element.ALIGN_LEFT;
                requisicionFecha.BorderWidthBottom = 0f;
                requisicionFecha.BorderWidthLeft = 0f;
                requisicionFecha.BorderWidthRight = 0f;
                requisicionFecha.BorderWidthTop = 0f;

                PdfPCell requisicionCodigo = new PdfPCell(new Paragraph("\n" + (char)00, fuente));/*codigo de papel*/
                requisicionCodigo.Colspan = 1;
                requisicionCodigo.HorizontalAlignment = Element.ALIGN_LEFT;
                requisicionCodigo.BorderWidthBottom = 0f;
                requisicionCodigo.BorderWidthLeft = 0f;
                requisicionCodigo.BorderWidthRight = 0f;
                requisicionCodigo.BorderWidthTop = 0f;

                PdfPCell requisicionEspaciado = new PdfPCell(new Paragraph(" " + (char)00, fuenteReqFecha));
                requisicionEspaciado.Colspan = 2;
                requisicionEspaciado.HorizontalAlignment = Element.ALIGN_LEFT;
                requisicionEspaciado.BorderWidthBottom = 0f;
                requisicionEspaciado.BorderWidthLeft = 0f;
                requisicionEspaciado.BorderWidthRight = 0f;
                requisicionEspaciado.BorderWidthTop = 0f;

                tablaNumReqFecha.AddCell(requisicionVacia);
                tablaNumReqFecha.AddCell(requisicionTit);
                tablaNumReqFecha.AddCell(requisicionVacia);
                tablaNumReqFecha.AddCell(requisicionFecha);
                tablaNumReqFecha.AddCell(requisicionCodigo);
                tablaNumReqFecha.AddCell(requisicionVacia);
                tablaNumReqFecha.AddCell(requisicionEspaciado);
                #endregion

                #region tituloRequisicion

                PdfPTable tablaTituloRequ = new PdfPTable(1);
                tablaTituloRequ.TotalWidth = 560f;
                tablaTituloRequ.LockedWidth = true;
                tablaTituloRequ.HorizontalAlignment = 1;
                tablaTituloRequ.SetWidths(anchoTitReq);

                PdfPCell Titrequisicion = new PdfPCell(new Paragraph("REQUISICIÓN DE MATERIALES Y/O SERVICIOS" + (char)00, fuenteReqTitulo));
                Titrequisicion.Colspan = 1;
                Titrequisicion.HorizontalAlignment = Element.ALIGN_CENTER;
                Titrequisicion.BorderWidthBottom = 0f;
                Titrequisicion.BorderWidthLeft = 0f;
                Titrequisicion.BorderWidthRight = 0f;
                Titrequisicion.BorderWidthTop = 0f;

                Titrequisicion.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));


                PdfPCell titEspaciado = new PdfPCell(new Paragraph(" " + (char)00, fuenteReqTitulo));
                titEspaciado.Colspan = 1;
                titEspaciado.HorizontalAlignment = Element.ALIGN_CENTER;
                titEspaciado.BorderWidthBottom = 0f;
                titEspaciado.BorderWidthLeft = 0f;
                titEspaciado.BorderWidthRight = 0f;
                titEspaciado.BorderWidthTop = 0f;

                tablaTituloRequ.AddCell(Titrequisicion);
                tablaTituloRequ.AddCell(titEspaciado);
                #endregion

                #region Solicitante / Área

                PdfPTable solArea = new PdfPTable(5);
                solArea.TotalWidth = 560f;
                solArea.LockedWidth = true;
                solArea.HorizontalAlignment = 1;
                solArea.SetWidths(anchoSolArea);

                PdfPCell solTitulo = new PdfPCell(new Paragraph("Solicitante: " + (char)00, fuenteSolicitanteArea));
                solTitulo.Colspan = 1;
                solTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                solTitulo.BorderWidthBottom = 0f;
                solTitulo.BorderWidthLeft = 0f;
                solTitulo.BorderWidthRight = 0f;
                solTitulo.BorderWidthTop = 0f;
                solTitulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell solLlena = new PdfPCell(new Paragraph("" + datosGenLlenado[0] + (char)00, fuente));
                solLlena.Colspan = 1;
                solLlena.HorizontalAlignment = Element.ALIGN_LEFT;
                solLlena.BorderWidthBottom = 1f;
                solLlena.BorderWidthLeft = 0f;
                solLlena.BorderWidthRight = 1f;
                solLlena.BorderWidthTop = 0f;
                solLlena.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell solVac = new PdfPCell(new Paragraph(" " + (char)00, fuente));
                solVac.Colspan = 1;
                solVac.HorizontalAlignment = Element.ALIGN_CENTER;
                solVac.BorderWidthBottom = 0f;
                solVac.BorderWidthLeft = 0f;
                solVac.BorderWidthRight = 0f;
                solVac.BorderWidthTop = 0f;

                PdfPCell areaTitulo = new PdfPCell(new Paragraph("Área: " + (char)00, fuenteSolicitanteArea));
                areaTitulo.Colspan = 1;
                areaTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                areaTitulo.BorderWidthBottom = 0f;
                areaTitulo.BorderWidthLeft = 0f;
                areaTitulo.BorderWidthRight = 0f;
                areaTitulo.BorderWidthTop = 0f;
                areaTitulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell areaLlena = new PdfPCell(new Paragraph("" + datosGenLlenado[1] + (char)00, fuente));
                areaLlena.Colspan = 1;
                areaLlena.HorizontalAlignment = Element.ALIGN_LEFT;
                areaLlena.BorderWidthBottom = 1f;
                areaLlena.BorderWidthLeft = 0f;
                areaLlena.BorderWidthRight = 1f;
                areaLlena.BorderWidthTop = 0f;
                areaLlena.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                solArea.AddCell(solTitulo);
                solArea.AddCell(solLlena);
                solArea.AddCell(solVac);
                solArea.AddCell(areaTitulo);
                solArea.AddCell(areaLlena);
                #endregion

                #region Solicitud Tipo

                PdfPTable solicitudTipo = new PdfPTable(5);
                solicitudTipo.TotalWidth = 560f;
                solicitudTipo.LockedWidth = true;
                solicitudTipo.HorizontalAlignment = 1;
                solicitudTipo.SetWidths(anchoSolicitudTipo);

                PdfPCell solicitudTitulo = new PdfPCell(new Paragraph("Solicitud: " + (char)00, fuenteSolicitanteArea));
                solicitudTitulo.Colspan = 1;
                solicitudTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                solicitudTitulo.BorderWidthBottom = 0f;
                solicitudTitulo.BorderWidthLeft = 0f;
                solicitudTitulo.BorderWidthRight = 0f;
                solicitudTitulo.BorderWidthTop = 0f;
                solicitudTitulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell solicitudLlena = new PdfPCell(new Paragraph("" + datosGenLlenado[3] + (char)00, fuente));
                solicitudLlena.Colspan = 1;
                solicitudLlena.HorizontalAlignment = Element.ALIGN_LEFT;
                solicitudLlena.BorderWidthBottom = 1f;
                solicitudLlena.BorderWidthLeft = 0f;
                solicitudLlena.BorderWidthRight = 1f;
                solicitudLlena.BorderWidthTop = 0f;
                solicitudLlena.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell tipoTitulo = new PdfPCell(new Paragraph("Tipo: " + (char)00, fuenteSolicitanteArea));
                tipoTitulo.Colspan = 1;
                tipoTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                tipoTitulo.BorderWidthBottom = 0f;
                tipoTitulo.BorderWidthLeft = 0f;
                tipoTitulo.BorderWidthRight = 0f;
                tipoTitulo.BorderWidthTop = 0f;
                tipoTitulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell tipoLlena = new PdfPCell(new Paragraph("" + datosGenLlenado[2] + (char)00, fuente));
                tipoLlena.Colspan = 1;
                tipoLlena.HorizontalAlignment = Element.ALIGN_LEFT;
                tipoLlena.BorderWidthBottom = 1f;
                tipoLlena.BorderWidthLeft = 0f;
                tipoLlena.BorderWidthRight = 1f;
                tipoLlena.BorderWidthTop = 0f;
                tipoLlena.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell espaciadoDatGen = new PdfPCell(new Paragraph(" " + (char)00, fuenteVacGen));
                espaciadoDatGen.Colspan = 5;
                espaciadoDatGen.HorizontalAlignment = Element.ALIGN_LEFT;
                espaciadoDatGen.BorderWidthBottom = 0f;
                espaciadoDatGen.BorderWidthLeft = 0f;
                espaciadoDatGen.BorderWidthRight = 0f;
                espaciadoDatGen.BorderWidthTop = 0f;

                solicitudTipo.AddCell(espaciadoDatGen);
                solicitudTipo.AddCell(solicitudTitulo);
                solicitudTipo.AddCell(solicitudLlena);
                solicitudTipo.AddCell(solVac);
                solicitudTipo.AddCell(tipoTitulo);
                solicitudTipo.AddCell(tipoLlena);
                solicitudTipo.AddCell(espaciadoDatGen);
                #endregion

                #region Fecha programada Fecha Recepción

                PdfPTable fechaProgRecep = new PdfPTable(5);
                fechaProgRecep.TotalWidth = 560f;
                fechaProgRecep.LockedWidth = true;
                fechaProgRecep.HorizontalAlignment = 1;
                fechaProgRecep.SetWidths(anchofechaprogrecep);

                PdfPCell fechaRecTitulo = new PdfPCell(new Paragraph("Fecha programada: " + (char)00, fuenteSolicitanteArea));
                fechaRecTitulo.Colspan = 1;
                fechaRecTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                fechaRecTitulo.BorderWidthBottom = 0f;
                fechaRecTitulo.BorderWidthLeft = 0f;
                fechaRecTitulo.BorderWidthRight = 0f;
                fechaRecTitulo.BorderWidthTop = 0f;
                fechaRecTitulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell fechaRecLlena = new PdfPCell(new Paragraph("" + fechaProgramada.ToString("dd/MMM/yy HH:mm:ss").Replace(".", "") + (char)00, fuente));
                fechaRecLlena.Colspan = 1;
                fechaRecLlena.HorizontalAlignment = Element.ALIGN_LEFT;
                fechaRecLlena.BorderWidthBottom = 1f;
                fechaRecLlena.BorderWidthLeft = 0f;
                fechaRecLlena.BorderWidthRight = 1f;
                fechaRecLlena.BorderWidthTop = 0f;
                fechaRecLlena.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                fechaProgRecep.AddCell(fechaRecTitulo);
                fechaProgRecep.AddCell(fechaRecLlena);
                fechaProgRecep.AddCell(solVac);
                fechaProgRecep.AddCell(solVac);
                fechaProgRecep.AddCell(solVac);
                fechaProgRecep.AddCell(espaciadoDatGen);
                #endregion

                #region ordenes compra, fecha cierre
                PdfPTable ocfechacierre = new PdfPTable(5);
                ocfechacierre.TotalWidth = 560f;
                ocfechacierre.LockedWidth = true;
                ocfechacierre.HorizontalAlignment = 1;
                ocfechacierre.SetWidths(anchoocFechaCierre);

                PdfPCell fechaCierreTit = new PdfPCell(new Paragraph("Fecha de cierre: " + (char)00, fuenteSolicitanteArea));
                fechaCierreTit.Colspan = 1;
                fechaCierreTit.HorizontalAlignment = Element.ALIGN_LEFT;
                fechaCierreTit.BorderWidthBottom = 0f;
                fechaCierreTit.BorderWidthLeft = 0f;
                fechaCierreTit.BorderWidthRight = 0f;
                fechaCierreTit.BorderWidthTop = 0f;
                fechaCierreTit.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell fechaCierreLlena = new PdfPCell(new Paragraph("" + fechaCierre.ToString("dd/MMM/yy HH:mm:ss").Replace(".", "").ToUpper() + (char)00, fuente));
                fechaCierreLlena.Colspan = 1;
                fechaCierreLlena.HorizontalAlignment = Element.ALIGN_LEFT;
                fechaCierreLlena.BorderWidthBottom = 1f;
                fechaCierreLlena.BorderWidthLeft = 0f;
                fechaCierreLlena.BorderWidthRight = 1f;
                fechaCierreLlena.BorderWidthTop = 0f;
                fechaCierreLlena.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell ordenCTitiulo = new PdfPCell(new Paragraph("Ordenes de compra: " + (char)00, fuenteSolicitanteArea));
                ordenCTitiulo.Colspan = 1;
                ordenCTitiulo.HorizontalAlignment = Element.ALIGN_LEFT;
                ordenCTitiulo.BorderWidthBottom = 0f;
                ordenCTitiulo.BorderWidthLeft = 0f;
                ordenCTitiulo.BorderWidthRight = 0f;
                ordenCTitiulo.BorderWidthTop = 0f;
                ordenCTitiulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell ordenCLlena = new PdfPCell(new Paragraph("" + oc + (char)00, fuente));
                ordenCLlena.Colspan = 1;
                ordenCLlena.HorizontalAlignment = Element.ALIGN_LEFT;
                ordenCLlena.BorderWidthBottom = 1f;
                ordenCLlena.BorderWidthLeft = 0f;
                ordenCLlena.BorderWidthRight = 1f;
                ordenCLlena.BorderWidthTop = 0f;
                ordenCLlena.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                if (banderafechaCierre.Equals(true) && banderaOC.Equals(true))
                {
                    ocfechacierre.AddCell(ordenCTitiulo);
                    ocfechacierre.AddCell(ordenCLlena);
                    ocfechacierre.AddCell(solVac);
                    ocfechacierre.AddCell(fechaCierreTit);
                    ocfechacierre.AddCell(fechaCierreLlena);
                    ocfechacierre.AddCell(espaciadoDatGen);
                }

                else if (banderaOC.Equals(true) && banderafechaCierre.Equals(false))
                {
                    ocfechacierre.AddCell(ordenCTitiulo);
                    ocfechacierre.AddCell(ordenCLlena);
                    ocfechacierre.AddCell(solVac);
                    ocfechacierre.AddCell(solVac);
                    ocfechacierre.AddCell(solVac);
                    ocfechacierre.AddCell(espaciadoDatGen);
                }
                else if (banderafechaCierre.Equals(true) && banderaOC.Equals(false))
                {
                    ocfechacierre.AddCell(fechaCierreTit);
                    ocfechacierre.AddCell(fechaCierreLlena);
                    ocfechacierre.AddCell(solVac);
                    ocfechacierre.AddCell(solVac);
                    ocfechacierre.AddCell(solVac);
                    ocfechacierre.AddCell(espaciadoDatGen);
                }
                #endregion

                #region motivo
                PdfPTable motivo = new PdfPTable(2);
                motivo.TotalWidth = 560f;
                motivo.LockedWidth = true;
                motivo.HorizontalAlignment = 1;
                motivo.SetWidths(anchoMotivo);

                PdfPCell motivoTitulo = new PdfPCell(new Paragraph("Motivo: " + (char)00, fuenteSolicitanteArea));
                motivoTitulo.Colspan = 1;
                motivoTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                motivoTitulo.BorderWidthBottom = 0f;
                motivoTitulo.BorderWidthLeft = 0f;
                motivoTitulo.BorderWidthRight = 0f;
                motivoTitulo.BorderWidthTop = 0f;
                motivoTitulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell motivoLleno = new PdfPCell(new Paragraph("" + datosGenLlenado[7] + (char)00, fuente));
                motivoLleno.Colspan = 1;
                motivoLleno.HorizontalAlignment = Element.ALIGN_LEFT;
                motivoLleno.BorderWidthBottom = 1f;
                motivoLleno.BorderWidthLeft = 0f;
                motivoLleno.BorderWidthRight = 1f;
                motivoLleno.BorderWidthTop = 0f;
                motivoLleno.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                motivo.AddCell(motivoTitulo);
                motivo.AddCell(motivoLleno);
                motivo.AddCell(requisicionEspaciado);
                #endregion

                #region encabezados 
                PdfPTable enc = new PdfPTable(6);
                enc.TotalWidth = 560f;
                enc.LockedWidth = true;
                enc.HorizontalAlignment = 1;
                enc.SetWidths(anchoCampos);

                for (int i = 0; i < primeraLista.Count; i++)
                {
                    PdfPCell encabezadoTit = new PdfPCell(new Paragraph(primeraLista[i] + (char)00, fuenteSolicitanteArea));
                    encabezadoTit.Colspan = 1;
                    encabezadoTit.HorizontalAlignment = Element.ALIGN_CENTER;
                    encabezadoTit.BorderWidthBottom = 0f;
                    encabezadoTit.BorderWidthLeft = 0f;
                    encabezadoTit.BorderWidthRight = 0f;
                    encabezadoTit.BorderWidthTop = 0f;
                    encabezadoTit.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                    encabezadoTit.BorderColor = new BaseColor(Color.White);
                    enc.AddCell(encabezadoTit);
                }
                for (int i = 0; i < LlenadoGeneralesLlenado.Count; i++)
                {
                    PdfPCell encabezadollenado = new PdfPCell(new Paragraph("" + LlenadoGeneralesLlenado[i] + (char)00, fuente));
                    encabezadollenado.Colspan = 1;
                    encabezadollenado.HorizontalAlignment = Element.ALIGN_CENTER;
                    encabezadollenado.BorderWidthBottom = 1f;
                    encabezadollenado.BorderWidthLeft = 1f;
                    encabezadollenado.BorderWidthRight = 1f;
                    encabezadollenado.BorderWidthTop = 1f;
                    encabezadollenado.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));
                    enc.AddCell(encabezadollenado);
                }
                #endregion

                #region Datos adicionales

                PdfPTable datosAdicionales = new PdfPTable(4);
                datosAdicionales.TotalWidth = 560f;
                datosAdicionales.LockedWidth = true;
                datosAdicionales.HorizontalAlignment = 1;
                datosAdicionales.SetWidths(anchoDatosAdicionales);

                PdfPCell datosAdicionalesEspaciado = new PdfPCell(new Paragraph(" " + (char)00, fuenteReqFecha));
                datosAdicionalesEspaciado.Colspan = 4;
                datosAdicionalesEspaciado.HorizontalAlignment = Element.ALIGN_LEFT;
                datosAdicionalesEspaciado.BorderWidthBottom = 0f;
                datosAdicionalesEspaciado.BorderWidthLeft = 0f;
                datosAdicionalesEspaciado.BorderWidthRight = 0f;
                datosAdicionalesEspaciado.BorderWidthTop = 0f;

                PdfPCell datosAdTitulo = new PdfPCell(new Paragraph("Datos adicionales: " + (char)00, fuenteSolicitanteArea));
                datosAdTitulo.Colspan = 1;
                datosAdTitulo.HorizontalAlignment = Element.ALIGN_LEFT;
                datosAdTitulo.BorderWidthBottom = 0f;
                datosAdTitulo.BorderWidthLeft = 0f;
                datosAdTitulo.BorderWidthRight = 0f;
                datosAdTitulo.BorderWidthTop = 0f;
                datosAdTitulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                PdfPCell datosAdVacio = new PdfPCell(new Paragraph(" " + (char)00, fuente));
                datosAdVacio.Colspan = 1;
                datosAdVacio.HorizontalAlignment = Element.ALIGN_LEFT;
                datosAdVacio.BorderWidthBottom = 0f;
                datosAdVacio.BorderWidthLeft = 0f;
                datosAdVacio.BorderWidthRight = 0f;
                datosAdVacio.BorderWidthTop = 0f;

                PdfPCell imagenTitulo = new PdfPCell(new Paragraph("Imagen: " + (char)00, fuenteSolicitanteArea));
                imagenTitulo.Colspan = 1;
                imagenTitulo.HorizontalAlignment = Element.ALIGN_CENTER;
                imagenTitulo.VerticalAlignment = Element.ALIGN_MIDDLE;
                imagenTitulo.BorderWidthBottom = 0f;
                imagenTitulo.BorderWidthLeft = 0f;
                imagenTitulo.BorderWidthRight = 0f;
                imagenTitulo.BorderWidthTop = 0f;
                imagenTitulo.BackgroundColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                if (arr1[0].Equals("Si") && !arr1[1].Equals("-"))
                {
                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"\\192.168.0.231\Litopolis Publico\Litopolis\Compras\Requisicion\Imagenes\rq " + requisicion + ".png");
                    imagen.ScaleToFit(100f, 100f);

                    iTextSharp.text.Image imagenQR = iTextSharp.text.Image.GetInstance(@"\\192.168.0.231\Litopolis Publico\Litopolis\Compras\Requisicion\QR\rq " + requisicion + ".png");
                    imagenQR.ScaleToFit(100f, 100f);

                    PdfPCell imagenUno = new PdfPCell(imagen);
                    imagenUno.Colspan = 1;
                    imagenUno.HorizontalAlignment = Element.ALIGN_CENTER;
                    imagenUno.BorderWidthBottom = 0f;
                    imagenUno.BorderWidthLeft = 0f;
                    imagenUno.BorderWidthRight = 0f;
                    imagenUno.BorderWidthTop = 0f;
                    imagenUno.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                    PdfPCell imagenDos = new PdfPCell(imagenQR);
                    imagenDos.Colspan = 1;
                    imagenDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    imagenDos.BorderWidthBottom = 0f;
                    imagenDos.BorderWidthLeft = 0f;
                    imagenDos.BorderWidthRight = 0f;
                    imagenDos.BorderWidthTop = 0f;
                    imagenDos.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                    datosAdicionales.AddCell(datosAdicionalesEspaciado);
                    datosAdicionales.AddCell(datosAdTitulo);
                    for (int i = 0; i < 7; i++)
                        datosAdicionales.AddCell(datosAdVacio);

                    datosAdicionales.AddCell(imagenTitulo);
                    datosAdicionales.AddCell(imagenUno);
                    datosAdicionales.AddCell(datosAdVacio);
                    datosAdicionales.AddCell(imagenDos);
                    for (int i = 0; i < 6; i++)
                        datosAdicionales.AddCell(datosAdicionalesEspaciado);
                }
                else if (arr1[0].Equals("No") && !arr1[1].Equals("-"))
                {
                    //iTextSharp.text.Image imagenQR = iTextSharp.text.Image.GetInstance(@"\\192.168.0.231\Litopolis Publico\Litopolis\Compras\Requisicion\QR\rq " + requisicion + ".png");
                    iTextSharp.text.Image imagenQR = iTextSharp.text.Image.GetInstance(Rt.rutaQRq(requisicion + ""));
                    imagenQR.ScaleToFit(100f, 100f);


                    PdfPCell imagenDos = new PdfPCell(imagenQR);
                    imagenDos.Colspan = 1;
                    imagenDos.HorizontalAlignment = Element.ALIGN_CENTER;
                    imagenDos.BorderWidthBottom = 0f;
                    imagenDos.BorderWidthLeft = 0f;
                    imagenDos.BorderWidthRight = 0f;
                    imagenDos.BorderWidthTop = 0f;
                    imagenDos.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));


                    datosAdicionales.AddCell(datosAdicionalesEspaciado);
                    datosAdicionales.AddCell(datosAdTitulo);
                    for (int i = 0; i < 7; i++)
                        datosAdicionales.AddCell(datosAdVacio);
                    datosAdicionales.AddCell(imagenTitulo);
                    datosAdicionales.AddCell(imagenDos);
                    datosAdicionales.AddCell(datosAdVacio);
                    datosAdicionales.AddCell(datosAdVacio);
                    for (int i = 0; i < 6; i++)
                        datosAdicionales.AddCell(datosAdicionalesEspaciado);
                }
                else if (arr1[0].Equals("Si") && arr1[1].Equals("-"))
                {
                    //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(@"\\192.168.0.231\Litopolis Publico\Litopolis\Compras\Requisicion\Imagenes\rq " + requisicion + ".png");
                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(Rt.rutaimg(requisicion + ""));
                    imagen.ScaleToFit(100f, 100f);

                    PdfPCell imagenUno = new PdfPCell(imagen);
                    imagenUno.Colspan = 1;
                    imagenUno.HorizontalAlignment = Element.ALIGN_CENTER;
                    imagenUno.BorderWidthBottom = 0f;
                    imagenUno.BorderWidthLeft = 0f;
                    imagenUno.BorderWidthRight = 0f;
                    imagenUno.BorderWidthTop = 0f;
                    imagenUno.BorderColor = new BaseColor(urgencia.Equals(0) ? System.Drawing.Color.FromArgb(0, 179, 108) :
                    (urgencia.Equals(1) ? System.Drawing.Color.FromArgb(242, 159, 5) : (urgencia.Equals(2) ? System.Drawing.Color.FromArgb(255, 77, 88) :
                    (urgencia.Equals(3) ? System.Drawing.Color.FromArgb(128, 0, 8) : System.Drawing.Color.FromArgb(20, 133, 204)))));

                    datosAdicionales.AddCell(datosAdicionalesEspaciado);
                    datosAdicionales.AddCell(datosAdTitulo);
                    for (int i = 0; i < 7; i++)
                        datosAdicionales.AddCell(datosAdVacio);
                    datosAdicionales.AddCell(imagenTitulo);
                    datosAdicionales.AddCell(imagenUno);
                    datosAdicionales.AddCell(datosAdVacio);
                    datosAdicionales.AddCell(datosAdVacio);
                    for (int i = 0; i < 6; i++)
                        datosAdicionales.AddCell(datosAdicionalesEspaciado);
                }
                else
                {
                    for (int i = 0; i < 6; i++)
                        datosAdicionales.AddCell(datosAdicionalesEspaciado);
                }
                #endregion

                #region Firmas

                PdfPTable firmaSolicitante = new PdfPTable(3);
                firmaSolicitante.TotalWidth = 560f;
                firmaSolicitante.LockedWidth = true;
                firmaSolicitante.HorizontalAlignment = 1;
                firmaSolicitante.SetWidths(anchoFirmaSolicitante);

                PdfPTable firmas = new PdfPTable(5);
                firmas.TotalWidth = 560f;
                firmas.LockedWidth = true;
                firmas.HorizontalAlignment = 1;
                firmas.SetWidths(anchoFirmas);

                PdfPCell firmaGerenteArea = new PdfPCell(new Paragraph(("Gerente del Área\n" + datosGenLlenado[1]).ToUpper() + (char)00, fuente));
                firmaGerenteArea.Colspan = 1;
                firmaGerenteArea.HorizontalAlignment = Element.ALIGN_CENTER;
                firmaGerenteArea.BorderWidthBottom = 0f;
                firmaGerenteArea.BorderWidthLeft = 0f;
                firmaGerenteArea.BorderWidthRight = 0f;
                firmaGerenteArea.BorderWidthTop = 1f;

                PdfPCell firmaCompras = new PdfPCell(new Paragraph("" + datosGenLlenado[8] + (char)00, fuente));
                firmaCompras.Colspan = 1;
                firmaCompras.HorizontalAlignment = Element.ALIGN_CENTER;
                firmaCompras.BorderWidthBottom = 0f;
                firmaCompras.BorderWidthLeft = 0f;
                firmaCompras.BorderWidthRight = 0f;
                firmaCompras.BorderWidthTop = 1f;

                PdfPCell firmaSolicitanteLlenado = new PdfPCell(new Paragraph(("Firma del solicitante" + (char)00).ToUpper(), fuente));
                firmaSolicitanteLlenado.Colspan = 1;
                firmaSolicitanteLlenado.HorizontalAlignment = Element.ALIGN_CENTER;
                firmaSolicitanteLlenado.BorderWidthBottom = 0f;
                firmaSolicitanteLlenado.BorderWidthLeft = 0f;
                firmaSolicitanteLlenado.BorderWidthRight = 0f;
                firmaSolicitanteLlenado.BorderWidthTop = 1f;

                firmaSolicitante.AddCell(solVac);
                firmaSolicitante.AddCell(firmaSolicitanteLlenado);
                firmaSolicitante.AddCell(solVac);
                for (int i = 0; i < 12; i++)
                    firmaSolicitante.AddCell(solVac);

                firmas.AddCell(solVac);
                firmas.AddCell(firmaGerenteArea);
                firmas.AddCell(solVac);
                firmas.AddCell(firmaCompras);
                firmas.AddCell(solVac);
                #endregion

                #region Agregados al documento
                doc.Add(logo);
                doc.Add(tablaNumReqFecha);
                doc.Add(tablaTituloRequ);
                doc.Add(solArea);
                doc.Add(solicitudTipo);
                doc.Add(fechaProgRecep);
                doc.Add(ocfechacierre);
                doc.Add(motivo);
                doc.Add(enc);
                doc.Add(datosAdicionales);
                doc.Add(firmaSolicitante);
                doc.Add(firmas);
                doc.Close();
                writer.Close();
                #endregion


                new MarcasAgua().marcaAguaDiagonalCarta(ruta, Rt.requiComprasMArca(requisicion), " Requisición de Compra " + requisicion);
                arc.abirArchivo(Rt.requiComprasMArca(requisicion));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine(ex);
                doc.Close();
                writer.Close();

            }
        }
        class HeaderFooter : PdfPageEventHelper
        {
            public override void OnEndPage(PdfWriter writer, Document document)
            {
                var fuentePP = FontFactory.GetFont("Aria Narrow", 9, new BaseColor(Color.Black));

                PdfPTable piePagina = new PdfPTable(1);
                piePagina.TotalWidth = 560f;
                piePagina.LockedWidth = true;
                piePagina.HorizontalAlignment = 1;

                PdfPCell piePaginaLleno = new PdfPCell(new Paragraph("Toda requisición de compra debe cumplir con todos los datos \ncorrespondientes, de no ser asi, será rechazada en el área de compras. " + (char)00, fuentePP));
                piePaginaLleno.Colspan = 1;
                piePaginaLleno.HorizontalAlignment = Element.ALIGN_CENTER;
                piePaginaLleno.BorderWidthBottom = 0f;
                piePaginaLleno.BorderWidthLeft = 0f;
                piePaginaLleno.BorderWidthRight = 0f;
                piePaginaLleno.BorderWidthTop = 0f;
                piePagina.AddCell(piePaginaLleno);

                piePagina.WriteSelectedRows(0, 1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) + 10, writer.DirectContent);
            }
        }



    }
}
