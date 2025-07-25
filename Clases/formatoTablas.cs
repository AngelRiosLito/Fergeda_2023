using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Clases
{
    class formatoTablas
    {
        public void quitarCelda(DataGridView tb) { tb.AllowUserToAddRows = false; }
        public void quitarOrdenamiento(DataGridView tb)
        {
            foreach (DataGridViewColumn Col in tb.Columns)
            {
                Col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
        public void colores(DataGridView tb,  int opcion)
        {

            /**/
            int r = 80, g = 212, b = 255, r1 = 175, g1 = 238, b1 = 238, en = 86, ca = 142, be = 191;

            /*tamaño de la fuente*/
            /*encabezado*/
            tb.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12);
            tb.ColumnHeadersDefaultCellStyle.BackColor = Color.Crimson;
            tb.DefaultCellStyle.Font = new Font("Arial", 12);
            /*color de la seleccion*/
            tb.DefaultCellStyle.SelectionForeColor = Color.White;
            tb.DefaultCellStyle.SelectionBackColor = Color.FromArgb(r, g, b);


            //tb.RowHeadersDefaultCellStyle.BackColor = Color.Crimson;

            /*altenado de colores en las tablas*/
            tb.RowsDefaultCellStyle.BackColor = Color.White;
            tb.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(r1, g1, b1);
            /**/

            /*ocualtar la barra lateral*/
            tb.RowHeadersVisible = false;

            //tb.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;/*Filas*/
            if (opcion == 0)
            {
                tb.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else if (opcion == 1) { tb.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; }


            /*propiedad para los encabezados*/
            tb.EnableHeadersVisualStyles = false;
            tb.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            tb.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(en, ca, be);
            tb.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }
        string res;

        public string CalculaTotales(DataGridView tb, int ps)
        {
            res = "";
            float Total1 = 0;
            try
            {
                for (int contador = 0; contador < tb.Rows.Count; contador++)
                { Total1 = Total1 + float.Parse(tb[ps, contador].Value.ToString()); }
                res = "" + Total1; ;
            }
            catch (Exception ex) { MessageBox.Show("Error " + ex, ex.Message, MessageBoxButtons.OK); res = "0"; }
            return res;
        }
        public void elimElemConConsulta(DataGridView tb) { tb.DataSource = null; }           /*borra los elementos con consulta*/
        public void elminarRows(DataGridView tb) { tb.Rows.Clear(); }                               /*borra con un encabezado*/
        public void selecElementosTabla(DataGridView tb, TextBox txt, int pos)                       /*ontienes el valor de una celda especifica*/
        {
            txt.Text = tb[pos, tb.CurrentCell.RowIndex].Value.ToString();
        }

        public void colorStatus(DataGridView tb, string celda, string comprobacion, int r, int g, int b)
        {
            foreach (DataGridViewRow row in tb.Rows)
            {
                if (row.Cells[celda].Value.ToString() == comprobacion)
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(r, g, b);
                }
            }
        }

        public void colorStatusNumeros(DataGridView tb, string celda, string comprobacion, int r, int g, int b)
        {
            foreach (DataGridViewRow row in tb.Rows)
            {
                if (double.Parse(row.Cells[celda].Value.ToString()) == double.Parse(comprobacion))
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(r, g, b);
                }
            }
        }
        public double sumaceldas(DataGridView tb, int col)
        {
            double Total1 = 0;
            for (int contador = 0; contador < tb.Rows.Count; contador++)
            {
                if (tb[col, contador].Value.ToString().Equals("") || tb[col, contador].Value.ToString().Equals(null))
                {
                    //Total1 = 0;
                }
                else { Total1 = Total1 + double.Parse(tb[col, contador].Value.ToString()); }

            }
            return Total1;
        }
        public void TamañoLetra(DataGridView tb, int tam)
        {
            tb.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", tam);
            tb.DefaultCellStyle.Font = new Font("Arial", tam);
        }


        #region FORMATOS CON FORMATING
        public void FilaNegritas(DataGridViewCellFormattingEventArgs e, int columna)
        {
            if (e.ColumnIndex == columna && e.Value != null)
                e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 12, FontStyle.Bold);

        }
        public void alinear(DataGridViewCellFormattingEventArgs e, int columna, int alig)
        {
            if (e.ColumnIndex == columna && e.Value != null)
            {
                switch (alig)
                {
                    case 0:
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case 1:
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        break;
                }

            }
        }

        public void celdaColor(DataGridViewCellFormattingEventArgs e, int columna, string op, int r, int g, int b)
        {
            if (e.ColumnIndex == columna && e.Value != null && e.Value.ToString() == op)
            {
                e.CellStyle.BackColor = Color.FromArgb(r, g, b);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }


        public void celdaColorExp(DataGridViewCellFormattingEventArgs e, int columna, string exp, int r, int g, int b)
        {
            Regex regex = new Regex(exp);
            bool dat = regex.IsMatch(e.Value.ToString());

            if (e.ColumnIndex == columna && e.Value != null && dat)
            {
                e.CellStyle.BackColor = Color.FromArgb(r, g, b);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }


        public void informacionCelda(DataGridView tb, DataGridViewCellFormattingEventArgs e, int columna, string op)
        {
            if (e.ColumnIndex == columna && e.Value != null)
            {
                tb.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = op + " " + e.Value.ToString();
            }
        }

        public void ColocarIcono(DataGridView tb, DataGridViewCellFormattingEventArgs e, int columna, string op)
        {
            if (e.ColumnIndex == columna && e.Value != null && e.Value.ToString() == op)
            {
                e.Value = (Bitmap)Image.FromFile("computer.ico"); /*Properties.Resources.IconoAprobado; */// Reemplaza con tu propio recurso de imagen.
            }
        }

        public void AjustarAltura(DataGridView tb, DataGridViewCellFormattingEventArgs e, int columna, int alto)
        {

            if (e.ColumnIndex == columna && e.Value != null)
                tb.Rows[e.RowIndex].Height = alto; /*se puede calcular para generar cualquier ancho*/

        }

        public void SoloLectura(DataGridView tb, DataGridViewCellFormattingEventArgs e, int columna)
        {
            if (e.ColumnIndex == columna)
            {
                e.CellStyle.BackColor = Color.LightGray;
                tb.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;
            }
        }
        public void formatoCondicional(DataGridView tb, DataGridViewCellFormattingEventArgs e, int columna, int columna2)
        {
            if (e.ColumnIndex == columna && e.Value != null && e.Value is int &&
                tb.Rows[e.RowIndex].Cells[columna2].Value != null &&
                (int)e.Value > (int)tb.Rows[e.RowIndex].Cells[columna2].Value)
            {
                e.CellStyle.BackColor = Color.Yellow;
            }
        }
        public void formatoFecha(DataGridViewCellFormattingEventArgs e, int columna)
        {
            if (e.ColumnIndex == columna && e.Value != null && e.Value is DateTime)
            {
                e.Value = ((DateTime)e.Value).ToString("dd/MM/yyyy"); // Personaliza el formato de fecha según tus necesidades.
            }
        }
        public void formatoAlternado(DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex % 2 == 0)
            {
                e.CellStyle.BackColor = Color.LightGray;
            }
        }
        public void FuenteContenido(DataGridViewCellFormattingEventArgs e, int columna)
        {
            // Cambiar el tamaño de la fuente para valores grandes en una columna específica (cambia el índice de columna según tu caso).
            if (e.ColumnIndex == columna && e.Value != null && e.Value is int && (int)e.Value > 100)
            {
                e.CellStyle.Font = new Font(e.CellStyle.Font.FontFamily, 12, FontStyle.Bold); // Ajusta según tus necesidades.
            }
        }
        public void FormatoCondicional(DataGridViewCellFormattingEventArgs e, int columna, int a, int b)
        {
            // Cambiar el tamaño de la fuente para valores grandes en una columna específica (cambia el índice de columna según tu caso).
            if (e.ColumnIndex == columna && e.Value != null && e.Value is int)
            {
                int valor = (int)e.Value;
                if (valor < a)
                {
                    e.CellStyle.BackColor = Color.Red;
                }
                else if (valor < b)
                {
                    e.CellStyle.BackColor = Color.Yellow;
                }
                else
                {
                    e.CellStyle.BackColor = Color.Green;
                }
            }
        }
        public void OcultarCelda(DataGridView tb, DataGridViewCellFormattingEventArgs e, int columna, string nom)
        {
            if (e.ColumnIndex == columna && e.Value != null && e.Value.ToString() == nom)
            {
                e.CellStyle.BackColor = Color.Gray;
                tb.Rows[e.RowIndex].Visible = false;
            }
        }
        public void MostrarCelda(DataGridView tb, DataGridViewCellFormattingEventArgs e, int columna, string nom)
        {
            if (e.ColumnIndex == columna && e.Value != null && e.Value.ToString() == nom)
            {
                e.CellStyle.BackColor = Color.Gray;
                tb.Rows[e.RowIndex].Visible = true;
            }
        }
        public void FormatoCondicionalDatos(DataGridViewCellFormattingEventArgs e)
        {
            // Cambiar el formato de celda basado en el tipo de datos (por ejemplo, mostrar valores decimales con dos decimales).
            if (e.Value != null)
            {
                if (e.Value is double)
                {
                    e.Value = ((double)e.Value).ToString("N2");
                }
                else if (e.Value is DateTime)
                {
                    e.Value = ((DateTime)e.Value).ToShortDateString();
                }
            }
        }
        public void ResaltarSeleccion(DataGridView tb, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == tb.SelectedCells[0].RowIndex)
            {
                e.CellStyle.BackColor = Color.LightBlue;
            }
        }
        public void LongitudContenido(DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value != null && e.Value.ToString().Length > 150)
            {
                e.Value = e.Value.ToString().Substring(0, 90) + "..."; // Mostrar solo los primeros 10 caracteres.
            }
        }

        #endregion


    }
}
