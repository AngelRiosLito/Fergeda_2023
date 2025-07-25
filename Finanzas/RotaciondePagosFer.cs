using Fergeda_2023.Clases;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Finanzas
{
    class RotaciondePagosFer
    {
        private Consultas X = new Consultas();
        /***************************************************/
        private ArrayList TotalF = new ArrayList();
        private ArrayList diasCred = new ArrayList();
        private ArrayList diasCredT = new ArrayList();
        private ArrayList diasCredM = new ArrayList();
        /****************************************************/
        private ArrayList TotalFP = new ArrayList();
        private ArrayList diasCredTP = new ArrayList();
        private ArrayList diasCredMP = new ArrayList();
        /****************************************************/
        private ArrayList Porcentage = new ArrayList();
        /****************************************************/
        private ArrayList TotalFPS = new ArrayList();
        private ArrayList diasCredTPS = new ArrayList();
        private ArrayList diasCredMPS = new ArrayList();
        /******************************************************/
        private ArrayList TotalFPSS = new ArrayList();
        private ArrayList diasCredTPSS = new ArrayList();
        private ArrayList diasCredMPSS = new ArrayList();
        private void limpiar()
        {
            TotalF.Clear(); diasCred.Clear(); diasCredT.Clear(); diasCredM.Clear();
            TotalFP.Clear(); diasCredTP.Clear(); diasCredM.Clear();
            Porcentage.Clear();
            TotalFPS.Clear(); diasCredTPS.Clear(); diasCredMPS.Clear();
            diasCredMP.Clear();
            TotalFPSS.Clear(); diasCredTPSS.Clear(); diasCredMPSS.Clear();
        }

        private void diasCredito(int año)
        {
            limpiar();
            diasCred = X.listaPago("SELECT distinct Crédito FROM fergeda.f_rotacion order by Crédito asc;");
            for (int i = 1; i <= 12; i++)
            {
                TotalF.Add(X.elem3("SELECT count(Factura) FROM  fergeda.f_rotacion  where year(Fecha_Vencimiento)='" + año + "' and month(Fecha_Vencimiento)='" + i + "';"));
                TotalFP.Add(X.elem3("SELECT count(Factura) FROM  fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and month(Fecha_Vencimiento)='" + i + "' and Pagado='Si';"));
                TotalFPS.Add(X.elem3("SELECT Sum(Importe_Factura) FROM  fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and month(Fecha_Vencimiento)='" + i + "';"));
                TotalFPSS.Add(X.elem3("SELECT Sum(Importe_Factura) FROM  fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and month(Fecha_Vencimiento)='" + i + "' and (Pagado='No' or Pagado='V');"));
            }
            TotalF.Add(X.elem3("SELECT count(*) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "';"));
            TotalFP.Add(X.elem3("SELECT count(*) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and Pagado='Si';"));
            TotalFPS.Add(X.elem3("SELECT Sum(Importe_Factura) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "';"));
            TotalFPSS.Add(X.elem3("SELECT Sum(Importe_Factura) FROM  fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and(Pagado='No' or Pagado='V');"));
            for (int i = 0; i < diasCred.Count; i++)
            {
                diasCredT.Add(X.elem3("select count(*) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and Crédito='" + diasCred[i] + "';"));
                diasCredTP.Add(X.elem3("select count(*) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and Crédito='" + diasCred[i] + "' and Pagado='Si';"));
                diasCredTPS.Add(X.elem3("select Sum(Importe_Factura) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and Crédito='" + diasCred[i] + "';"));
                diasCredTPSS.Add(X.elem3("select Sum(Importe_Factura) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and Crédito='" + diasCred[i] + "' and(Pagado='No' or Pagado='V');"));
            }
            for (int i = 0; i < diasCred.Count; i++)
            {
                int cred = Convert.ToInt32(diasCred[i]);
                for (int j = 1; j <= 12; j++)
                {
                    diasCredM.Add(X.elem3("select count(*) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and month(Fecha_Vencimiento)='" + j
                        + "' and Crédito='" + cred + "';"));
                    diasCredMP.Add(X.elem3("select count(*) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and month(Fecha_Vencimiento)='" + j
                        + "' and Crédito='" + cred + "' and (Pagado='No' or Pagado='V');"));/*facturas pagadas*/
                    diasCredMPS.Add(X.elem3("select Sum(Importe_Factura) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and month(Fecha_Vencimiento)='" + j
                        + "' and Crédito='" + cred + "';"));
                    diasCredMPSS.Add(X.elem3("select Sum(Importe_Factura) FROM fergeda.f_rotacion where year(Fecha_Vencimiento)='" + año + "' and month(Fecha_Vencimiento)='" + j
                        + "' and Crédito='" + cred + "'  and(Pagado='No' or Pagado='V');"));

                }
            }

        }

        public void llenarTb(DataGridView tb, int año)
        {
            int mes = 0;
            diasCredito(año);
            tb.DataSource = null;
            tb.Columns.Add("inicial", "Inicial");
            for (int i = 0; i < TotalF.Count + 2; i++)
            {
                tb.Columns.Add("--------", "--------");
            }
            tb.Rows.Add("-");
            tb.Rows.Add("-", "", "", "", "", "", "Rotación ", "      de ", "Pagos", "", "", "", "", DateTime.Now.ToString("yyyy/MM/dd"));

            tb.Rows.Add("-");
            tb.Rows.Add("Cuenta de Importe Factura", "", "", "Facturas", "Vencidas", "en el mes");
            tb.Rows.Add("Dias de Crédito", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Total General");
            for (int i = 0; i < diasCred.Count; i++)
            {
                tb.Rows.Add(diasCred[i], "      " + diasCredM[0 + mes], "      " + diasCredM[1 + mes], "      " + diasCredM[2 + mes], "      " + diasCredM[3 + mes], "      " +
                    diasCredM[4 + mes], "      " + diasCredM[5 + mes], "      " + diasCredM[6 + mes], "      " + diasCredM[7 + mes], "      " + diasCredM[8 + mes], "      " +
                    diasCredM[9 + mes], "      " + diasCredM[10 + mes], "      " + diasCredM[11 + mes], "      " + diasCredT[i]);
                mes += 12;
            }
            tb.Rows.Add("Total General", "      " + TotalF[0], "      " + TotalF[1], "      " + TotalF[2], "      " + TotalF[3], "      " + TotalF[4], "      " + TotalF[5], "      " + TotalF[6], "      " + TotalF[7], "      " + TotalF[8], "      " + TotalF[9], "      " + TotalF[10], "      " + TotalF[11], "      " + TotalF[12]);
            tb.Rows.Add(" ");
            tb.Rows.Add("Cuenta de Importe Factura", "", "", "Facturas", "pendientes", "por pagar", "en el mes");
            tb.Rows.Add("Dias de Crédito", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Total General");
            mes = 0;
            for (int i = 0; i < diasCred.Count; i++)
            {
                tb.Rows.Add(diasCred[i], "      " + diasCredMP[0 + mes], "      " + diasCredMP[1 + mes], "      " + diasCredMP[2 + mes], "      " + diasCredMP[3 + mes], "      " +
                    diasCredMP[4 + mes], "      " + diasCredMP[5 + mes], "      " + diasCredMP[6 + mes], "      " + diasCredMP[7 + mes], "      " + diasCredMP[8 + mes], "      " +
                    diasCredMP[9 + mes], "      " + diasCredMP[10 + mes], "      " + diasCredMP[11 + mes], "      " + diasCredTP[i]);
                mes += 12;
            }
            tb.Rows.Add("Total General", "      " + TotalFP[0], "      " + TotalFP[1], "      " + TotalFP[2], "      " + TotalFP[3], "      " + TotalFP[4], "      " + TotalFP[5], "      " + TotalFP[6], "      " + TotalFP[7], "      " + TotalFP[8], "      " + TotalFP[9], "      " + TotalFP[10], "      " + TotalFP[11], "      " + TotalFP[12]);
            for (int i = 0; i <= 12; i++)
            {
                if (Convert.ToInt32(compo(TotalF[i].ToString())) > 0)
                {
                    Porcentage.Add((Convert.ToInt32(compo(TotalFP[i].ToString())) * 100) / Convert.ToInt32(compo(TotalF[i].ToString())));
                }
                else
                {
                    Porcentage.Add(0);
                }

            }
            mes = 0;
            tb.Rows.Add("                  %", "    %" + Porcentage[0], "    %" + Porcentage[1], "    %" + Porcentage[2], "    %" + Porcentage[3], "    %" + Porcentage[4], "    %" + Porcentage[5], "    %" + Porcentage[6], "    %" + Porcentage[7], "    %" + Porcentage[8], "    %" + Porcentage[9], "    %" + Porcentage[10], "    %" + Porcentage[11], "    %" + Porcentage[12]);
            tb.Rows.Add("-");
            tb.Rows.Add("Suma de Importe Factura", "", "", "Saldo", "total", "de facturas", "por mes");
            tb.Rows.Add("Dias de Crédito", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Total General");
            for (int i = 0; i < diasCred.Count; i++)
            {
                tb.Rows.Add(diasCred[i], float.Parse(compo(diasCredMPS[0 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPS[1 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPS[2 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPS[3 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPS[4 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPS[5 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPS[6 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPS[7 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPS[8 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPS[9 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPS[10 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPS[11 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredTPS[i].ToString())).ToString("N2"));
                mes += 12;
            }
            tb.Rows.Add("Total General", float.Parse(compo(TotalFPS[0].ToString())).ToString("N2"), float.Parse(compo(TotalFPS[1].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPS[2].ToString())).ToString("N2"), float.Parse(compo(TotalFPS[3].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPS[4].ToString())).ToString("N2"), float.Parse(compo(TotalFPS[5].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPS[6].ToString())).ToString("N2"), float.Parse(compo(TotalFPS[7].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPS[8].ToString())).ToString("N2"), float.Parse(compo(TotalFPS[9].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPS[10].ToString())).ToString("N2"), float.Parse(compo(TotalFPS[11].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPS[12].ToString())).ToString("N2"));
            tb.Rows.Add("-");
            tb.Rows.Add("Suma de Importe Factura", "", "", "Saldo", "pendiente", "de facturas", "por mes");
            tb.Rows.Add("Dias de Crédito", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre", "Total General");
            mes = 0;
            for (int i = 0; i < diasCred.Count; i++)
            {
                tb.Rows.Add(diasCred[i], float.Parse(compo(diasCredMPSS[0 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPSS[1 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPSS[2 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPSS[3 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPSS[4 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPSS[5 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPSS[6 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPSS[7 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPSS[8 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPSS[9 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredMPSS[10 + mes].ToString())).ToString("N2"), float.Parse(compo(diasCredMPSS[11 + mes].ToString())).ToString("N2"),
                                         float.Parse(compo(diasCredTPSS[i].ToString())).ToString("N2"));
                mes += 12;
            }
            tb.Rows.Add("Total General", float.Parse(compo(TotalFPSS[0].ToString())).ToString("N2"), float.Parse(compo(TotalFPSS[1].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPSS[2].ToString())).ToString("N2"), float.Parse(compo(TotalFPSS[3].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPSS[4].ToString())).ToString("N2"), float.Parse(compo(TotalFPSS[5].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPSS[6].ToString())).ToString("N2"), float.Parse(compo(TotalFPSS[7].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPSS[8].ToString())).ToString("N2"), float.Parse(compo(TotalFPSS[9].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPSS[10].ToString())).ToString("N2"), float.Parse(compo(TotalFPSS[11].ToString())).ToString("N2"),
                                         float.Parse(compo(TotalFPSS[12].ToString())).ToString("N2"));

            tb.Rows.Add("                  %", "    %" + Porcentage[0], "    %" + Porcentage[1], "    %" + Porcentage[2], "    %" + Porcentage[3], "    %"
                + Porcentage[4], "    %" + Porcentage[5], "    %" + Porcentage[6], "    %" + Porcentage[7], "    %" + Porcentage[8], "    %" + Porcentage[9], "    %"
                + Porcentage[10], "    %" + Porcentage[11], "    %" + Porcentage[12]);
            tb.Rows.Add("-");
        }
        private string compo(string dat)
        {
            if (dat == null || dat == "")           
                dat = "0";   
            else { dat = dat; }
            return dat;
        }
    }
}
