using Fergeda_2023.General;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Clases
{
    class Consultas
    {
        MySqlConnection mcon = new MySqlConnection("datasource=192.168.0.4;port=3306; username= Admin1; password= LitoSistem31");
        private MensageC msg = new MensageC();
        string concatenado = "", val = "";
        #region Generales

        public void elimElemConConsulta(DataGridView tb) { tb.DataSource = null; }
        public void tablaDatos(DataGridView x, String con)
        {
            try
            {
                mcon.Close();
                //MessageBox.Show(con);
                mcon.Open();
                DataTable TablaDatos = new DataTable();
                MySqlDataAdapter Adaptador = new MySqlDataAdapter(con, mcon);
                Adaptador.Fill(TablaDatos);
                x.DataSource = TablaDatos;
                mcon.Close();

            }
            catch (Exception ex)
            {
                mcon.Close();
                Console.WriteLine(ex.Message);
            }

        }

        public string elem3(String query)
        {

            try
            {
                concatenado = "";
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        concatenado = linea.ItemArray[0].ToString();

                    }
                    mcon.Close();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                mcon.Close();

            }
            return concatenado;
        }
        public string elem2(String query, string dato)
        {
            mcon.Close();
            mcon.Open();
            MySqlCommand comando = new MySqlCommand(query, mcon);
            MySqlDataReader reader = comando.ExecuteReader();
            while (reader.Read())
            {

                val = reader[dato].ToString();
            }

            mcon.Close();

            return val;
        }
        public void insertar(string query)
        {
            try
            {
                mcon.Close();
                mcon.Open();
                MySqlCommand cmd = new MySqlCommand(query, mcon);
                cmd.ExecuteNonQuery();
                mcon.Close();

            }
            catch (Exception ex)
            {
                mcon.Close();
                MessageBox.Show("No se puedo Realizar la peticion " + ex);
            }
        }
        public string datConcatenado(String query)
        {

            try
            {
                concatenado = "";
                mcon.Close();
                mcon.Open();
                string otra = "";
                MySqlDataAdapter sda = new MySqlDataAdapter(query, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                //MessageBox.Show(query);
                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        concatenado = linea.ItemArray[0].ToString();
                        otra = "" + concatenado + "/" + otra;
                    }
                    mcon.Close();
                }

                return otra;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mcon.Close();
                return "";
            }

        }

        public void comb(String query, ComboBox com)
        {

            try
            {
                concatenado = "";
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        concatenado = linea.ItemArray[0].ToString();
                        com.Items.Add(concatenado);
                    }
                    mcon.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mcon.Close();
            }

        }

        public List<string> Litado(string query)
        {
            List<string> datos = new List<string>();
            try
            {
                mcon.Close();
                concatenado = "";
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        concatenado = linea.ItemArray[0].ToString();
                        datos.Add(concatenado);
                    }
                    mcon.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mcon.Close();
            }
            return datos;

        }
        ArrayList n1 = new ArrayList();
        public ArrayList listaArray(string query)
        {
            n1.Clear();
            try
            {
                concatenado = "";
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        n1.Add(linea.ItemArray[0].ToString());
                    }
                    mcon.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mcon.Close();
            }

            return n1;
        }
        public void text(String query, TextBox com)
        {

            try
            {
                com.Text = "";
                concatenado = "";
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        concatenado = linea.ItemArray[0].ToString();
                        com.Text += concatenado + "/";
                    }
                    mcon.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                mcon.Close();
            }
        }

        #endregion

        #region CUENTAS POR PAGAR   
        public void importePaFer(DataGridView tb, string cons, string cons2)
        {
            try
            {
                int mes = 1; double sum = 0;
                int año = 2020;
                tb.Columns.Add("Mes", "Mes"); tb.Columns.Add("Semana", "Semana"); tb.Columns.Add("Fecha", "Fecha Factura o Revision");
                tb.Columns.Add("Importe", "Importe Registrado");
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(cons, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        if (mes != Convert.ToInt32(linea.ItemArray[0].ToString()))
                        {
                            tb.Rows.Add("", "", "Total:", "   $ " + sum.ToString("N2"));
                            mes++;
                            sum = 0;
                        }
                        tb.Rows.Add(linea.ItemArray[0].ToString(), linea.ItemArray[1].ToString(), DateTime.Parse(linea.ItemArray[2].ToString()).ToString("yyyy/MM/dd"), "   $ " + double.Parse(linea.ItemArray[3].ToString()).ToString("N2"));
                        sum += float.Parse(linea.ItemArray[3].ToString());
                        año = Convert.ToInt32(DateTime.Parse(linea.ItemArray[2].ToString()).ToString("yyyy"));
                    }
                    mcon.Close();
                }
                tb.Rows.Add("", "", "Total:", "  $ " + sum.ToString("N2"));
                sum = 0;
                mes = 0;
                mcon.Open();

                MySqlCommand comando = new MySqlCommand(cons2, mcon);/*cambiar este importe por el año*/
                MySqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    val = reader["impor"].ToString();
                }
                mcon.Close();
                tb.Rows.Add("-", "", "Total general:", "    $ " + double.Parse(val).ToString("N2"));
            }

            catch (Exception ex)
            {
                mcon.Close();
                Console.WriteLine(ex.Message);
                
            }
        }

        public void importePagos(DataGridView tb, string cons)
        {
            try
            {
                int mes = 1; double sum = 0,  tolG = 0;
                tb.Columns.Add("Mes", "Mes"); tb.Columns.Add("Semana", "Semana"); tb.Columns.Add("Fecha", "Fecha de Pago");
                tb.Columns.Add("Importe", "Saldo Pagado");
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(cons, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                   
                        if (mes != Convert.ToInt32(linea.ItemArray[0].ToString()))
                        {
                            tb.Rows.Add("", "", "Total:", "   $ " + sum.ToString("N2"));
                            mes++;
                            tolG += sum;
                            sum = 0;
                        }
                        tb.Rows.Add(linea.ItemArray[0].ToString(), linea.ItemArray[1].ToString(), DateTime.Parse(linea.ItemArray[2].ToString()).ToString("yyyy/MM/dd"), "   $ " + double.Parse(linea.ItemArray[3].ToString()).ToString("N2"));
                        sum += float.Parse(linea.ItemArray[3].ToString());
                    }
                    mcon.Close();
                }
                tb.Rows.Add("", "", "Total:", "   $ " + sum.ToString("N2"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("No se pudo Completar la peticion" + ex.Message);
                throw;
            }
        }

        public void importeApag2(DataGridView tb, string cons)
        {
            try
            {
                concatenado = "";
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(cons, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        concatenado = linea.ItemArray[0].ToString();
                    }
                    mcon.Close();
                }
                tb.Rows.Add("", "", "Total General:", "$" + double.Parse(concatenado).ToString("N2"));
            }

            catch (Exception ex)
            {
                mcon.Close();
                Console.WriteLine("No se pudo Completar la peticion" + ex.Message);
                throw;
            }
        }
        public void repPagos(DataGridView tb, string cons)
        {
            try
            {
                int mes = 1; double sum = 0, tolG = 0;
                tb.Columns.Add("Mes", "Mes"); tb.Columns.Add("Semana", "Semana"); tb.Columns.Add("Fecha", "Fecha de Pago");
                tb.Columns.Add("Importe", "Pagos Realizados");
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(cons, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        if (mes != Convert.ToInt32(linea.ItemArray[0].ToString()))
                        {
                            tb.Rows.Add("", "", "Total:", "   $ " + sum.ToString("N2"));
                            mes++;
                            tolG += sum;
                            sum = 0;
                        }
                        tb.Rows.Add(linea.ItemArray[0].ToString(), linea.ItemArray[1].ToString(), DateTime.Parse(linea.ItemArray[2].ToString()).ToString("yyyy/MM/dd"), "   $ " + double.Parse(linea.ItemArray[3].ToString()).ToString("N2"));
                        sum += float.Parse(linea.ItemArray[3].ToString());
                    }
                    mcon.Close();
                }

                tb.Rows.Add("", "", "Total:", "   $ " + (sum - 1).ToString("N2"));////
                tolG += sum;
                tb.Rows.Add("-", "", "Total general:", "$  " + (tolG - 1).ToString("N2"));

            }
            catch (Exception ex)
            {
                mcon.Close();
                Console.WriteLine("No se pudo Completar la peticion" + ex.Message);
                throw;
            }
        }
        public void pagProveedores(DataGridView tb, string cons)
        {
            try
            {
                double totl = 0;
                tb.Columns.Add("Proveedor", "Proveedores");
                tb.Columns.Add("Saldo", "Saldo x Pagar");
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(cons, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    {
                        tb.Rows.Add(linea.ItemArray[0].ToString(), double.Parse(linea.ItemArray[1].ToString()).ToString("N2"));
                        totl += double.Parse(linea.ItemArray[1].ToString());
                    }
                    mcon.Close();
                }
                tb.Rows.Add("Total General:", totl.ToString("N2"));
            }
            catch (Exception ex)
            {
                mcon.Close();
                Console.WriteLine("No se pudo Completar la peticion" + ex);
                throw;
            }
        }
        public ArrayList listaPago(string query)
        {
            ArrayList n = new ArrayList();
            try
            {
                n.Clear();
                mcon.Close();
                mcon.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter(query, mcon);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                foreach (System.Data.DataRow linea in dt.Rows)
                {
                    { n.Add(linea.ItemArray[0].ToString()); }
                    mcon.Close();
                }
            }
            catch (Exception ex)
            {
                n.Add("0");
                Console.WriteLine(ex.Message);
                mcon.Close();
                throw;
            }
            return n;
        }


        #endregion

    }
}
