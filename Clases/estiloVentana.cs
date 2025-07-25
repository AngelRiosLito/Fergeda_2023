using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fergeda_2023.Clases
{
    class estiloVentana
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
      (
          int nLeftRect,     // x-coordinada de esquina izquierda
          int nTopRect,      // y-coordinate de esquuia izquierda
          int nRightRect,    // x-coordinate de esquina derecha arriba
          int nBottomRect,   // y-coordinate de esquina derecha abajo
          int nWidthEllipse, // width de ellipse
          int nHeightEllipse // height de ellipse
      );

        public void estilo(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, form.Width, form.Height, 20, 20));
        }

        public void MensagePeque(Control ctl, string msg)
        {
            var msayuda = new ToolTip();
            msayuda.SetToolTip(ctl, msg);
            msayuda.BackColor = Color.FromArgb(52, 152, 219);
            //msayuda.ForeColor = Color.White;
            msayuda.IsBalloon = true;
        }

        #region CAMBIAR FONDO DE PANTALLA
        private const uint SPI_SETDESKWALLPAPER = 20;
        private const uint SPIF_UPDATEINIFILE = 0x01;
        private const uint SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, string pvParam, uint fWinIni);

        public static void SetDesktopWallpaper(string path)
        {
            if (!SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE))
            {
                throw new Win32Exception();
            }
        }
        #endregion
    }
}
