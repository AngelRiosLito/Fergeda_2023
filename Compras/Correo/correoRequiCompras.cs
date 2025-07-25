using Fergeda_2023.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fergeda_2023.Compras
{
    class correoRequiCompras
    {

        private Consultas X = new Consultas();
        private FormatoFechas fech = new FormatoFechas();
        string fecha, hora, Titulo, atte, urlcuerpo, cuerpo;
        public void datos(string folio)
        {
            char[] sp = { '|' };
            string datos = X.elem3("SELECT concat_ws('|',DATE(fecha_generada),TIME(fecha_generada),IF(tipo_sol = 0,'Normal',if (tipo_sol = 1,'Regular'," +
                "if (tipo_sol = 3,'Urgente','Super Urgente'))),IF(tipo_requi = 'serPre', 'Servicio Preventivo',if (tipo_requi = 'serCorr','Servicio Correctivo'," +
                "tipo_requi )),fk_solicitante) FROM fergeda.c_requisicion WHERE idc_requisicion = '" + folio + "';");
            string[] arr = datos.Split(sp);
            Titulo = "Requisición de Compra "+folio;
            fecha = fech.formatoFechaLarga(DateTime.Parse(arr[0]).ToString("yyyy/MM/dd"));
            hora = arr[1];
            atte = "";
            urlcuerpo = "https://i.postimg.cc/brp8Btjs/logolito.jpg";

            string cuerpoS = "";
            List<string> llenado = new List<string>();
            llenado.Clear();
            llenado = X.Litado("SELECT concat('<li>',no_pza,' ',unidad,' - ',descripcion,'</li>') FROM fergeda.c_requsiciondesgloce where fk_requi='" + folio + "';");
            for (int i = 0; i < llenado.Count; i++)
                cuerpoS += llenado[i];

            cuerpo = "Se solicita una compra de " + arr[3] + " con una urgencia: " + arr[2] + "<br> La Solicitud es la siguiente:</br>" +
            "<ul>" +
               " " + cuerpoS + "" +
               "</ul>" +
               "" +
               "";

            atte = X.elem3("SELECT Nombre FROM fergeda.r_empleados where idr_empleado='" + arr[4] + "';");

        }

        public string envio(string folio)
        {
            datos(folio);
            string html = "<!DOCTYPE html>" +
                "<html >" +
                "<head >" +
                "<title >Requisición de Compra Chalco</title>" +
                "<meta http - equiv = " + (char)34 + "Content - Type" + (char)34 + " content = " + (char)34 + "text / html; charset = utf - 8" + (char)34 + "/>" +
                "<meta content = " + (char)34 + "width = device - width, minimal - ui, initial - scale = 1.0, maximum - scale = 1.0, user - scalable = 0; " + (char)34 + " name = " + (char)34 + "viewport" + (char)34 + "/>" +
                "<meta name = " + (char)34 + "color - scheme" + (char)34 + " content = " + (char)34 + "light dark" + (char)34 + "/>" +
                "<meta name = " + (char)34 + "supported - color - schemes" + (char)34 + " content = " + (char)34 + "light dark" + (char)34 + "/>" +
                "<meta content = " + (char)34 + "telephone = no" + (char)34 + " name = " + (char)34 + "format - detection" + (char)34 + "/>" +
                "<style>" +
                    "@font - face {" +
                    "font - family: 'AmazonEmber-Light';" +
                    "src: url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Light.woff2') format('woff2'), url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Light.woff2') format('woff');" +
                    "font - weight: normal;" +
                    "font - style: normal;" +
                    "}" +
                    "@font - face {" +
                    "font - family: 'AmazonEmber';" +
                    "src: url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Regular.woff2') format('woff2'), url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Regular.woff2') format('woff');" +
                    "font - weight: normal;" +
                    "font - style: normal;" +
                    "}" +
                    "@font - face {" +
                    "font - family: 'AmazonEmber-Bold';" +
                    "src: url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Bold.woff2') format('woff2'), url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Bold.woff2') format('woff');" +
                    "font - weight: bold;" +
                    "font - style: normal;" +
                    "}" +
                    "@font - face {" +
                    "font - family: 'AmazonEmber-Heavy';" +
                    "src: url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Heavy.woff2') format('woff2'), url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Heavy.woff') format('woff'), url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Heavy.eot') format('eot'), url('https://pages.awscloud.com/rs/112-TZM-766/images/AmazonEmber-Heavy.ttf') format('ttf');" +
                    "font - weight: normal;" +
                    "font - style: normal;" +
                    "}" +
              "</style>" +
              "<style type = " + (char)34 + "text / css" + (char)34 + ">" +
                    ".font - family - decor {" +
                    "font - family: 'AmazonEmber';" +
                    "}" +
                    ":root {" +
                    "color - scheme: light dark;" +
                    "supported - color - schemes: light dark;" +
                    "}" +
                    "strong{font - weight: normal !important;" +
                    "font - family: AmazonEmber - Heavy,Arial, Helvetica, sans - serif !important;}" +
                    "b{font - weight: normal !important;font - family: AmazonEmber - Heavy,Arial, Helvetica, sans - serif !important;}" +
                    "# outlook a {        padding: 0;        }" +
                    ".ReadMsgBody {" +
                    "width: 100%;  }" +
                    ".ExternalClass {        width: 100%;    }" +
                    ".ExternalClass,    .ExternalClass p," +
                    ".ExternalClass span," +
                    ".ExternalClass font," +
                    ".ExternalClass td," +
                    ".ExternalClass div" +
                    "{line-height: 100%;}" +
                    "@-ms-viewport {" +
                    "width: device-width;" +
                    "}" +
              "</style> " +
              "<style type = " + (char)34 + "text/css" + (char)34 + ">" +
                    "body {" +
                    "font-family: 'AmazonEmber', Helvetica, sans-serif;" +
                    "margin: 0;" +
                    "padding: 0;" +
                    "-webkit-font-smoothing: antialiased !important;" +
                    "-webkit-text-size-adjust: none !important;" +
                    "width: 100% !important;" +
                    "height: 100% !important;" +
                    "}" +
                    "a[x - apple - data - detectors] {" +
                    "color: inherit !important;" +
                    "text-decoration: none !important;" +
                    "font-size: inherit !important;" +
                    "font-family: inherit !important;" +
                    "font-weight: inherit !important;" +
                    "line-height: inherit !important;" +
                    "}" +
                    "a {" +
                    "outline: 0;" +
                    "text-decoration: none;" +
                    "}" +
                    "table {" +
                    "border-spacing: 0;" +
                    "mso-table-lspace: 0pt;" +
                    "mso-table-rspace: 0pt;" +
                    "}" +
                    "body,table tr, table td,a, span,table.MsoNormalTable,th  {" +
                    "font-family: 'AmazonEmber', Arial, Helvetica, sans-serif;    }" +
                    "th {" +
                    "text-align: left;" +
                    "font-weight: normal;" +
                    "}" +
                    "#Social-Media td{color:#007EB9 !important;}" +
               "</style> " +
               "<style type = " + (char)34 + "text/css" + (char)34 + ">" +
               "    @media only screen and(max-width: 600px)" +
               "{" +
               ".deviceWidth {" +
               "width: 100 % !important;" +
               "min - width: 100 % !important;" +
               "float: none !important;" +
               "}" +
               ".responsive - table, .responsive - table table{" +
               "max - width:100 % !important;" +
               "width: 100 % !important;" +
               "} " +
               ".deviceWidth1 {" +
               "width: 90 % !important;" +
               "margin: 0 auto !important;" +
               "}        " +
               ".full - width img {" +
               "width: 100 % !important;" +
               "height: auto !important;" +
               "}" +
               ".noBgImage {" +
               "background - image: none !important;" +
               "}" +
               ".floatNone {" +
               "float: none !important;" +
               "width: 100 % !important;" +
               "}" +
               ".block {" +
               "display: block !important;" +
               "}" +
               ".center {" +
               "margin: 0 auto !important;" +
               "text - align: center !important;" +
               "}" +
               ".none{" +
               "display: none !important;" +
               "}" +
               ".agendaWidth{" +
               "width: 74px !important;" +
               "padding: 5px 10px !important;" +
               "}" +
               ".agendaPad{" +
               "padding: 5px 10px !important;" +
               "}       .widthAuto{" +
               "width: auto !important;" +
               "}" +
               ".calTableRes{" +
               "font - size: 46px !important;" +
               "line - height: 56px !important;" +
               "padding: 13px 30px !important;" +
               "}" +
               ".font38{" +
               "font - size: 38px !important;" +
               "line - height: 48px !important;" +
               "}" +
               ".Mob - wid { width: 70px !important; }" +
               ".Mob - wid img { width: 70px !important; }" +
               ".size16{ font - size:16px !important; line - height:22px !important; }" +
               "}" +
          "</style>" +
          "<style type = " + (char)34 + "text/css" + (char)34 + ">" +
               "@media only screen and(max-width: 480px)" +
               "{" +
               ".tdWidth {" +
               "width: 10px !important;" +
               "}" +
               ".fontSize14 {" +
               "font - size: 12px !important;" +
               "line - height: 18px !important;" +
               "}" +
               ".fontSize23 {" +
               "font - size: 23px !important;" +
               "line - height: 28px !important;" +
               "}" +
               ".hidden {" +
               "display: none !important;" +
               "}" +
               ".height10{" +
               "height: 10px !important;" +
               "}" +
               ".height15{" +
               "height: 15px !important;" +
               "}" +
               ".logo{ width: 60px !important; }" +
               ".logo img { width: 60px !important; }" +
               ".width10{ width: 20px !important; }" +
               "}" +
               "@media only screen and(max-width: 360px)" +
               "{" +
               ".logo{ width: 50px !important; }" +
               ".logo img { width: 50px !important; }" +
               ".width10{ width: 10px !important; }" +
               ".tdWidth {    width: 5px !important;" +
               "}" +
               ".height15{" +
               "height: 10px !important;" +
               "}" +
               "}" +
          "</style> " +
          "</head> " +
          "<body style = " + (char)34 + "margin-bottom: 0; -webkit-text-size-adjust: 100%; padding-bottom: 0;  margin-top: 0; margin-right: 0; -ms-text-size-adjust: 100%; margin-left: 0; padding-top: 0; padding-right: 0; padding-left: 0; width: 100%;" + (char)34 + "><style type = " + (char)34 + "text/css" + (char)34 + ">div#emailPreHeader{ display: none !important; }</style><div id=" + (char)34 + "emailPreHeader" + (char)34 + " style=" + (char)34 + "mso-hide:all; visibility:hidden; opacity:0; color:transparent; mso-line-height-rule:exactly; line-height:0; font-size:0px; overflow:hidden; border-width:0; display:none !important;" + (char)34 + ">Visita nuesta pagina</div>" +
          "<img src=" + (char)34 + "https://email.awscloud.com/trk?t=1&mid=MTEyLVRaTS03NjY6MDo1MDcxODQ6MzA4NTM0NzoxNDA0NjA3Nzo3NTc5NjY6OTozMTM5NjUyOjM4MzgyMTE5Njphcmlvc0BsaXRvcG9saXMuY29t" + (char)34 + " width=" + (char)34 + "1" + (char)34 + " height=" + (char)34 + "1" + (char)34 + " style=" + (char)34 + "display:none !important;" + (char)34 + " alt=" + (char)34 + "" + (char)34 + "/>" +
          "<!-- Outer table START --> " +
          "<table border = " + (char)34 + "0" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " style=" + (char)34 + "-webkit-text-size-adjust: 100%; -ms-text-size-adjust: 100%; mso-table-lspace: 0pt; mso-table-rspace: 0pt;  border-spacing: 0; border-collapse: collapse;margin:0 auto;" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td style = " + (char)34 + "background-color:#030226;" + (char)34 + " bgcolor=" + (char)34 + "#030226" + (char)34 + "> " +
          "<table class=" + (char)34 + "deviceWidth" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "600" + (char)34 + " style=" + (char)34 + "border-collapse: collapse; margin:0 auto;min-width:600px;" + (char)34 + "> " +
          "<tbody>" +
          "<tr> " +
          "<td class=" + (char)34 + "mktoContainer" + (char)34 + " id=" + (char)34 + "template-wrapper" + (char)34 + " valign=" + (char)34 + "top" + (char)34 + " style=" + (char)34 + "vertical-align:top;border-collapse: collapse;" + (char)34 + ">" +
          "<table class=" + (char)34 + "mktoModule" + (char)34 + " id=" + (char)34 + "Hidden-Pre-Header" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " style=" + (char)34 + "border-collapse: collapse; margin:0 auto; min-width:100%;" + (char)34 + ">" +
          "<tbody>" +
          "<tr> " +
          "<td style = " + (char)34 + "display:none !important; visibility:hidden; mso-hide:all; font-size:1px; color:#D982B2; line-height:1px; max-height:0px; max-width:0px; opacity:0; overflow:hidden;" + (char)34 + ">  </td> " +
          "</tr> " +
          "</tbody> " +
          "</table>" +
          "<table class=" + (char)34 + "mktoModule" + (char)34 + " id=" + (char)34 + "Hero-Section269f913c-778e-46e4-bc86-72e599cdba6c" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " style=" + (char)34 + "margin:0 auto; min-width:100%;" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td style = " + (char)34 + "background-color:#030226;" + (char)34 + "> " +
          "<table width = " + (char)34 + "100%" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " style=" + (char)34 + "border-spacing: 0; mso-table-lspace: 0pt; mso-table-rspace: 0pt; margin:0 auto;width:100%;border-collapse: collapse;" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " class=" + (char)34 + "deviceWidth" + (char)34 + " align=" + (char)34 + "center" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td class=" + (char)34 + "full-width" + (char)34 + " valign=" + (char)34 + "top" + (char)34 + " style=" + (char)34 + "text-align: center;" + (char)34 + "> " +
          "<div class=" + (char)34 + "mktoImg" + (char)34 + " id=" + (char)34 + "Hero-Image269f913c-778e-46e4-bc86-72e599cdba6c" + (char)34 + " mktolockimgsize=" + (char)34 + "true" + (char)34 + "> " +
          "<a href =" + (char)34 + "https://www.litopolis.com/" + (char)34 + " target=" + (char)34 + "_blank" + (char)34 + ">" +
          "<img src = " + (char)34 + urlcuerpo + (char)34 + " width=" + (char)34 + "600" + (char)34 + " alt=" + (char)34 + "Hero Graphic" + (char)34 + " style=" + (char)34 + "" + (char)34 + "/>" +
          "</a> " +
          "</div></td> " +
          "</tr> " +
          "</tbody> " +
          "</table>" +
          "</td>" +
          "</tr> " +
          "</tbody>" +
          "</table>" +
          "<table class=" + (char)34 + "mktoModule" + (char)34 + " id=" + (char)34 + "title-section1d2cb5c78-c533-46bd-8658-018514fe3455" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " style=" + (char)34 + "margin:0 auto; min-width:100%;" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td bgcolor = " + (char)34 + "#FAFAFA" + (char)34 + "> " +
          "<table width=" + (char)34 + "540" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " style=" + (char)34 + "border-spacing: 0; mso-table-lspace: 0pt; mso-table-rspace: 0pt; margin:0 auto;width:540px;border-collapse: collapse;" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " class=" + (char)34 + "deviceWidth1" + (char)34 + " align=" + (char)34 + "center" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td height = " + (char)34 + "15" + (char)34 + " style=" + (char)34 + "line-height:1px;font-size:1px;" + (char)34 + ">&nbsp;</td>" +
          "</tr> " +
          "<tr> " +
          "<td class=" + (char)34 + "fontBold fontSize23" + (char)34 + " valign=" + (char)34 + "top" + (char)34 + " style=" + (char)34 + "vertical-align:top;color:#232F3E;font-size:28px;line-height:38px;text-align:left;font-family: 'AmazonEmber-Heavy',arial, Helvetica, sans-serif;font-weight:normal;" + (char)34 + "> " +
          "<div class=" + (char)34 + "mktoText" + (char)34 + " id=" + (char)34 + "titlesecTextd2cb5c78-c533-46bd-8658-018514fe3455" + (char)34 + "> " +
          "<div style = " + (char)34 + "text-align: left;" + (char)34 + "> " +
          "<span style = " + (char)34 + "font-size: 22px;" + (char)34 + "><strong>" + Titulo + "</strong></span> " +
          "<br/> " +
          "</div> " +
          "</div> </td> " +
          "</tr> " +
          "<tr>" +
          "<td height = " + (char)34 + "10" + (char)34 + " style=" + (char)34 + "line-height:1px;font-size:1px;" + (char)34 + ">&nbsp;</td> " +
          "</tr> " +
          "<tr> " +
          "<td valign = " + (char)34 + "top" + (char)34 + " style=" + (char)34 + "vertical-align:top;color:#232F3E;font-size:16px;line-height:24px;text-align:left;font-family: AmazonEmber,arial, Helvetica, sans-serif;font-weight:normal;" + (char)34 + "> " +
          "<div class=" + (char)34 + "mktoText" + (char)34 + " id=" + (char)34 + "csContentd2cb5c78-c533-46bd-8658-018514fe3455" + (char)34 + "> " +

          "" +

          "<span style=" + (char)34 + "font-family: Arial; color: #000000; background-color: transparent; font-weight: 400; font-variant-ligatures: normal; font-variant-caps: normal; font-variant-east-asian: normal; font-variant-position: normal; text-decoration: none; vertical-align: baseline; white-space: pre-wrap;" + (char)34 + "> " + cuerpo + " </span></span> " +

          "<div style = " + (char)34 + "line-height: 1.2; margin-top: 0pt; margin-bottom: 0pt; text-align: left;" + (char)34 + "> " +
          "<span style = " + (char)34 + "font-size: 16px; font-family: Arial; color: #000000; background-color: transparent; font-weight: 400; font-variant-ligatures: normal; font-variant-caps: normal; font-variant-east-asian: normal; font-variant-position: normal; text-decoration: none; vertical-align: baseline; white-space: " +
          "pre-wrap;" + (char)34 + "></span> " +
          "<br/> " +
          "</div> " +
          /*parte final correo*/
          "<div style = " + (char)34 + "text-align: left;" + (char)34 + "> " +
          "<span style = " + (char)34 + "font-family: arial black, avant garde;" + (char)34 + "><strong><span style = " + (char)34 + "color: #1B4C8C; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; text-indent: 0px; text-transform: none; white-space: normal; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-align: center; background-color: " +
          "#fafafa; float: none; display: inline !important;" + (char)34 + ">" + fecha + " a las " + hora + " hrs.</span></strong></span> " +
          "</div> " +
          "<div style = " + (char)34 + "text-align: left;" + (char)34 + ">" +
          "<strong><span style = " + (char)34 + "font-family: arial, helvetica, sans-serif;" + (char)34 + "><span style = " + (char)34 + "color: #1B4C8C; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; text-indent: 0px; text-transform: none; white-space: normal; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-align: center; background-color: " +
          "#fafafa; float: none; display: inline !important; font-family: arial black, avant garde;" + (char)34 + ">" +
          "Atte: " + atte + "" +
          "</span><span style = " + (char)34 + "color: #ff9900; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; text-indent: 0px; text-transform: none; white-space: normal; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-align: center; " +
          "background-color: #fafafa; float: none; display: inline !important;" + (char)34 + "><br/></span></span></strong>" +
          "</div> " +
          "<div style = " + (char)34 + "text-align: left;" + (char)34 + "> " +
          "<strong><span style = " + (char)34 + "font-family: arial, helvetica, sans-serif;" + (char)34 + "><span style = " + (char)34 + "color: #ff9900; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; text-indent: 0px; text-transform: none; white-space: normal; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-align: center; background-color: " +
          "#fafafa; float: none; display: inline !important; font-family: arial black, avant garde;" + (char)34 + "></span></span></strong> " +
          "<br/> " +
          "</div> " +
          "<div style = " + (char)34 + "text-align: left;" + (char)34 + "> " +
          "<strong><span style = " + (char)34 + "font-family: arial, helvetica, sans-serif;" + (char)34 + "><span style = " + (char)34 + "color: #ff9900; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; text-indent: 0px; text-transform: none; white-space: normal; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-align: center; background-color: " +
          "#fafafa; float: none; display: inline !important; font-family: arial black, avant garde;" + (char)34 + "></span></span></strong> " +
          "</div> " +
          "<div style = " + (char)34 + "text-align: center;" + (char)34 + "> " +
          "<strong></strong> " +
          "<a href =" + (char)34 + "https://www.litopolis.com/" + (char)34 + " target=" + (char)34 + "_blank" + (char)34 + "" +
          "><strong><span style = " + (char)34 + "font-family: AmazonEmber, Arial, Helvetica, sans-serif; color: #ff9900; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; text-indent: 0px; text-transform: none; white-space: normal; " +
          "word-spacing: 0px; -webkit-text-stroke-width: 0px; text-align: center; background-color: #fafafa; float: none; display: inline !important;" + (char)34 + "><img src=" + (char)34 +
          "https://i.postimg.cc/T2zHvn0B/Captura.png" + (char)34 + " alt=" + (char)34 + "Screen Shot 2022-08-03 at 2.23.03 PM.png" + (char)34 + " style=" + (char)34 + "float: left;" + (char)34 + " width=" + (char)34 + "519" + (char)34 + " height=" + (char)34 + "290" + (char)34 + "/></span></strong></a> " +
          "<br/> " +
          "</div> " +
          "<div style = " + (char)34 + "text-align: center;" + (char)34 + "> " +
          "<strong><span style = " + (char)34 + "font-family: AmazonEmber, Arial, Helvetica, sans-serif; color: #ff9900; font-size: 16px; font-style: normal; font-variant-ligatures: normal; font-variant-caps: normal; letter-spacing: normal; text-indent: 0px; text-transform: none; white-space: normal; word-spacing: 0px; -webkit-text-stroke-width: 0px; text-align: center; background-color: " +
          "#fafafa; float: none; display: inline !important;" + (char)34 + "></span></strong> " +
          "<br/> " +
          "</div> " +
          "</div> </td> " +
          "</tr> " +
          "<tr> " +
          "<td height = " + (char)34 + "20" + (char)34 + " style=" + (char)34 + "line-height:1px;font-size:1px;" + (char)34 + ">&nbsp;</td> " +
          "</tr> " +
          "</tbody> " +
          "</table> </td> " +
          "</tr> " +
          "</tbody> " +
          "</table>" +
          "<table class=" + (char)34 + "mktoModule" + (char)34 + " id=" + (char)34 + "button-section2627e2a9-c36c-46f2-b08c-d417c6519f86" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " style=" + (char)34 + "margin:0 auto; min-width:100%;" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td bgcolor = " + (char)34 + "#FAFAFA" + (char)34 + "> " +
          "<table width=" + (char)34 + "540" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " style=" + (char)34 + "border-spacing: 0; mso-table-lspace: 0pt; mso-table-rspace: 0pt; margin:0 auto;width:540px;border-collapse: collapse;" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " class=" + (char)34 + "deviceWidth1" + (char)34 + " align=" + (char)34 + "center" + (char)34 + "> " +
          "<tbody>" +
          "<tr>" +
          "<td height = " + (char)34 + "15" + (char)34 + " style=" + (char)34 + "line-height:1px;font-size:1px;" + (char)34 + ">&nbsp;</td> " +
          "</tr> " +
          "<tr> " +
          "<td valign = " + (char)34 + "top" + (char)34 + " style=" + (char)34 + "vertical-align:top;" + (char)34 + "> " +
          "<div> " +
          "<table align = " + (char)34 + "Left" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " border=" + (char)34 + "0" + (char)34 + "> " +
          "</table> " +
          "</div> </td> " +
          "</tr> " +
          "<tr> " +
          "<td height = " + (char)34 + "15" + (char)34 + " style=" + (char)34 + "line-height:1px;font-size:1px;" + (char)34 + ">&nbsp;</td> " +
          "</tr> " +
          "</tbody> " +
          "</table> </td> " +
          "</tr> " +
          "</tbody> " +
          "</table>" +
          "<table class=" + (char)34 + "mktoModule" + (char)34 + " id=" + (char)34 + "footer59b8489c-176a-488d-8848-3e3b64855196" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " style=" + (char)34 + "margin:0 auto; min-width:100%;" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td> " +
          "<table class=" + (char)34 + "deviceWidth1" + (char)34 + " align=" + (char)34 + "center" + (char)34 + " width=" + (char)34 + "540" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " border=" + (char)34 + "0" + (char)34 + "> " +
          "<tbody>" +
          "<tr> " +
          "<td height = " + (char)34 + "20" + (char)34 + " style=" + (char)34 + "font-size: 1px; line-height: 1px;" + (char)34 + ">&nbsp;</td> " +
          "</tr> " +
          "<tr> " +
          "<td style = " + (char)34 + "vertical-align:top;color:#888888;font-size: 14px;line-height: 20px;text-align:left;font-family:'AmazonEmber',arial,helvetica;font-weight:normal;" + (char)34 + "> " +
          "<table width = " + (char)34 + "100%" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " border=" + (char)34 + "0" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td> " +
          "<div class=" + (char)34 + "mktoSnippet" + (char)34 + " id=" + (char)34 + "footersnippet159b8489c-176a-488d-8848-3e3b64855196" + (char)34 + "> " +
          "<table border = " + (char)34 + "0" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td class=" + (char)34 + "social-container" + (char)34 + " style=" + (char)34 + "padding: 10px 0px 10px 0px;" + (char)34 + " align=" + (char)34 + "center" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + "><span class=" + (char)34 + "socialPod" + (char)34 + "> <a target = " + (char)34 + "_blank" + (char)34 + " href=" +
          "" + (char)34 + "https://www.google.com/maps?ll=19.406425,-99.136564&z=16&t=m&hl=es-MX&gl=US&mapclient=embed&cid=7658837218352766040" + (char)34 + "" +
          "><img src = " + (char)34 + "https://cdn-icons-png.flaticon.com/512/2875/2875433.png" + (char)34 + " alt=" + (char)34 + "AWS Blog" + (char)34 + " height=" + (char)34 + "20" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "20" + (char)34 + "/></a></span>&nbsp;<span class=" + (char)34 + "socialDivider" + (char)34 + ">&nbsp;<img src=" + (char)34 + "http://112-TZM-766.mktoweb.com/rs/112-TZM-766/images/aws-email-footer-line.png" + (char)34 + " alt=" + (char)34 + "ln brk" + (char)34 + "/>&nbsp;</span>&nbsp;<span class=" + (char)34 + "socialPod" + (char)34 + "> <a target=" + (char)34 + "_blank" + (char)34 + " href=" +
          "" + (char)34 + "https://www.facebook.com/impresos.litopolis.oficialhttps://www.facebook.com/impresos.litopolis.oficial" + (char)34 + "" +
          "><img src = " + (char)34 + "https://cdn-icons-png.flaticon.com/512/3670/3670124.png" + (char)34 + " alt=" + (char)34 + "Facebook" + (char)34 + " height=" + (char)34 + "20" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "20" + (char)34 + "/></a></span>&nbsp;&nbsp;<span class=" + (char)34 + "socialPod" + (char)34 + "> <a target=" + (char)34 + "_blank" + (char)34 + " href=" +
          "" + (char)34 + "https://twitter.com/litopolis" + (char)34 + ">" +
          "<img src = " + (char)34 + "https://cdn-icons-png.flaticon.com/512/3670/3670127.png" + (char)34 + " alt=" + (char)34 + "Twitter" + (char)34 + " height=" + (char)34 + "20" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "20" + (char)34 + "/></a></span>&nbsp;&nbsp;<span class=" + (char)34 + "socialPod" + (char)34 + "> <a target=" + (char)34 + "_blank" + (char)34 + " href=" +
          "" + (char)34 + "https://www.youtube.com/channel/UCu3f-MoEuPGwMrkKELG0cug" + (char)34 + ">" +
          "<img src = " + (char)34 + "https://cdn-icons-png.flaticon.com/512/3670/3670163.png" + (char)34 + " alt=" + (char)34 + "YouTube" + (char)34 + " height=" + (char)34 + "20" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "20" + (char)34 + "/></a></span>&nbsp;&nbsp;<span class=" + (char)34 + "socialPod" + (char)34 + "> <a target=" + (char)34 + "_blank" + (char)34 + " href=" +
          "" + (char)34 + "https://api.whatsapp.com/send?phone=+525563471549&text=Litopolis-Holaestoyinteresadoen:" + (char)34 + ">" +
          "<img src = " + (char)34 + "https://cdn-icons-png.flaticon.com/512/5968/5968841.png" + (char)34 + " alt=" + (char)34 + "WhatsApp" + (char)34 + " height=" + (char)34 + "20" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " width=" + (char)34 + "20" + (char)34 + "/></a> </span></td> " +
          "</tr> " +
          "</tbody> " +
          "</table> " +
          "</div> </td> " +
          "</tr> " +
          "<tr> " +
          "<td height = " + (char)34 + "15" + (char)34 + " style=" + (char)34 + "font-size: 1px; line-height: 1px;" + (char)34 + ">&nbsp;</td> " +
          "</tr> " +
          "<tr> " +
          "<td> " +
          "<div class=" + (char)34 + "mktoSnippet" + (char)34 + " id=" + (char)34 + "footersnippet259b8489c-176a-488d-8848-3e3b64855196" + (char)34 + "> " +
          "<table width = " + (char)34 + "100%" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " border=" + (char)34 + "0" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td style = " + (char)34 + "padding: 10px 0px 10px 0px; border-top: 1px solid #e8e8e8;" + (char)34 + "> " +
          "<table style=" + (char)34 + "border-collapse: collapse;" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td align = " + (char)34 + "center" + (char)34 + "> " +
          "<table style = " + (char)34 + "border-collapse: collapse;" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + ">" +
          "</table> </td> " +
          "</tr> " +
          "</tbody> " +
          "</table> </td> " +
          "</tr> " +
          "</tbody> " +
          "</table> " +
          "</div> </td> " +
          "</tr> " +
          "<tr> " +
          "<td height = " + (char)34 + "15" + (char)34 + " style=" + (char)34 + "font-size: 1px; line-height: 1px;" + (char)34 + ">&nbsp;</td> " +
          "</tr> " +
          "<tr> " +
          "<td> " +
          "<div class=" + (char)34 + "mktoSnippet" + (char)34 + " id=" + (char)34 + "footersnippet359b8489c-176a-488d-8848-3e3b64855196" + (char)34 + "> " +
          "<div class=" + (char)34 + "mktEditable" + (char)34 + " id=" + (char)34 + "Footer" + (char)34 + "> " +
          "<table style = " + (char)34 + "max-width: 600px;" + (char)34 + " class=" + (char)34 + "responsive-table" + (char)34 + " width=" + (char)34 + "100%" + (char)34 + " cellspacing=" + (char)34 + "0" + (char)34 + " cellpadding=" + (char)34 + "0" + (char)34 + " border=" + (char)34 + "0" + (char)34 + " align=" + (char)34 + "center" + (char)34 + "> " +
          "<tbody> " +
          "<tr> " +
          "<td class=" + (char)34 + "font-family-decor" + (char)34 + " style=" + (char)34 + "font-size: 12px; line-height: 18px; color: #F0F2F3;" + (char)34 + " align=" + (char)34 + "center" + (char)34 + "> " +
          "<dl>" +
          "<dt style = " + (char)34 + "color: #F0F2F3; font-size: 10px; text-align: center;" + (char)34 + ">" +
          " Si desea consultar nuestro aviso de privacidad empleados," +
          "<a href =" + (char)34 + "https://www.litopolis.com/aviso-de-privacidad-para-proveedores/" + (char)34 + " style=" + (char)34 + "-ms-text-size-adjust: 100%; -webkit-text-size-adjust: 100%; color: #D90479; font-size: 10px; text-decoration: none;" + (char)34 + "" +
          ">de clic aquí</a>.</dt> " +
          "<dt style = " + (char)34 + "color: #F0F2F3; font-size: 10px; text-align: center;" + (char)34 + ">" +
          "La primera impresión jamas se Olvida, Aviso de Privacidad Clientes" +
          "<a href=" + (char)34 + "https://www.litopolis.com/aviso-de-privacidad-para-clientes/" + (char)34 + "> " +
          "de clic aqui</a>,<br> Este mensaje lo ha generado y distribuido automaticamente, Cualquier duda Comunicarse a la Ext. 4301, o al Correo: arios@litopolis.com | programador.finanzas@litopolis.com (MX). " +
          "</dt> " +
          "<dt style = " + (char)34 + "color: #F0F2F3; font-size: 10px; text-align: center;" + (char)34 + ">" +
          " © Litopolis 2022 |  Todos los derechos reservados</dt> " +
          "</dl> </td> " +
          "</tr> " +
          "</tbody> " +
          "</table> " +
          "</div> " +
          "</div> </td> " +
          "</tr> " +
          "</tbody> " +
          "</table> </td> " +
          "</tr> " +
          "<tr> " +
          "<td height = " + (char)34 + "20" + (char)34 + " style= " + (char)34 + "font-size: 1px; line-height: 1px;" + (char)34 + ">&nbsp;</td> " +
          "</tr> " +
          "</tbody> " +
          "</table> </td> " +
          "</tr> " +
          "</tbody> " +
          "</table></td> " +
          "</tr> " +
          "</tbody> " +
          "</table> </td> " +
          "</tr> " +
          "</tbody> " +
          "</table> " +
          "<p></p>" +
          "<img alt = " + (char)34 + "" + (char)34 + " src=" + (char)34 + "https://4hs3rzdz.r.us-east-1.awstrack.me/I0/010001826e566341-1c6d87a8-68e1-4067-9715-751c1c8c1774-000000/L7osFy_JpiH7BYb3odmwD8GVQbA=281" + (char)34 + " style=" + (char)34 + "display: none; width: 1px; height: 1px;" + (char)34 + ">" +
          "</body>" +
          "</html>";

            return html;

        }
    }
}
