using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Clases
{
    class FormatoFechas
    {
        public string foratoFecha(string fecha)
        {
            char[] sp = { '/' };
            string[] arr = fecha.Split(sp);

            fecha = arr[2] + "/" + meses(arr[1]) + "/" + arr[0].Remove(0, 2);


            return fecha;
        }
        public string formatoFechaLarga(string fecha)
        {
            char[] sp = { '/' };
            string[] arr = fecha.Split(sp);
            return "Ciudad de México a " + arr[2] + " de " + Mes(fecha) + " de " + arr[0];
        }
        public string formatoFecha(string fecha)
        {
            char[] sp = { '/' };
            string[] arr = fecha.Split(sp);
            return "Ciudad de México a " + arr[2] + " de " + Mes(fecha) + " de " + arr[0];
        }
        public string Mes(string fecha)
        {
            char[] sp = { '/' };
            string[] arr = fecha.Split(sp);
            switch (arr[1])
            {
                case "01": arr[1] = "Enero"; break;
                case "02": arr[1] = "Febrero"; break;
                case "03": arr[1] = "Marzo"; break;
                case "04": arr[1] = "Abril"; break;
                case "05": arr[1] = "Mayo"; break;
                case "06": arr[1] = "Junio"; break;
                case "07": arr[1] = "Julio"; break;
                case "08": arr[1] = "Agosto"; break;
                case "09": arr[1] = "Septiembre"; break;
                case "10": arr[1] = "Octubre"; break;
                case "11": arr[1] = "Noviembre"; break;
                case "12": arr[1] = "Diciembre"; break;
            }
            return arr[1];
        }
        public string foratoFecha2(string fecha)
        {
            char[] sp = { '/' };
            string[] arr = fecha.Split(sp);


            fecha = arr[0] + "/" + meses(arr[1]) + "/" + arr[2].Remove(0, 2);



            return fecha;
        }


        public string meses(string mes)
        {
            int mesL = Convert.ToInt32(mes);
            switch (mesL)
            {
                case 1:
                    mes = "Ene";
                    break;
                case 2:
                    mes = "Feb";
                    break;
                case 3:
                    mes = "Mar";
                    break;
                case 4:
                    mes = "Abr";
                    break;
                case 5:
                    mes = "May";
                    break;
                case 6:
                    mes = "Jun";
                    break;
                case 7:
                    mes = "Jul";
                    break;
                case 8:
                    mes = "Ago";
                    break;
                case 9:
                    mes = "Sep";
                    break;
                case 10:
                    mes = "Oct";
                    break;
                case 11:
                    mes = "nov";
                    break;
                case 12:
                    mes = "Dic";
                    break;
            }
            return mes;
        }

        public DateTime diaPago(DateTimePicker time, int dias)
        {
            DateTime date = Convert.ToDateTime(time.Value.ToString("dd/MM/yyyy"));
            return date.AddDays(dias);
        }

        public DateTime diasFergeda(string dias, DateTimePicker time)
        {
            Consultas X = new Consultas();
            int diasPag = 0;

            diasPag = Convert.ToInt32(dias);
            string fecha = diaPago(time, diasPag).ToString("yyyy/MM/dd");

            for (int i = 1; i < 8; i++)
            {
                if (X.elem3("select Date_format('" + fecha + "','%W');").Equals("Wednesday")) { break; }
                else                
                    fecha = diaPago(time, diasPag + i).ToString("yyyy/MM/dd");
                
            }
            return DateTime.Parse(fecha);
        }

        public int CalcularDias(DateTime FechaNueva, DateTime fechaAnt)
        {
            TimeSpan ts = FechaNueva - fechaAnt;
            return Convert.ToInt32(ts.Days);
        }
        public void ActualizarDiasFergeda(int dias, int id)
        {
            Consultas X = new Consultas();
            if (dias > 7)
            {
                X.insertar("UPDATE fergeda.f_cuentasporpagar SET dias_restantes='" + dias + "' WHERE idf_cuentasporpagar='" + id + "';");
            }
            else /*if (dias <= 7 && dias > 0)*/
            {
                X.insertar("UPDATE fergeda.f_cuentasporpagar SET vencido='" + float.Parse(X.elem3("SELECT importe FROM fergeda.f_ban_cuentasporpagar" +
                    " where idf_ban_cuentasporpagar='" + id + "';")) + "', pagado='V' WHERE idf_cuentasporpagar='" + id + "';");
            }
        }

        public void valNegativosFergeda(int dias, int id)
        {
            Consultas X = new Consultas();
            //MessageBox.Show(""+dias+ "\t"+id);
            if (X.elem3("SELECT pagado FROM fergeda.f_cuentasporpagar where  idf_cuentasporpagar='" + id + "';") != "Si")
            {
                float saldo = float.Parse(X.elem3("SELECT importe FROM fergeda.f_ban_cuentasporpagar " +
                    "where idf_ban_cuentasporpagar='" + id + "';"));
                X.insertar("UPDATE fergeda.f_cuentasporpagar SET vencido='" + saldo + "',pagado='V' WHERE idf_cuentasporpagar='" + id + "';");

            }

        }
    }
}
