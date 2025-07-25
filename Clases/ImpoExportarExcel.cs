using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Clases
{
    class ImpoExportarExcel
    {
        private Archivos arc = new Archivos();
        public void exportarExcel(DataGridView tb, string ruta)
        {
            SLDocument sl = new SLDocument();
            int iR = 2, iC = 0;
            SLStyle style = new SLStyle();
            style.SetFont("Arial", 12);

            style.Font.Bold = true;
            style.Fill.SetPattern(PatternValues.LightDown, System.Drawing.Color.Blue, System.Drawing.Color.White);
            style.Fill.SetPatternType(PatternValues.Solid);
            style.Fill.SetPatternForegroundColor(SLThemeColorIndexValues.Accent1Color);

            /*Encabezado*/
            foreach (DataGridViewColumn col in tb.Columns)
            {
                sl.SetCellValue(1, iC + 1, col.HeaderText.ToString());
                sl.SetCellStyle(1, iC + 1, style);
                iC++;
            }

            foreach (DataGridViewRow row in tb.Rows)
            {
                for (int i = 0; i < tb.ColumnCount; i++)
                {
                    sl.SetCellValue(iR, i + 1, row.Cells[i].Value.ToString());
                }
                iR++;
            }
            sl.SaveAs(ruta);
            arc.abirArchivo(ruta);

        }
    }
}
